using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class ParasolDrill() : AbilityCard(2, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(7M, ValueProp.Move), new BlockVar(4M, ValueProp.Move)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override AbilityType abilityType => AbilityType.Forward;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ParasolDrill card = this;
        
        // Deal Damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).TargetingAllOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        // Gain Block
        await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2M);
        DynamicVars.Block.UpgradeValueBy(2M);
    }
}
