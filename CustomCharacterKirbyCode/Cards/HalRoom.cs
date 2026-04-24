using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class HalRoom() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        HalRoom card = this;
        CardModel? choice = await CardSelectCmd.FromChooseACardScreen(choiceContext, CardFactory.GetDistinctForCombat(card.Owner, card.Owner.Character.CardPool.GetUnlockedCards(card.Owner.UnlockState, card.Owner.RunState.CardMultiplayerConstraint).Where(c => c is CopyEssenceCard && c.Type == CardType.Skill), 3, card.Owner.RunState.Rng.CombatCardGeneration).ToList(), card.Owner, true);
        if (choice == null)
            return;
        choice.SetToFreeThisTurn();
        await CardPileCmd.AddGeneratedCardToCombat(choice, PileType.Hand, true);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}