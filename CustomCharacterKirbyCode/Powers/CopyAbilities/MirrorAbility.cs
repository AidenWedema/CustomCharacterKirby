using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class MirrorAbility : CopyAbility
{
    public override string DisplayName => new LocString("powers", "mirror.title").GetFormattedText();
    public override string SpritePath => Path.Join(MainFile.ResPath, "images", "powers", "big", "mirror_ability.png");

    public override AbilityCard BasicAttackCard => ModelDb.Card<MirrorCut>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<ReflectGuard>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<MirrorBody>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<MirrorBodySky>();
    
    public override AbilityCard DownCard => ModelDb.Card<ReflectForce>();
}