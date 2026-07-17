using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using CustomCharacterKirby.CustomCharacterKirbyCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace CustomCharacterKirby.CustomCharacterKirbyCode;

public static class DreamFriendCmd
{
    public static async Task<Creature> Summon<T>(Player summoner, int hp, bool allowRevive = true, bool raiseMaxHp = true) where T : MonsterModel
    {
        var monster = ModelDb.Monster<T>().ToMutable();
        return await Summon(monster, summoner, hp, allowRevive, raiseMaxHp);
    }
    
    
    public static async Task<Creature> Summon(MonsterModel monster, Player summoner, int hp, bool allowRevive = true, bool raiseMaxHp = true, bool forceNew = false)
    {
        var combatState = summoner.Creature.CombatState;
        ArgumentNullException.ThrowIfNull(combatState);
        ArgumentNullException.ThrowIfNull(summoner.PlayerCombatState);
        ArgumentNullException.ThrowIfNull(summoner.Creature.CombatState);

        // Find existing instance of this exact monster type owned by the player
        var existing = combatState.Allies.FirstOrDefault(c => c.Monster != null && c.Monster.GetType() == monster.GetType() && c.PetOwner == summoner);

        if (forceNew) existing = null;
        
        var isReviving = existing is { IsAlive: false };

        if (existing is { IsAlive: true })
        {
            if (raiseMaxHp)
                await CreatureCmd.GainMaxHp(existing, hp);
            else
                await CreatureCmd.Heal(existing, hp);
            return existing;
        }

        if (isReviving)
        {
            // Return if revive is not allowed
            if (!allowRevive) return existing!;
            summoner.PlayerCombatState.AddPetInternal(existing!);
        }
        else
        {
            existing = summoner.Creature.CombatState.CreateCreature(monster.ToMutable(), summoner.Creature.Side, null);
            await PlayerCmd.AddPet(existing, summoner);

            var node = SetPositionRelativeToOwner(existing, summoner, new Vector2(0f, -1000f));

            await PowerCmd.Apply<FriendHeartPower>(new ThrowingPlayerChoiceContext(), existing, 1M, null, null);
            node?.TrackBlockStatus(summoner.Creature);
            node?.ToggleIsInteractable(true);
        }

        ArgumentNullException.ThrowIfNull(existing);
        await CreatureCmd.SetMaxHp(existing, hp);
        await CreatureCmd.Heal(existing, hp, isReviving);

        return existing;
    }

    public static NCreature? SetPositionRelativeToOwner(Creature creature, Player owner, Vector2 position)
    {
        var node = NCombatRoom.Instance?.GetCreatureNode(creature);
        var playerNode = NCombatRoom.Instance?.GetCreatureNode(owner.Creature);

        if (node != null && playerNode != null)
        {
            node.Position = playerNode.Position + position;
            node.Modulate = Colors.Transparent;
            node.CreateTween()
                .TweenProperty(node, "modulate", Colors.White, 0.35)
                .SetDelay(0.1);
        }

        return node;
    }

    public static IReadOnlyList<T> GetAllPets<T>(Creature owner) where T : MonsterModel 
    {
        var pets = owner.Pets;
        List<T> existingPets = [];
        foreach (var pet in pets)
        {
            var monster = pet.Monster;
            if(monster is not T m) continue;
            existingPets.Add(m);
        }

        return existingPets;
    }

    public static IReadOnlyList<T> GetAllAlivePets<T>(Creature owner) where T : MonsterModel
    {
        var allPets = GetAllPets<T>(owner);
        return allPets.Where(pet => pet.Creature.IsAlive).ToList();
    }

    public static IReadOnlyList<T> GetAllDeadPets<T>(Creature owner) where T : MonsterModel
    {
        var allPets = GetAllPets<T>(owner);
        return allPets.Where(pet => pet.Creature.IsDead).ToList();
    }

    public static async Task<Creature> Befriend(Player player, Creature target)
    {
        // Prevent the HeartSpear relic from also befriending the target when it is killed, which would result in two of the same friend.
        var heartSpear = player.Relics.FirstOrDefault(r => r is HeartSpear) as  HeartSpear;
        heartSpear?.DontBefriendThese.Add(target);

        // Force the enemy to be my friend :)
        var existingFriends = GetAllPets<MonsterModel>(player.Creature);
        
        // Get the positions of all already existing friends
        var friendPositions = new Dictionary<MonsterModel, Vector2>();
        foreach (var f in existingFriends)
        {
            var node = NCombatRoom.Instance?.GetCreatureNode(f.Creature);
            friendPositions.Add(f, node.Position);
        }
        
        // Summon the target as a pet
        var creature = await Summon(target.Monster.CanonicalInstance, player, target.MaxHp, forceNew: true);
        heartSpear?.DontBefriendThese.Add(creature);
        
        // Position the summoned creature
        var pos = new Vector2(500f, -50f);
        SetPositionRelativeToOwner(creature, player, pos);
        
        // Re-position all already existing friends
        foreach (var f in GetAllPets<MonsterModel>(player.Creature))
            SetPositionRelativeToOwner(f.Creature, player, pos);
        
        // Flip the creature to be facing right
        var visuals = NCombatRoom.Instance?.GetCreatureNode(creature)?.Visuals;
        visuals?.SetScale(visuals.GetScale() * new Vector2(-1f, 1f));

        // Delete the original enemy from existence
        if (target.IsAlive)
            await CreatureCmd.Kill(target, true);
        
        return creature;
    }

    public static async Task<Creature> Befriend(Player player, MonsterModel monster, bool flipX = true)
    {
        // Prevent the HeartSpear relic from also befriending the target when it is killed, which would result in two of the same friend.
        var heartSpear = player.Relics.FirstOrDefault(r => r is HeartSpear) as  HeartSpear;
        
        // Get all existing friends
        var existingFriends = GetAllPets<MonsterModel>(player.Creature);
        
        // Get the positions of all already existing friends
        var friendPositions = new Dictionary<MonsterModel, Vector2>();
        foreach (var f in existingFriends)
        {
            var node = NCombatRoom.Instance?.GetCreatureNode(f.Creature);
            friendPositions.Add(f, node.Position);
        }
        
        // Summon the target as a pet
        var creature = await Summon(monster, player, monster.MaxInitialHp, forceNew: true);
        heartSpear?.DontBefriendThese.Add(creature);
        
        // Position the summoned creature
        var pos = new Vector2(500f, -50f);
        SetPositionRelativeToOwner(creature, player, pos);
        
        // Re-position all already existing friends
        foreach (var f in GetAllPets<MonsterModel>(player.Creature))
            SetPositionRelativeToOwner(f.Creature, player, pos);
        
        // Flip the creature to be facing right
        if (flipX)
        {
            var visuals = NCombatRoom.Instance?.GetCreatureNode(creature)?.Visuals;
            visuals?.SetScale(visuals.GetScale() * new Vector2(-1f, 1f));
        }
        
        return creature;
    }
    
}