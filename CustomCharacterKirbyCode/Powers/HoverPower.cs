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
        await PowerCmd.Decrement(power);
        power.Flash();
    }

    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        HoverPower power = this;
        return power.Amount > 0 ? count : 0;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        HoverPower power = this;
        if (side != CombatSide.Player && power.Amount > 0) return;
        await PowerCmd.Remove(power);
    }
    
    private bool IsPoweredAttack(ValueProp props) => props.HasFlag(ValueProp.Move) && !props.HasFlag(ValueProp.Unpowered);
}