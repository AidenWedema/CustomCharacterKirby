using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

[Pool(typeof(CustomCharacterKirbyCardPool))]
public class PoisonEssence() : CopyEssenceCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override CopyAbility CopyAbility => ModelDb.Power<PoisonAbility>();
}