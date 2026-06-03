using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Rooms;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Relics;

[Pool(typeof(CustomCharacterKirbyRelicPool))]
public class HeartSpear() : CustomCharacterKirbyRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    
    
    private static readonly Vector2[] FriendPositions = [
        new Vector2(250f, -75f),
        new Vector2(275f, -50f),
        new Vector2(250f, -25f)
    ];

    public bool Triggered;
    public List<Creature> DontBefriendThese = new();
    
    public override Task BeforeCombatStart()
    {
        Triggered = false;
        Status = RelicStatus.Active;
        DontBefriendThese.Clear();
        return Task.CompletedTask;
    }

    public override Task AfterCombatEnd(CombatRoom room)
    {
        Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (wasRemovalPrevented || creature.Monster == null || Triggered || DontBefriendThese.Any(c => c == creature)) return;
        
        await DreamFriendCmd.Befriend(Owner, creature);
        
        Status = RelicStatus.Normal;
        Triggered = true;
        
        this.Flash();
    }
}