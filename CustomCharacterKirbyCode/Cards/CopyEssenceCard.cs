using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public abstract class CopyEssenceCard(int cost, CardType type, CardRarity rarity, TargetType target) : CustomCharacterKirbyCard(cost, type, rarity, target)
{
    public abstract CopyAbility CopyAbility { get; } // The ability to give when the card is played

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, CardKeyword.Innate, CardKeyword.Retain];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        // Get this card
        CopyEssenceCard cardSource = this;
        
        // Copy the power
        PowerModel powerModel = (PowerModel)CopyAbility.MutableClone();
        
        // Remove existing CopyAbility powers
        var player = Owner.Creature;
        var existing = player.Powers.OfType<CopyAbility>().ToList();
        foreach (var power in existing)
            await PowerCmd.Remove(power);
        
        // Apply the copy ability to the player
        await PowerCmd.Apply(
            powerModel,                         // power to apply
            cardSource.Owner.Creature,    // creature to apply the power to
            1,                           // amount
            cardSource.Owner.Creature,    // creature that applies the power
            cardSource                          // card source
        );
        
        // Transform all cards
        List<CardModel> deck = new();
        var combatState = cardSource.Owner.PlayerCombatState;
        deck.AddRange(combatState.Hand.Cards);
        deck.AddRange(combatState.DiscardPile.Cards);
        deck.AddRange(combatState.DrawPile.Cards);
        deck.AddRange(combatState.ExhaustPile.Cards);
        
        List<AbilityCard> abilityCards = new();
        foreach (var card in deck)
            if (card is AbilityCard abilityCard) abilityCards.Add(abilityCard);

        for (int i = 0; i < abilityCards.Count; i++)
        {
            var card = abilityCards[i];
            await card.OnAbilityChanged(cardSource.CombatState, cardSource.Owner, CopyAbility);
        }
        // Lazy race condition fix incoming
        await Cmd.Wait(3);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}