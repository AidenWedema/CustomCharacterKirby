using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Relics;

public class Keeby() : CustomCharacterKirbyRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        Keeby keeby = this;
        if (cardPlay.Card.Owner != keeby.Owner || !CombatManager.Instance.IsInProgress) return;

        CopyEssenceCard essence = cardPlay.Card as CopyEssenceCard;
        if (essence is null || essence.Rarity == CardRarity.Token) return;

        Creature target = keeby.Owner.RunState.Rng.CombatTargets.NextItem(keeby.Owner.Creature.CombatState.HittableEnemies);
        if (target == null) return;
        
        keeby.Flash();
        await CreatureCmd.Damage(context, target, keeby.DynamicVars.Damage, keeby.Owner.Creature);
    }
}
