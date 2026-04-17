using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class OverheadSlash() : AbilityCard (1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    private bool HasSkyEnergySword
    {
        get => CombatManager.Instance.IsInProgress && this.Owner.Creature.HasPower<SkyEnergySwordPower>();
    }
    
    public override TargetType TargetType
    {
        get => !this.HasSkyEnergySword ? TargetType.AnyEnemy : TargetType.AllEnemies;
    }
    
    protected override AbilityType abilityType => AbilityType.BasicAttack;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(8M, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        OverheadSlash card = this;
        AttackCommand attackCommand1 = DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card);
        AttackCommand attackCommand2;
        OverheadSlash slash = card;
        if (card.HasSkyEnergySword)
        {
            Creature lastEnemy = card.CombatState.HittableEnemies.LastOrDefault<Creature>();
            attackCommand2 = attackCommand1.TargetingAllOpponents(card.CombatState).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(slash.Owner.Creature, lastEnemy, Colors.Pink)));
            
            // Decrement Sky Enegry Sword
            await PowerCmd.Remove(Owner.Creature.GetPower<SkyEnergySwordPower>());
        }
        else
        {
            ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
            attackCommand2 = attackCommand1.Targeting(play.Target).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(slash.Owner.Creature, play.Target, Colors.Pink)));
        }
        if (card.Owner.Character is Character.CustomCharacterKirby)
            attackCommand2.WithAttackerAnim(nameof (OverheadSlash), 0.2f);
        AttackCommand attackCommand3 = await attackCommand2.Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
}