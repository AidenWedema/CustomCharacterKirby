// using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
// using MegaCrit.Sts2.Core.Commands;
// using MegaCrit.Sts2.Core.Commands.Builders;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Localization.DynamicVars;
// using MegaCrit.Sts2.Core.ValueProps;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using BaseLib.Utils;
// using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
// using MegaCrit.Sts2.Core.Models;
// using MegaCrit.Sts2.Core.Models.Cards;
//
// namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
//
// // [Pool(typeof(CustomCharacterKirbyCardPool))]
// public class Jump() : AbilityCard(1, CardType.Attack, CardRarity.Common, TargetType.RandomEnemy)
// {
//     protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(2M, ValueProp.Move), new BlockVar(3M, ValueProp.Move)];
//
//     protected override AbilityType abilityType => AbilityType.BasicAttack;
//     
//     protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.None };
//
//
//     protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
//     {
//         Jump card = this;
//         AttackCommand attackCommand = await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(card.DynamicVars.Repeat.IntValue).FromCard((CardModel) card).TargetingRandomOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
//     }
//
//     protected override void OnUpgrade()
//     {
//         DynamicVars.Damage.UpgradeValueBy(4M);
//         DynamicVars.Block.UpgradeValueBy(3M);
//     }
// }
