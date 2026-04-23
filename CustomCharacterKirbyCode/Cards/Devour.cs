using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class Devour() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("StarGain", 2M)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Exhaust), HoverTipFactory.FromPower<ProjectileStarPower>()];
    
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Devour card = this;
        
        var choice = (await CardSelectCmd.FromHand(choiceContext, card.Owner, new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1), null, card)).ToList();
        await CardCmd.Exhaust(choiceContext, choice[0]);
        
        await PowerCmd.Apply<ProjectileStarPower>(card.Owner.Creature, DynamicVars["StarGain"].BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars["StarGain"].UpgradeValueBy(2M);
}