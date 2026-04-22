using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sleep;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class SleepPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override bool AllowNegative => false;


    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player) return;

        await CreatureCmd.Heal(Owner, 5);
    }

    public override decimal ModifyHandDrawLate(Player player, decimal count)
    {
        SleepPower power = this;
        return power.Owner.Player == player ? 0 : count; // only apply to the player the power is owned by
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await Awake.CreateInHand(player, CombatState);
    }
}