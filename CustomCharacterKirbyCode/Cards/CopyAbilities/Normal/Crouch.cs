using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Combat;
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
public class Crouch() : AbilityCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    private int AmountOfStars
    {
        get
        {
            if (CombatManager.Instance.IsInProgress && this.Owner.Creature.HasPower<ProjectileStarPower>())
                return this.Owner.Creature.GetPower<ProjectileStarPower>().Amount;
            return 0;
        }
    }
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [ new BlockVar(3M, ValueProp.Move)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [new DynamicVar("ProjectileStarGain", 1M)];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Copy>(), HoverTipFactory.FromPower<StrengthPower>(), HoverTipFactory.FromPower<ProjectileStarPower>()];

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };

    protected override AbilityType abilityType => AbilityType.Down;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Crouch card = this;
        
        // Gain block
        await CreatureCmd.GainBlock(card.Owner.Creature, card.DynamicVars.Block, cardPlay);
        
        // Gain strength equal to the amount of stars
        var strengthGain = AmountOfStars * DynamicVars["ProjectileStarGain"].BaseValue;
        await PowerCmd.Apply<StrengthPower>(card.Owner.Creature, strengthGain, card.Owner.Creature, card);
        
        // Remove all stars
        await PowerCmd.Remove(Owner.Creature.GetPower<ProjectileStarPower>());
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2M);
        DynamicVars["ProjectileStarGain"].UpgradeValueBy(1M);
    }
}
