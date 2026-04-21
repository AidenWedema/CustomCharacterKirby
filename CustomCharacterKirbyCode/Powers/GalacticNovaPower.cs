using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class GalacticNovaPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => false;


    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        GalacticNovaPower power = this;
        if (player != power.Owner.Player) return Task.CompletedTask;
        
        for (var i = 0; i < power.Amount; i++)
        {
            // Get all cards in the players hand that cost more than 0 energy
            var hand = PileType.Hand.GetPile(power.Owner.Player).Cards.Where(c => c.EnergyCost.Canonical > 0).ToList();
            if (hand.Count == 0) break;
            // Set the cost of a random card to 0
            var r = power.Owner.Player.RunState.Rng.CombatCardSelection.NextInt(0, hand.Count);
            hand[r].EnergyCost.SetThisTurn(0);
        }
        
        power.Flash();
        return Task.CompletedTask;
    }
}