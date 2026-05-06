using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class ActionStar() : CustomCharacterKirbyCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("MaxEnergyCost", 3)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        ActionStar card = this;
        
        // Choose 3 cards from hand
        var cards = await CardSelectCmd.FromHand(choiceContext, card.Owner, new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, 0, 3), null, card);
        
        // Randomize the cost of the chosen cards
        foreach (var cardModel in cards)
        {
            var r = card.Owner.RunState.Rng.CombatEnergyCosts.NextInt(0, DynamicVars["MaxEnergyCost"].IntValue + 1);
            cardModel.EnergyCost.SetThisCombat(r);
        }
    }

    protected override void OnUpgrade() => DynamicVars["MaxEnergyCost"].UpgradeValueBy(-1);
}