using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

[Pool(typeof(CustomCharacterKirbyCardPool))]
public class Inhale() : AbilityCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [ new DynamicVar("ProjectileStarGain", 1M)];

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Copy>(), HoverTipFactory.FromPower<StrengthPower>(), HoverTipFactory.FromPower<ProjectileStarPower>()];

    protected override AbilityType abilityType => AbilityType.BasicSkill;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Inhale card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        // Apply -1 strength to the target
        await PowerCmd.Apply<StrengthPower>(cardPlay.Target, -1, card.Owner.Creature, card);
        // Gain a projectile star
        await PowerCmd.Apply<ProjectileStarPower>(card.Owner.Creature, DynamicVars["ProjectileStarGain"].BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars["ProjectileStarGain"].UpgradeValueBy(2M);
}
