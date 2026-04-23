using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class UpwardSlashPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override bool AllowNegative => false;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        UpwardSlashPower power = this;
        if (side != CombatSide.Player) return;
        await PowerCmd.Remove(power);
    }
}