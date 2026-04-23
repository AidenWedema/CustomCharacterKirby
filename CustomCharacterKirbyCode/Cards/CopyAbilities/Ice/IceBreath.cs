using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class IceBreath() : AbilityCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override AbilityType abilityType => AbilityType.BasicAttack;
    
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(2M, ValueProp.Move)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [new DynamicVar("WeakAmount", 1M), new DynamicVar("VulnerableAmount", 1M)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Copy>(), HoverTipFactory.FromPower<WeakPower>(), HoverTipFactory.FromPower<VulnerablePower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IceBreath card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");

        // Deal damage
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        // Apply Weak
        await PowerCmd.Apply<WeakPower>(cardPlay.Target, DynamicVars["WeakAmount"].BaseValue, card.Owner.Creature, card);
        // Apply Vulnerable
        await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, DynamicVars["VulnerableAmount"].BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["WeakAmount"].UpgradeValueBy(1M);
        DynamicVars["VulnerableAmount"].UpgradeValueBy(1M);
    }
}
