// using BaseLib.Utils;
// using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
// using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
// using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
// using Godot;
// using MegaCrit.Sts2.Core.Combat;
// using MegaCrit.Sts2.Core.Commands;
// using MegaCrit.Sts2.Core.Commands.Builders;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.Entities.Creatures;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Localization.DynamicVars;
// using MegaCrit.Sts2.Core.Models;
// using MegaCrit.Sts2.Core.Models.Characters;
// using MegaCrit.Sts2.Core.Models.Powers;
// using MegaCrit.Sts2.Core.Nodes.Vfx;
// using MegaCrit.Sts2.Core.ValueProps;
//
// namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
//
// public class DownThrust() : AbilityCard (1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
// {
//     private bool HasUpwardSlash => CombatManager.Instance.IsInProgress && this.Owner.Creature.HasPower<UpwardSlashPower>();
//     
//     protected override AbilityType abilityType => AbilityType.Down;
//     
//     protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8M, ValueProp.Move), new ExtraDamageVar(6M)];
//
//     protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
//     {
//         DownThrust card = this;
//         
//         // Calculate damage based on if the owner has the Upward Slash power
//         decimal damage = DynamicVars.Damage.BaseValue;
//         if (card.HasUpwardSlash) damage += DynamicVars.ExtraDamage.BaseValue;
//         
//         // Attack the target
//         await DamageCmd.Attack(damage).FromCard((CardModel) card).Targeting(play.Target).WithHitVfxNode((Func<Creature, Node2D>) (_ => (Node2D) NShivThrowVfx.Create(card.Owner.Creature, play.Target, Colors.Pink))).Execute(choiceContext);
//         
//         // Decrement Upward Slash
//         await PowerCmd.Decrement(Owner.Creature.GetPower<UpwardSlashPower>());
//     }
//
//     protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(4M);
// }