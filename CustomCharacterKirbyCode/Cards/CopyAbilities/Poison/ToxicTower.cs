using BaseLib.Extensions;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class ToxicTower() : AbilityCard (1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(3M, ValueProp.Move), new IntVar("Count", 3M), new PowerVar<ToxinPower>(1M)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override AbilityType abilityType => AbilityType.Up;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ToxicTower card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        
        // Deal damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).Targeting(cardPlay.Target).WithHitCount(DynamicVars["Count"].IntValue).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        // Apply Toxin
        await PowerCmd.Apply<ToxinPower>(cardPlay.Target, DynamicVars.Power<ToxinPower>().BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2M);
        DynamicVars.Power<ToxinPower>().UpgradeValueBy(1M);
    }
}