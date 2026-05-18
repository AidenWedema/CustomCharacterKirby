using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class NeedleBurst() : AbilityCard (2, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [
        new DamageVar(4M, ValueProp.Move),
        new ExtraDamageVar(3M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) =>
        {
            var thornsAmount = 0;
            if (CombatManager.Instance.IsInProgress && card.Owner.Creature.HasPower<ThornsPower>())
                thornsAmount = card.Owner.Creature.GetPower<ThornsPower>()!.Amount;
            return (card.DynamicVars.Damage.BaseValue + card.DynamicVars.ExtraDamage.BaseValue * (thornsAmount - 1)) * 0.5M;
        })
    ];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ThornsPower>()];

    protected override AbilityType abilityType => AbilityType.Down;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NeedleBurst card = this;
        
        // Deal damage
        await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate(cardPlay.Target)).FromCard(card).TargetingAllOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
    }

    protected override void OnUpgrade() => DynamicVars.ExtraDamage.UpgradeValueBy(2M);
}