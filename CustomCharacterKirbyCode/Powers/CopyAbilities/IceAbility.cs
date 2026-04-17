using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class IceAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<IceBreath>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<IceBlock>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<IceStorm>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<IceSprinkle>();
    
    public override AbilityCard DownCard => ModelDb.Card<IceScatter>();
}