using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class HoverPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("DamageDecrease", 50M)];
    
    public override Decimal ModifyDamageMultiplicative(Creature? target, Decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource) =>
        target != this.Owner || !IsPoweredAttack(props) ? 1M : this.DynamicVars["DamageDecrease"].BaseValue / 100M;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        HoverPower power = this;
        if (target != power.Owner || result.UnblockedDamage == 0 || !IsPoweredAttack(props)) return;
        await PowerCmd.Decrement((PowerModel) power);
        power.Flash();
        if (power.Amount > 0) return;
        // await CreatureCmd.TriggerAnim(power.Owner, "StunTrigger", 0.6f);
        // await CreatureCmd.Stun(power.Owner, power.StunnedMove);
        await Cmd.Wait(0.25f);
    }

    private Task StunnedMove(IReadOnlyList<Creature> targets) => Task.CompletedTask;
    
    
    private bool IsPoweredAttack(ValueProp props) => props.HasFlag((Enum) ValueProp.Move) && !props.HasFlag((Enum) ValueProp.Unpowered);
}