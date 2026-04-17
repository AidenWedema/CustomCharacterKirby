using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class MirrorBodySky() : AbilityCard (2, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
{
    protected override AbilityType abilityType => AbilityType.Up;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(1M, ValueProp.Move), new RepeatVar(4)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        MirrorBodySky card = this;
        
        // Deal damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(DynamicVars.Repeat.IntValue).FromCard((CardModel) card).TargetingAllOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);

        // Apply blur
        await PowerCmd.Apply<BlurPower>(card.Owner.Creature, 1, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(1M);
}