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
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

[Pool(typeof(CustomCharacterKirbyCardPool))]
public class StrikeKirby() : AbilityCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(6M, ValueProp.Move)];

    protected override AbilityType abilityType => AbilityType.BasicAttack;
    
    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { CardTag.Strike };
    }


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        StrikeKirby card = this;
        ArgumentNullException.ThrowIfNull((object)cardPlay.Target, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3M);
}
