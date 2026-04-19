using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class WarpStar() : CustomCharacterKirbyCard(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        WarpStar card = this;

        // Apply Intangible
        await PowerCmd.Apply<IntangiblePower>(card.Owner.Creature, 1, card.Owner.Creature, card);
        
        // Apply damage next turn (via The Bomb)
        (await PowerCmd.Apply<TheBombPower>(card.Owner.Creature, 1, card.Owner.Creature, card)).SetDamage(DynamicVars.Damage.BaseValue);
        
        PlayerCmd.EndTurn(card.Owner, false);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(5M);
}