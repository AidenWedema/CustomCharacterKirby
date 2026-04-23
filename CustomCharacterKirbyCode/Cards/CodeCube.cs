using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class CodeCube() : CustomCharacterKirbyCard(2, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Exhaust), HoverTipFactory.FromKeyword(CardKeyword.Ethereal)];

    protected override bool IsPlayable
    {
        get
        {
            var hand = this.Owner.PlayerCombatState?.Hand.Cards;
            return hand != null && hand.Any(c => c.Keywords.Contains(CardKeyword.Exhaust));
        }
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        CodeCube card = this;
        
        var choice = (await CardSelectCmd.FromHand(choiceContext, card.Owner, new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1), (Func<CardModel, bool>)(c => c.Keywords.Contains(CardKeyword.Exhaust)), card)).ToList();
        choice[0].RemoveKeyword(CardKeyword.Exhaust);
        choice[0].AddKeyword(CardKeyword.Ethereal);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}