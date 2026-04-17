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

public class LeafRain() : AbilityCard (2, CardType.Skill, CardRarity.Basic, TargetType.AllEnemies)
{
    private int LeafAmount
    {
        get
        {
            if (CombatManager.Instance.IsInProgress && this.Owner.Creature.HasPower<LeafPower>())
                return this.Owner.Creature.GetPower<LeafPower>().Amount;
            return 0;
        }
    }
    
    private bool HasLeaf => LeafAmount > 0;

    protected override AbilityType abilityType => AbilityType.Down;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [
        new DamageVar(3M, ValueProp.Move),
        new ExtraDamageVar(2M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) =>
        {
            var leafAmount = 0;
            if (CombatManager.Instance.IsInProgress && card.Owner.Creature.HasPower<LeafPower>())
                leafAmount = card.Owner.Creature.GetPower<LeafPower>()!.Amount;
            return (card.DynamicVars.Damage.BaseValue + card.DynamicVars.ExtraDamage.BaseValue * (leafAmount - 1)) * 0.5M;
        })
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        LeafRain card = this;
        
        Creature lastEnemy = card.CombatState.HittableEnemies.LastOrDefault<Creature>();
        await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate(play.Target)).FromCard((CardModel) card).TargetingAllOpponents(card.CombatState).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(card.Owner.Creature, lastEnemy, Colors.ForestGreen))).Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(2M);
}