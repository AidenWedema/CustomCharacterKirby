using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class MirrorAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<MirrorCut>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<ReflectGuard>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<MirrorBody>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<MirrorBodySky>();
    
    public override AbilityCard DownCard => ModelDb.Card<ReflectForce>();
}