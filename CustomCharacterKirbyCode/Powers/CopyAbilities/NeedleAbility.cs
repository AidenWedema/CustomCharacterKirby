using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class NeedleAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<NeedleAttack>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<WallStick>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<RollingNeedle>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<MegaNeedle>();
    
    public override AbilityCard DownCard => ModelDb.Card<NeedleBurst>();
}