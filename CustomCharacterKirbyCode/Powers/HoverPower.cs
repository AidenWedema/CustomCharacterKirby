using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
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

    public override bool AllowNegative => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("DamageDecrease", 50M)];
    
    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource) =>
        target != this.Owner || !IsPoweredAttack(props) ? 1M : this.DynamicVars["DamageDecrease"].BaseValue / 100M;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        HoverPower power = this;
        if (target != power.Owner || result.UnblockedDamage == 0 || !IsPoweredAttack(props)) return;
        power.Flash();
        var amount = await PowerCmd.ModifyAmount(power, -1M, null, null);
        if (amount <= 0)
            await PowerCmd.Apply<StunnedPower>(power.Owner, 1, power.Owner, null);
    }

    private bool IsPoweredAttack(ValueProp props) => props.HasFlag(ValueProp.Move) && !props.HasFlag(ValueProp.Unpowered);
}