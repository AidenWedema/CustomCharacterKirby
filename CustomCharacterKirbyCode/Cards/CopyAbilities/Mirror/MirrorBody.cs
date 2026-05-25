using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class MirrorBody() : AbilityCard(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override AbilityType abilityType => AbilityType.Forward;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new BlockVar(4M, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Copy>(), HoverTipFactory.FromPower<BlurPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        MirrorBody card = this;
        
        // Gain block
        await CreatureCmd.GainBlock(card.Owner.Creature, DynamicVars.Block, cardPlay);
        
        // Deal damage
        var blockAmount = card.Owner.Creature.Block;
        await DamageCmd.Attack(blockAmount).FromCard(card).TargetingRandomOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);

        // Remove block
        await CreatureCmd.LoseBlock(card.Owner.Creature, blockAmount);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
}
