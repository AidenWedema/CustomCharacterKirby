using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class ReflectGuard() : AbilityCard(2, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [ new BlockVar(4M, ValueProp.Move)];

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };

    protected override AbilityType abilityType => AbilityType.BasicSkill;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ReflectGuard card = this;

        // Apply block
        await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
        
        // Give Reflect
        await PowerCmd.Apply<ReflectPower>(card.Owner.Creature, 1, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(3M);
}
