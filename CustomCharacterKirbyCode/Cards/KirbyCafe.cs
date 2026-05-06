using BaseLib.Extensions;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class KirbyCafe() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ProjectileStarPower>(1M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedVar("CalculatedStars").WithMultiplier(((Func<CardModel, Creature, Decimal>) ((card, _) => PileType.Exhaust.GetPile(card.Owner).Cards.Count(c => c is CopyEssenceCard && c.Type == CardType.Skill)))!)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ProjectileStarPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        KirbyCafe card = this;
        
        // Get amount of copy essences in exhaust pile
        IEnumerable<CardModel> list = PileType.Exhaust.GetPile(card.Owner).Cards.Where(c => c is CopyEssenceCard && c.Type == CardType.Skill).ToList();
        decimal amount = list.Count();
        
        await PowerCmd.Apply<ProjectileStarPower>(card.Owner.Creature, amount, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Power<ProjectileStarPower>().UpgradeValueBy(1);
}