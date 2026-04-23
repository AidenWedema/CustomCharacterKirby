using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class LeafScatter() : AbilityCard(2, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Copy>(), HoverTipFactory.FromPower<LeafPower>()];

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
    
    protected override AbilityType abilityType => AbilityType.Forward;

    protected override bool IsPlayable => HasLeaf;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [
        new DamageVar(0M, ValueProp.Move),
        new ExtraDamageVar(3M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) =>
        {
            var leafAmount = 0;
            if (CombatManager.Instance.IsInProgress && card.Owner.Creature.HasPower<LeafPower>())
                leafAmount = card.Owner.Creature.GetPower<LeafPower>()!.Amount;
            return card.DynamicVars.ExtraDamage.BaseValue * leafAmount * 0.2M;
        })
    ];

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        LeafScatter card = this;

        // Deal damage
        Creature lastEnemy = card.CombatState.HittableEnemies.LastOrDefault<Creature>();
        await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate(cardPlay.Target)).FromCard((CardModel) card).TargetingAllOpponents(card.CombatState).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(card.Owner.Creature, lastEnemy, Colors.ForestGreen))).Execute(choiceContext);
        
        // Remove all leaf
        await PowerCmd.Remove<LeafPower>(card.Owner.Creature);
    }

    protected override void OnUpgrade() => DynamicVars.ExtraDamage.UpgradeValueBy(2M);
}
