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
// public class ChopAndThrust() : AbilityCard (1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
// {
//     protected override AbilityType abilityType => AbilityType.Down;
//     
//     protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3M, ValueProp.Move)];
//
//     protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
//     {
//         ChopAndThrust card = this;
//         ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
//         // Apply Weak
//         await PowerCmd.Apply<WeakPower>(play.Target, 1, card.Owner.Creature, card);
//         
//         // Apply Vulnerable
//         await PowerCmd.Apply<VulnerablePower>(play.Target, 1, card.Owner.Creature, card);
//     }
//
//     protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
// }