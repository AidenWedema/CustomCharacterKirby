using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class SwordAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<OverheadSlash>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<SkyEnergySword>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<DrillStab>();
    
    // public override AbilityCard ForwardSkillCard => ModelDb.Card<Inhale>();

    public override AbilityCard UpCard =>  ModelDb.Card<UpwardSlash>();
    
    // public override AbilityCard UpSkillCard => ModelDb.Card<Hover>();

    public override AbilityCard DownCard => ModelDb.Card<SwordDive>();
    
    // public override AbilityCard DownSkillCard => ModelDb.Card<Crouch>();
}