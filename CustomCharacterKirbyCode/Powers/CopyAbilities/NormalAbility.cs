using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class NormalAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<StrikeKirby>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<Inhale>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<StarSpit>();
    
    // public override AbilityCard ForwardSkillCard => ModelDb.Card<Inhale>();

    public override AbilityCard UpCard =>  ModelDb.Card<Hover>();
    
    // public override AbilityCard UpSkillCard => ModelDb.Card<Jump>();

    public override AbilityCard DownCard => ModelDb.Card<Crouch>();
    
    // public override AbilityCard DownSkillCard => ModelDb.Card<Slide>();
}