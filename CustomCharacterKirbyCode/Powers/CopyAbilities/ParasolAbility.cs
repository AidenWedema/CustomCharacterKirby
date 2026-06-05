using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class ParasolAbility : CopyAbility
{
    public override string DisplayName => new LocString("powers", "parasol.title").GetFormattedText();
    public override string SpritePath => Path.Join(MainFile.ResPath, "images", "powers", "big", "parasol_ability.png");

    public override AbilityCard BasicAttackCard => ModelDb.Card<ParasolSwing>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<ParasolShield>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<ParasolDrill>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<CircusThrow>();
    
    public override AbilityCard DownCard => ModelDb.Card<ParasolTwirl>();
}