using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class LeafAbility : CopyAbility
{
    public override AbilityCard BasicAttackCard => ModelDb.Card<LeafAttack>();

    public override AbilityCard BasicSkillCard => ModelDb.Card<LeafHide>();
    
    public override AbilityCard ForwardCard => ModelDb.Card<LeafScatter>();
    
    public override AbilityCard UpCard =>  ModelDb.Card<LeafUppercut>();
    
    public override AbilityCard DownCard => ModelDb.Card<LeafRain>();
}