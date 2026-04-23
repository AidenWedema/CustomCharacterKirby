using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class DimensionMirror() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Ethereal)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        DimensionMirror card = this;
        
        var choice = (await CardSelectCmd.FromHand(choiceContext, card.Owner, new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1), (Func<CardModel, bool>)(c => c.EnergyCost.Canonical > 0), card)).ToList();
        choice[0].EnergyCost.SetThisCombat(choice[0].EnergyCost.Canonical - 1);
        choice[0].AddKeyword(CardKeyword.Ethereal);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}