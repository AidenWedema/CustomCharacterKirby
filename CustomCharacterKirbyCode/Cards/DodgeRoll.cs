using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class DodgeRoll() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(0M, ValueProp.Move)];

    public override bool GainsBlock => IsUpgraded;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        DodgeRoll card = this;
        await PowerCmd.Apply<DodgeRollPower>(card.Owner.Creature, 1, card.Owner.Creature, card);
        
        if(DynamicVars.Block.BaseValue > 0)
            await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(5M);
}
