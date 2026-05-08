using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class BeamAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<BeamWhip>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<CaptureBeam>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<CycleBeam>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<BeamBlast>();
    
    public override AbilityCard DownCard => ModelDb.Card<WaveBeam>();
}