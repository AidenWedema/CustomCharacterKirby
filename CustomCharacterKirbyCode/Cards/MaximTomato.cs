using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class MaximTomato() : CustomCharacterKirbyCard(5, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext,CardPlay play)
    {
        MaximTomato card = this;
        
        // Heal the owner
        await CreatureCmd.Heal(card.Owner.Creature, card.Owner.Creature.MaxHp);
        
        // Remove card from deck
        await CardPileCmd.RemoveFromDeck(card.DeckVersion);
        
        // Exhaust
        await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Retain);
}