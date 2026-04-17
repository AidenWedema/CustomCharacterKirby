using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards.CopyAbilities.Sword;

public class SkyEnergySword() : AbilityCard (1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    protected override AbilityType abilityType => AbilityType.BasicSkill;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        SkyEnergySword cardSource = this;
        await PowerCmd.Apply<SkyEnergySwordPower>(cardSource.Owner.Creature, 1M, cardSource.Owner.Creature, (CardModel) cardSource);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}