using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class FriendCircle() : CustomCharacterKirbyCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4, ValueProp.Move),
        new ExtraDamageVar(1M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => card.DynamicVars.Damage.IntValue * (DreamFriendCmd.GetAllPets<MonsterModel>(card.Owner.Creature).Count + 1))
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Exhaust), HoverTipFactory.FromPower<ProjectileStarPower>()];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        FriendCircle card = this;
        
        Creature lastEnemy = card.CombatState.HittableEnemies.LastOrDefault();
        var damage = card.DynamicVars.Damage.BaseValue * (DreamFriendCmd.GetAllAlivePets<MonsterModel>(card.Owner.Creature).Count + 1);
        await DamageCmd.Attack(damage).FromCard(card).TargetingAllOpponents(card.CombatState).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(card.Owner.Creature, lastEnemy, Colors.Pink))).Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(2);
}