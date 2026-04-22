// using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
// using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
// using MegaCrit.Sts2.Core.Commands;
// using MegaCrit.Sts2.Core.Commands.Builders;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Localization.DynamicVars;
// using MegaCrit.Sts2.Core.ValueProps;
// using MegaCrit.Sts2.Core.Models;
//
// namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
//
// public class BasicAttack() : AbilityCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
// {
//     protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(6M, ValueProp.Move)];
//     protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];
//
//     protected override AbilityType abilityType => AbilityType.BasicAttack;
//
//     protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
//     {
//         BasicAttack card = this;
//         ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
//         // Deal damage
//         AttackCommand attackCommand = await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
//     }
//
//     protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(2M);
// }
