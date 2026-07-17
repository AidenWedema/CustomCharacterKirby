using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using CustomCharacterKirby.CustomCharacterKirbyCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class FriendHeart() : CustomCharacterKirbyCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("MaxHP", 20)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Exhaust), HoverTipFactory.FromPower<ProjectileStarPower>()];
    

    private static readonly Vector2[] FriendPositions = [
        new Vector2(250f, -75f),
        new Vector2(275f, -50f),
        new Vector2(250f, -25f)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        FriendHeart card = this;
        var target = cardPlay.Target;
        ArgumentNullException.ThrowIfNull((object)target, "cardPlay.Target");
        
        // Check if enemy HP is over max hp
        if (target.CurrentHp > DynamicVars["MaxHP"].IntValue)
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext, target, 1, card.Owner.Creature, card);
            return;
        }

        // Befriend the target
        await DreamFriendCmd.Befriend(card.Owner, target);
    }

    protected override void OnUpgrade() => DynamicVars["MaxHP"].UpgradeValueBy(10);
}