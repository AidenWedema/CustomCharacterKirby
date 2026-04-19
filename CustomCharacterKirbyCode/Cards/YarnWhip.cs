using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class YarnWhip() : CustomCharacterKirbyCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(12M, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        YarnWhip card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        
        AttackContext attackContext = await AttackCommand.CreateContextAsync(card.CombatState, card);
        
        // Deal damage to the target
        var damageResults = await CreatureCmd.Damage(choiceContext, cardPlay.Target, card.DynamicVars.Damage, card.Owner.Creature, card);
        attackContext.AddHit(damageResults);

        // If lethal, damage a random enemy
        if (damageResults.Count(r => r.WasTargetKilled) > 0)
            await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).TargetingRandomOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        await attackContext.DisposeAsync();
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(5M);
}