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
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

[Pool(typeof(CustomCharacterKirbyCardPool))]
public class StarSpit() : AbilityCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    private int AmountOfStars
    {
        get
        {
            if (CombatManager.Instance.IsInProgress && this.Owner.Creature.HasPower<ProjectileStarPower>())
                return this.Owner.Creature.GetPower<ProjectileStarPower>().Amount;
            return 0;
        }
    }
    protected override AbilityType abilityType => AbilityType.Forward;
    
    protected override bool IsPlayable => AmountOfStars > 0;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [
        new DamageVar(8M, ValueProp.Move),
        new ExtraDamageVar(5M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) =>
        {
            var amountOfStars = 0;
            if (CombatManager.Instance.IsInProgress && card.Owner.Creature.HasPower<ProjectileStarPower>())
                amountOfStars = card.Owner.Creature.GetPower<ProjectileStarPower>()!.Amount;
            return (card.DynamicVars.Damage.BaseValue + card.DynamicVars.ExtraDamage.BaseValue * amountOfStars) * 0.2M;
        })
    ];

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        StarSpit card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        
        // Damage goes up with the amount of stars
        await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate(cardPlay.Target)).FromCard((CardModel) card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        // Remove all Stars
        await PowerCmd.Remove<ProjectileStarPower>(card.Owner.Creature);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3M);
        //DynamicVars.CalculatedDamage.UpgradeValueBy(3M);
    }
}
