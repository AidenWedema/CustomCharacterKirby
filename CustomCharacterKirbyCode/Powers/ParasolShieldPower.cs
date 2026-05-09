using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class ParasolShieldPower : CustomCharacterKirbyPower, ITemporaryPower
{
    private bool _shouldIgnoreNextInstance;
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeApplied(Creature target, Decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (_shouldIgnoreNextInstance) _shouldIgnoreNextInstance = false;
        else
            await PowerCmd.Apply<DexterityPower>(target, amount, applier, cardSource, true);
    }
    
    public override async Task AfterPowerAmountChanged(PowerModel power, Decimal amount, Creature? applier, CardModel? cardSource)
    {
        ParasolShieldPower parasolShieldPower = this;
        if (amount == parasolShieldPower.Amount || power != parasolShieldPower) return;
        if (parasolShieldPower._shouldIgnoreNextInstance) parasolShieldPower._shouldIgnoreNextInstance = false;
        else
            await PowerCmd.Apply<DexterityPower>(parasolShieldPower.Owner, amount, applier, cardSource, true);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        ParasolShieldPower power = this;
        var thornsPower = power.Owner.Powers.OfType<ThornsPower>().FirstOrDefault();
        if (thornsPower == null) return;
        await PowerCmd.ModifyAmount(thornsPower, power.Amount, null, null);
    }

    public void IgnoreNextInstance() => _shouldIgnoreNextInstance = true;

    public AbstractModel OriginModel => ModelDb.Card<ParasolShield>();
    public PowerModel InternallyAppliedPower => ModelDb.Power<ThornsPower>();
}