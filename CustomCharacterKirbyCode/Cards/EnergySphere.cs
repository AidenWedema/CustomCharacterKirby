using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class EnergySphere() : CustomCharacterKirbyCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        EnergySphere card = this;
        
        var choice = (await CardSelectCmd.FromHand(choiceContext, card.Owner, new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1), (Func<CardModel, bool>)(c => !c.Keywords.Contains(CardKeyword.Retain)), card)).ToList();
        choice[0].AddKeyword(CardKeyword.Retain);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}