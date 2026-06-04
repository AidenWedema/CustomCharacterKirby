using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class LeafAbility : CopyAbility
{
    public override string DisplayName => new LocString("powers", "leaf").GetFormattedText();
    public override string SpritePath => Path.Join(MainFile.ResPath, "images", "powers", "big", "leaf_ability.png");

    public override AbilityCard BasicAttackCard => ModelDb.Card<LeafAttack>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<LeafHide>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<LeafScatter>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<LeafUppercut>();
    
    public override AbilityCard DownCard => ModelDb.Card<LeafRain>();
}