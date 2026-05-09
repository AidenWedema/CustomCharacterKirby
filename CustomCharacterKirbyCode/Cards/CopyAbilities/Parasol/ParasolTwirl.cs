using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class ParasolTwirl() : AbilityCard (2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(4M, ValueProp.Move), new IntVar("Count", 4M), new BlockVar(4M, ValueProp.Move)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override AbilityType abilityType => AbilityType.Down;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ParasolTwirl card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        
        // Deal Damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).WithHitCount(DynamicVars["Count"].IntValue).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        // Gain Block
        await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2M);
        DynamicVars.Block.UpgradeValueBy(2M);
    }
}