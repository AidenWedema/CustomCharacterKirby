using CustomCharacterKirby.CustomCharacterKirbyCode.DreamFriends;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.Fonts;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace CustomCharacterKirby.CustomCharacterKirbyCode;

public static class DreamFriendCmd
{
    public static async Task<Creature> Summon<T>(PlayerChoiceContext ctx, Player summoner, int hp, AbstractModel? source, bool allowRevive = true, bool raiseMaxHp = true) where T : MonsterModel
    {
        var monster = ModelDb.Monster<T>().ToMutable();
        return await Summon(monster, ctx, summoner, hp, source, allowRevive, raiseMaxHp);
    }
    
    
    public static async Task<Creature> Summon(MonsterModel monster, PlayerChoiceContext ctx, Player summoner, int hp, AbstractModel? source, bool allowRevive = true, bool raiseMaxHp = true)
    {
        var combatState = summoner.Creature.CombatState;
        ArgumentNullException.ThrowIfNull(combatState);
        ArgumentNullException.ThrowIfNull(summoner.PlayerCombatState);
        ArgumentNullException.ThrowIfNull(summoner.Creature.CombatState);

        // Find existing instance of this exact monster type owned by the player
        var existing = combatState.Allies.FirstOrDefault(c => c.Monster != null && c.Monster.GetType() == monster.GetType() && c.PetOwner == summoner);

        var isReviving = existing is { IsAlive: false };

        if (existing is { IsAlive: true })
        {
            if (raiseMaxHp)
                await CreatureCmd.GainMaxHp(existing, hp);
            else
                await CreatureCmd.Heal(existing,  hp);
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

            await PowerCmd.Apply<DieForYouPower>(existing, 1M, null, null);
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
}