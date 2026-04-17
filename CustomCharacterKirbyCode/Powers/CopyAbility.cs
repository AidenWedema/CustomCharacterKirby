using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public abstract class CopyAbility : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.None;

    public abstract AbilityCard BasicAttackCard { get; }
    public abstract AbilityCard BasicSkillCard { get; }
    
    public abstract AbilityCard ForwardCard { get; }
    
    public  abstract AbilityCard UpCard { get; }
    
    public abstract AbilityCard DownCard { get; }
}