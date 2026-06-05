using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class IceAbility : CopyAbility
{
    public override string DisplayName => new LocString("powers", "ice.title").GetFormattedText();
    public override string SpritePath => Path.Join(MainFile.ResPath, "images", "powers", "big", "ice_ability.png");

    public override AbilityCard BasicAttackCard => ModelDb.Card<IceBreath>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<IceBlock>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<IceStorm>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<IceSprinkle>();
    
    public override AbilityCard DownCard => ModelDb.Card<IceScatter>();
}