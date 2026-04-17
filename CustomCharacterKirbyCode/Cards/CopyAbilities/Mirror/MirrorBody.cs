using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class MirrorBody() : AbilityCard(2, CardType.Attack, CardRarity.Basic, TargetType.RandomEnemy)
{
    protected override AbilityType abilityType => AbilityType.Forward;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(6M, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        MirrorBody card = this;
        
        // Deal damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).TargetingRandomOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);

        // Apply blur
        await PowerCmd.Apply<BlurPower>(card.Owner.Creature, 1, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
}
