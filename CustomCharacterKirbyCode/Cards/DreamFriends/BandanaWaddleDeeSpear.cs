// using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
// using CustomCharacterKirby.CustomCharacterKirbyCode.DreamFriends;
// using MegaCrit.Sts2.Core.Combat;
// using MegaCrit.Sts2.Core.Commands;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.Entities.Creatures;
// using MegaCrit.Sts2.Core.Entities.Players;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Localization.DynamicVars;
// using MegaCrit.Sts2.Core.Models;
// using MegaCrit.Sts2.Core.Models.Cards;
// using MegaCrit.Sts2.Core.ValueProps;
//
// namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.DreamFriends;
//
// public class BandanaWaddleDeeSpear() : CustomCharacterKirbyCard(0, CardType.Attack, CardRarity.Token, TargetType.AllEnemies)
// {
//     protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(2M, ValueProp.Move), new RepeatVar(3)];
//
//     public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
//     
//     protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
//     {
//         BandanaWaddleDeeSpear card = this;
//         
//         // Deal damage
//         await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(card.DynamicVars.Repeat.IntValue).FromCard(card).TargetingAllOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
//     }
//
//     protected override void OnUpgrade() => DynamicVars.Repeat.UpgradeValueBy(2M);
//     
//     public static async Task<IEnumerable<CardModel>> CreateInHand(Player owner, int count, CombatState combatState)
//     {
//         if (count == 0 || CombatManager.Instance.IsOverOrEnding) return [];
//         
//         List<CardModel> cards = new List<CardModel>();
//         for (int index = 0; index < count; ++index)
//             cards.Add(combatState.CreateCard<BandanaWaddleDeeSpear>(owner));
//         IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat(cards, PileType.Hand, true);
//         return cards;
//     }
// }