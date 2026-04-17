using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.DreamFriends;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.DreamFriends;

public sealed class BandanaWaddleDee : DreamFriend
{
    public override int MaxInitialHp => 12;

    private const int Damage = 2;
    private const int Repeat = 3;
    
    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        // if (player != Creature.PetOwner || Creature.IsDead) return;
        // IEnumerable<CardModel> inHand = await BandanaWaddleDeeSpear.CreateInHand(Creature.PetOwner, 1, combatState);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player || Creature.IsDead) return;
        
        var friend = this;

        for (var i = 0; i < Repeat; i++)
        {
            var target = RandomEnemy;
            if (target == null) return;
            await CreatureCmd.TriggerAnim(friend.Creature, "Attack", 0.2F);
            await CreatureCmd.Damage(choiceContext, target, Damage, ValueProp.Unpowered, friend.Creature);
        }

        
        
        // // Return if it's not the enemy turn that ended or if bandana dee is alive
        // if (side != CombatSide.Enemy || Creature.IsAlive) return;
        //
        // // Exhaust all BandanaWaddleDeeSpear cards the player has
        // List<CardModel> deck = new();
        // var combatState = Creature.PetOwner?.PlayerCombatState;
        // if (combatState == null) return;
        // deck.AddRange(combatState.Hand.Cards);
        // deck.AddRange(combatState.DiscardPile.Cards);
        // deck.AddRange(combatState.DrawPile.Cards);
        // deck.AddRange(combatState.ExhaustPile.Cards);
        //
        // List<BandanaWaddleDeeSpear> spearCards = new();
        // foreach (var card in deck)
        //     if (card is BandanaWaddleDeeSpear spearCard) spearCards.Add(spearCard);
        //
        // for (int i = 0; i < spearCards.Count; i++)
        // {
        //     var card = spearCards[i];
        //     await CardCmd.Exhaust(choiceContext, card);
        // }
    }
}