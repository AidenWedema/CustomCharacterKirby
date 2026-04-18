using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class SleepAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<StrikeKirby>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<Inhale>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<StarSpit>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<Hover>();
    
    public override AbilityCard DownCard => ModelDb.Card<Crouch>();
}