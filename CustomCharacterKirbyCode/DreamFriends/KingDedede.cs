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

public sealed class KingDedede : DreamFriend
{
    public override int MaxInitialHp => 20;

    private const int Damage = 8;

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player != Creature.PetOwner || Creature.IsDead) return;
        
        var friend = this;

        var target = RandomEnemy;
        if (target == null) return;
        await CreatureCmd.TriggerAnim(friend.Creature, "Attack", 0.2F);
        await CreatureCmd.Damage(choiceContext, target, Damage, ValueProp.Unpowered, friend.Creature);
    }
}