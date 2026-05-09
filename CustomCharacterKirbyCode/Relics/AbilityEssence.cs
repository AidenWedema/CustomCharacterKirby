using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Relics;

public class AbilityEssence() : CustomCharacterKirbyRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public static readonly List<CopyEssenceCard> EssenceCards =
    [
        ModelDb.Card<SwordEssence>(),
        ModelDb.Card<LeafEssence>(),
        ModelDb.Card<IceEssence>(),
        ModelDb.Card<MirrorEssence>(),
        ModelDb.Card<NeedleEssence>(),
        ModelDb.Card<BeamEssence>(),
        ModelDb.Card<ParasolEssence>()
    ];

    public override async Task BeforeCombatStart()
    {
        // Chose a random card
        var r = Owner.RunState.Rng.Niche.NextInt(0, EssenceCards.Count);
        var essenceCard = EssenceCards[r];
        
        // Copy the power
        var powerModel = (PowerModel)essenceCard.CopyAbility.MutableClone();
        
        // Remove existing CopyAbility powers
        var player = Owner.Creature;
        var existing = player.Powers.OfType<CopyAbility>().ToList();
        foreach (var power in existing)
            await PowerCmd.Remove(power);
        
        // Apply the copy ability to the player
        await PowerCmd.Apply(
            powerModel,                         // power to apply
            Owner.Creature,    // creature to apply the power to
            1,                           // amount
            Owner.Creature,    // creature that applies the power
            null                          // card source
        );
        
        // Transform all cards
        List<CardModel> deck = [];
        var combatState = Owner.PlayerCombatState;
        if (combatState == null) throw new NullReferenceException(nameof(combatState));
        deck.AddRange(combatState.Hand.Cards);
        deck.AddRange(combatState.DiscardPile.Cards);
        deck.AddRange(combatState.DrawPile.Cards);
        deck.AddRange(combatState.ExhaustPile.Cards);
        
        List<AbilityCard> abilityCards = [];
        foreach (var card in deck)
            if (card is AbilityCard abilityCard) abilityCards.Add(abilityCard);

        for (var i = 0; i < abilityCards.Count; i++)
        {
            var card = abilityCards[i];
            await card.OnAbilityChanged(this.Owner.Creature.CombatState, this.Owner, essenceCard.CopyAbility);
        }
    }
}