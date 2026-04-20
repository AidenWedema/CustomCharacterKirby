using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class GalacticNova() : CustomCharacterKirbyCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("CardAmount", 1)];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<GalacticNovaPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        GalacticNova card = this;

        await PowerCmd.Apply<GalacticNovaPower>(card.Owner.Creature, 1, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars["CardAmount"].UpgradeValueBy(1);
}