using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class ParasolAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<ParasolSwing>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<ParasolShield>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<ParasolDrill>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<CircusThrow>();
    
    public override AbilityCard DownCard => ModelDb.Card<ParasolTwirl>();
}