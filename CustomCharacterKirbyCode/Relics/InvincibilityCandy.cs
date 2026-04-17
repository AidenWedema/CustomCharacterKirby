using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Relics;

public class InvincibilityCandy() : CustomCharacterKirbyRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override async Task BeforeCombatStart()
    {
        InvincibilityCandy relic = this;
        relic.Flash();
        await PowerCmd.Apply<BufferPower>(relic.Owner.Creature, 1, relic.Owner.Creature, null);
    }
}