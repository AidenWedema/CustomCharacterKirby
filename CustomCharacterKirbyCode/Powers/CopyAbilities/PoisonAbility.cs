using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class PoisonAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<StickyToxin>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<ToxicSmog>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<ToxicSlide>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<ToxicTower>();
    
    public override AbilityCard DownCard => ModelDb.Card<StickyStrike>();
}