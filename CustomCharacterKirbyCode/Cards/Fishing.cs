using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class Fishing() : CustomCharacterKirbyCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Fishing card = this;
        CardSelectorPrefs prefs = new CardSelectorPrefs(card.SelectionScreenPrompt, 1);
        CardModel? choice = (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(card.Owner).Cards, card.Owner, prefs)).FirstOrDefault();
        if (choice == null)
            return;
        await CardPileCmd.Add(choice, PileType.Hand);
    }

    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Retain);
}