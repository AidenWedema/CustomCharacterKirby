using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class Crackler() : CustomCharacterKirbyCard(1, CardType.Power, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("CracklerDamage", 4M), new DynamicVar("TurnAmount", 6)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<CracklerPower>()];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Crackler card = this;
        var crackler = await PowerCmd.Apply<CracklerPower>(card.Owner.Creature, DynamicVars["TurnAmount"].BaseValue, card.Owner.Creature, card);
        crackler?.SetDamage(DynamicVars["CracklerDamage"].BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["CracklerDamage"].UpgradeValueBy(2);
        DynamicVars["TurnAmount"].UpgradeValueBy(1);
    }
}