using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
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

public class ReflectForce() : AbilityCard (2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override AbilityType abilityType => AbilityType.Down;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [
        new DamageVar(2M, ValueProp.Move),
        new BlockVar(3M, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ReflectForce card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
          
        // Deal damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);

        // Apply block
        var amount = await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
        
        // Give Block next turn
        await PowerCmd.Apply<BlockNextTurnPower>(card.Owner.Creature, amount, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(3M);
}