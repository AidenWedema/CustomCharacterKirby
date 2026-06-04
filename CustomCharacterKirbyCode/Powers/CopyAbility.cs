using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public abstract class CopyAbility : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public abstract string DisplayName { get; }
    public abstract string SpritePath { get; }

    public abstract AbilityCard BasicAttackCard { get; }
    public abstract AbilityCard BasicSkillCard { get; }
    
    public abstract AbilityCard ForwardCard { get; }
    
    public  abstract AbilityCard UpCard { get; }
    
    public abstract AbilityCard DownCard { get; }
}