using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.DreamFriends;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.DreamFriends;

public sealed class MetaKnight : DreamFriend
{
    public override int MaxInitialHp => 16;

    private const int Damage = 3;
    private const int Repeat = 5;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player || Creature.IsDead) return;
        
        var friend = this;

        var target = RandomEnemy;
        if (target == null) return;
        await CreatureCmd.TriggerAnim(friend.Creature, "Attack", 0.2F);
        for (var i = 0; i < Repeat; i++)
            await CreatureCmd.Damage(choiceContext, target, Damage, ValueProp.Unpowered, friend.Creature);
    }
}