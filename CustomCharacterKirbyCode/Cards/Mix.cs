using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class Mix() : CopyEssenceCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
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

    public override CopyAbility CopyAbility => null!;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        // Chose a random card
        var r = this.Owner.RunState.Rng.Niche.NextInt(0, EssenceCards.Count);
        var essenceCard = (CopyEssenceCard)EssenceCards[r].MutableClone();
        
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
        // Lazy race condition fix incoming
        await Cmd.Wait(3);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}