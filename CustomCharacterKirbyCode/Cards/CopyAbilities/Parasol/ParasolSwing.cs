using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class ParasolSwing() : AbilityCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(6M, ValueProp.Move), new PowerVar<SpeedPotionPower>(2M)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<DexterityPower>()];

    protected override AbilityType abilityType => AbilityType.BasicAttack;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ParasolSwing card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        
        // Deal damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        // Apply Dexterity this turn
        await PowerCmd.Apply<SpeedPotionPower>(card.Owner.Creature, DynamicVars.Power<SpeedPotionPower>().BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2M);
        DynamicVars.Power<SpeedPotionPower>().UpgradeValueBy(1M);
    }
}
