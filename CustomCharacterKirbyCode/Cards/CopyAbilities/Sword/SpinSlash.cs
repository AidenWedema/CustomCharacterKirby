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
// public class SpinSlash() : AbilityCard (1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
// {
//     protected override AbilityType abilityType => AbilityType.Forward;
//     
//     protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3M, ValueProp.Move), new BlockVar(2M, ValueProp.Move)];
//
//     protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
//     {
//         SpinSlash card = this;
//
//         // Apply Thorns
//         await PowerCmd.Apply<ThornsPower>(card.Owner.Creature, DynamicVars.Damage.BaseValue, card.Owner.Creature, card);
//         
//         // Give block
//         await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, play);
//     }
//
//     protected override void OnUpgrade()
//     {
//         DynamicVars.Damage.UpgradeValueBy(2M);
//         DynamicVars.Block.UpgradeValueBy(2M);
//     }
// }