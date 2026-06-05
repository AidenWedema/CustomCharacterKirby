using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class PoisonAbility : CopyAbility
{
    public override string DisplayName => new LocString("powers", "poison.title").GetFormattedText();
    public override string SpritePath => Path.Join(MainFile.ResPath, "images", "powers", "big", "poison_ability.png");

    public override AbilityCard BasicAttackCard => ModelDb.Card<StickyToxin>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<ToxicSmog>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<ToxicSlide>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<ToxicTower>();
    
    public override AbilityCard DownCard => ModelDb.Card<StickyStrike>();
}