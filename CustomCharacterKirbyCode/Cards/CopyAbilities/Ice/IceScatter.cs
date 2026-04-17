using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class IceScatter() : AbilityCard (2, CardType.Skill, CardRarity.Basic, TargetType.AllEnemies)
{
    protected override AbilityType abilityType => AbilityType.Down;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [
        new DamageVar(3M, ValueProp.Move),
        new ExtraDamageVar(0M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => card.DynamicVars.Damage.BaseValue * card.Owner.Creature.Block + card.DynamicVars.Damage.BaseValue)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IceScatter card = this;
        
        // Deal Damage
        var blockAmount = card.Owner.Creature.Block;
        await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate((cardPlay.Target))).FromCard(card).TargetingAllOpponents(card.CombatState).Execute(choiceContext);
        
        // Remove Block
        await CreatureCmd.LoseBlock(card.Owner.Creature, blockAmount);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
}