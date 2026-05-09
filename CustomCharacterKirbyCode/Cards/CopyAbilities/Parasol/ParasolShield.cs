using BaseLib.Extensions;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class ParasolShield() : AbilityCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [ new BlockVar(6M, ValueProp.Move), new PowerVar<ParasolShieldPower>(2M)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };

    protected override AbilityType abilityType => AbilityType.BasicSkill;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ParasolShield card = this;
        
        // Gain Block
        await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
        
        // Apply ParasolShieldPower this turn
        await PowerCmd.Apply<ParasolShieldPower>(card.Owner.Creature, DynamicVars.Power<ParasolShieldPower>().BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(3M);
}
