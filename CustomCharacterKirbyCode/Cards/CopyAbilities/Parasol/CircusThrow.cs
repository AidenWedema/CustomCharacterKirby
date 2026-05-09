using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class CircusThrow() : AbilityCard (1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(8M, ValueProp.Move)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Fatal)];
    
    protected override AbilityType abilityType => AbilityType.Up;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CircusThrow card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        
        // Deal damage
        var attackCommand = await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);

        var isFatal = attackCommand.Results.Any(r => r.WasTargetKilled);
        if (isFatal) return;
        
        // If the target survives, check if there is another enemy
        var combatState = CombatState;
        var allEnemies = combatState!.HittableEnemies;
        if (allEnemies.Count < 2) return;
        
        // Get a random other enemy that is not the target
        var otherEnemies = allEnemies.Where(c => !c.Equals(cardPlay.Target)).ToList();
        var r = combatState.RunState.Rng.CombatTargets.NextInt(0, otherEnemies.Count);
        var otherTarget = otherEnemies[r];
        
        // Deal damage to the Target and a random enemy
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).Targeting(otherTarget).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(4M);
}