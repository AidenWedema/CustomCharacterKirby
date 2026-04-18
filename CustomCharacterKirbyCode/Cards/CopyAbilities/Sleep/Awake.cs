using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sleep;

public class Awake() : CopyEssenceCard(0, CardType.Status, CardRarity.Token, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal, CardKeyword.Exhaust];

    public override CopyAbility CopyAbility => ModelDb.Power<NormalAbility>();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Awake card = this;
        
        // Copy ability stuff
        await base.OnPlay(choiceContext, play);
        
        // Remove Sleep
        await PowerCmd.Remove<SleepPower>(card.Owner.Creature);
        
        // Draw 4 cards
        await CardPileCmd.Draw(choiceContext, 4M, card.Owner);
    }
    
    public static async Task<IEnumerable<CardModel>> CreateInHand(Player owner, CombatState combatState)
    {
        if (CombatManager.Instance.IsOverOrEnding)
            return [];
        List<CardModel> cards = [combatState.CreateCard<Awake>(owner)];
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat(cards, PileType.Hand, true);
        return cards;
    }
}