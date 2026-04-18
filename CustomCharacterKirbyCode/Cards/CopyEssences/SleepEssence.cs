using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class SleepEssence() : CopyEssenceCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override CopyAbility CopyAbility => ModelDb.Power<SleepAbility>();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        // Do the normal copy ability switch
        await base.OnPlay(choiceContext, play);
        
        // Apply the Asleep power
        await PowerCmd.Apply<SleepPower>(Owner.Creature, 1, Owner.Creature, this);
        
        // End the turn
        PlayerCmd.EndTurn(Owner, false);
    }
}