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

public class ParasolShieldPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        ParasolShieldPower power = this;
        await PowerCmd.Remove(power);
    }
    
    public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, Decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        ParasolShieldPower power = this;
        if (target != power.Owner || dealer == null || !props.IsPoweredAttack())
            return;
        power.Flash();
        await CreatureCmd.Damage(choiceContext, dealer, power.Amount, ValueProp.Unpowered | ValueProp.SkipHurtAnim, power.Owner, null);
    }
}