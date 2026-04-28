using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class WallStick() : AbilityCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [ new BlockVar(3M, ValueProp.Move)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [new PowerVar<ThornsPower>(3M)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ThornsPower>()];
    
    protected override AbilityType abilityType => AbilityType.BasicSkill;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        WallStick card = this;
        // Apply Thorns
        await PowerCmd.Apply<ThornsPower>(card.Owner.Creature, DynamicVars.Power<ThornsPower>().BaseValue, card.Owner.Creature, card);
        // Apply Block
        await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(2M);
}
