using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class SwordAbility : CopyAbility
{
    public override string DisplayName => new LocString("powers", "sword.title").GetFormattedText();
    public override string SpritePath => Path.Join(MainFile.ResPath, "images", "powers", "big", "sword_ability.png");

    public override AbilityCard BasicAttackCard => ModelDb.Card<OverheadSlash>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<SkyEnergySword>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<DrillStab>();
    
    // public override AbilityCard ForwardSkillCard => ModelDb.Card<Inhale>();

    public override AbilityCard UpCard =>  ModelDb.Card<UpwardSlash>();
    
    // public override AbilityCard UpSkillCard => ModelDb.Card<Hover>();

    public override AbilityCard DownCard => ModelDb.Card<SwordDive>();
    
    // public override AbilityCard DownSkillCard => ModelDb.Card<Crouch>();
}