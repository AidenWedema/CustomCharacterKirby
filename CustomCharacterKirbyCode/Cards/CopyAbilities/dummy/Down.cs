using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class Down() : AbilityCard (2, CardType.Skill, CardRarity.Basic, TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> OverrideCanonicalVars => [new DamageVar(3M, ValueProp.Move), new ExtraDamageVar(2M)];
    protected override IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    protected override AbilityType abilityType => AbilityType.Down;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Down card = this;
    }

    protected override void OnUpgrade() => DynamicVars.ExtraDamage.UpgradeValueBy(2M);
}