using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class TreasureChest() : CustomCharacterKirbyCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        TreasureChest card = this;
        
        CardModel? choice = CardFactory.GetDistinctForCombat(card.Owner, card.Owner.Character.CardPool.GetUnlockedCards(card.Owner.UnlockState, card.Owner.RunState.CardMultiplayerConstraint).Where(c => c.Rarity == CardRarity.Common), 1, card.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();
        if (choice == null)
            return;
        choice.SetToFreeThisTurn();
        await CardPileCmd.AddGeneratedCardToCombat(choice, PileType.Hand, true);
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}