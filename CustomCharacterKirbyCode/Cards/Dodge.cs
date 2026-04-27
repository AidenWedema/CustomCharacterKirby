using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class Dodge() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Dodge card = this;
        await CreatureCmd.GainBlock(card.Owner.Creature, card.Owner.Creature.Block, ValueProp.Move, cardPlay);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}
