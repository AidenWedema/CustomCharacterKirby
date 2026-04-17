using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class LeafUppercut() : AbilityCard (1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override AbilityType abilityType => AbilityType.Up;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(8M, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        LeafUppercut card = this;

        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        
        // Deal damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(play.Target).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(card.Owner.Creature, play.Target, Colors.Pink))).Execute(choiceContext);
        // Gain leaf
        await PowerCmd.Apply<LeafPower>(card.Owner.Creature, 1, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(4M);
}