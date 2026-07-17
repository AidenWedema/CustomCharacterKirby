using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Relics;

public class InvincibilityCandy() : CustomCharacterKirbyRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        InvincibilityCandy relic = this;
        if (!(room is CombatRoom))
            return;
        relic.Flash();
        await PowerCmd.Apply<BufferPower>(new ThrowingPlayerChoiceContext(), relic.Owner.Creature, 1, relic.Owner.Creature, null);
    }
}