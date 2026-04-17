using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

[Pool(typeof(CustomCharacterKirbyCardPool))]
public class DropAbility() : CopyEssenceCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override CopyAbility CopyAbility => ModelDb.Power<NormalAbility>();

    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DynamicVar("ProjectileStarGain", 3M)];

    protected override bool IsPlayable => !Owner.Creature.HasPower<NormalAbility>();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        DropAbility card = this;
        
        // Do normal CopyEssenceCard OnPlay
        base.OnPlay(choiceContext, play);
        
        // Gain projectile stars
        await PowerCmd.Apply<ProjectileStarPower>(card.Owner.Creature, DynamicVars["ProjectileStarGain"].BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ProjectileStarGain"].UpgradeValueBy(1M);
        EnergyCost.UpgradeBy(-1);
    }
}