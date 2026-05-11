using BaseLib.Extensions;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class StickyStrike() : AbilityCard (2, CardType.Skill, CardRarity.Basic, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new PowerVar<ToxinPower>(2M)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override AbilityType abilityType => AbilityType.Down;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ToxinPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        StickyStrike card = this;
        
        // Apply Toxin
        await PowerCmd.Apply<ToxinPower>(card.CombatState.Enemies, DynamicVars.Power<ToxinPower>().BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Power<ToxinPower>().UpgradeValueBy(2M);
}