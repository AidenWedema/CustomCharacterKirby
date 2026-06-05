using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class BeamAbility : CopyAbility
{
    public override string DisplayName => new LocString("powers", "beam.title").GetFormattedText();
    public override string SpritePath => Path.Join(MainFile.ResPath, "images", "powers", "big", "beam_ability.png");
    
    public override AbilityCard BasicAttackCard => ModelDb.Card<BeamWhip>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<CaptureBeam>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<CycleBeam>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<BeamBlast>();
    
    public override AbilityCard DownCard => ModelDb.Card<WaveBeam>();
}