using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class DimensionalRift() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("StarGain", 2M)];

    protected override bool IsPlayable => AllEssences.Count > 0;
    
    protected IReadOnlyList<CopyEssenceCard> AllEssences {
        get
        {
            // Get the entire current deck
            List<CardModel> deck = [];
            var combatState = this.Owner.PlayerCombatState;
            deck.AddRange(combatState.Hand.Cards);
            deck.AddRange(combatState.DiscardPile.Cards);
            deck.AddRange(combatState.DrawPile.Cards);
        
            // Filter all copy essences
            var essences = deck.Where(c => c.GetType() == typeof(CopyEssenceCard)) as List<CopyEssenceCard>;
            return essences == null || essences.Count > 0 ? [] : essences;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        DimensionalRift card = this;

        foreach (var essence in AllEssences)
        {
            await CardCmd.Exhaust(choiceContext, essence);
            await PowerCmd.Apply<ProjectileStarPower>(card.Owner.Creature, DynamicVars["StarGain"].BaseValue, card.Owner.Creature, card);
        }
    }

    protected override void OnUpgrade() => DynamicVars["StarGain"].UpgradeValueBy(1M);
}