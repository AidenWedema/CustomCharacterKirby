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

public class FriendStar() : CustomCharacterKirbyCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(3, ValueProp.Move),
        new ExtraDamageVar(1M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedVar("FriendAmount").WithMultiplier((card, _) => DreamFriendCmd.GetAllAlivePets<MonsterModel>(card.Owner.Creature).Count)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Exhaust), HoverTipFactory.FromPower<ProjectileStarPower>()];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        FriendStar card = this;

        var friendCount = DreamFriendCmd.GetAllPets<MonsterModel>(card.Owner.Creature).Count;
        if (friendCount > 0)
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).WithHitCount(friendCount).FromCard(card).TargetingRandomOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        Creature lastEnemy = card.CombatState.HittableEnemies.LastOrDefault();
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(card).TargetingAllOpponents(card.CombatState).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(card.Owner.Creature, lastEnemy, Colors.Yellow))).Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(1);
}