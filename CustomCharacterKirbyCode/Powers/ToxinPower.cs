using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class ToxinPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PoisonPower>(1M)];

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
    {
        // Check if the player turn is about to start
        if (side != CombatSide.Player) return;

        ToxinPower power = this;
        
        // Apply Poison
        await PowerCmd.Apply<PoisonPower>(power.Owner, power.Amount, null, null);
    }
}