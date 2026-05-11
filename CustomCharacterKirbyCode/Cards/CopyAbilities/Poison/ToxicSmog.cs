using BaseLib.Extensions;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class ToxicSmog() : AbilityCard(1, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [ new PowerVar<ToxinPower>(3M)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };

    protected override AbilityType abilityType => AbilityType.BasicSkill;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ToxicSmog card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        
        // Apply Toxin
        await PowerCmd.Apply<ToxinPower>(cardPlay.Target, DynamicVars.Power<ToxinPower>().BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Power<ToxinPower>().UpgradeValueBy(2M);
}
