using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using CustomCharacterKirby.CustomCharacterKirbyCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Relics;

public class SquishySkin() : CustomCharacterKirbyRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new HpLossVar("HpLossReduction", 2M)];
    
    public override Decimal ModifyHpLostAfterOsty(Creature target, Decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        return target != this.Owner.Creature ? amount : Math.Max(0M, amount - this.DynamicVars["HpLossReduction"].BaseValue);
    }

    public override Task AfterModifyingHpLostAfterOsty()
    {
        this.Flash();
        return Task.CompletedTask;
    }
    
    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        SquishySkin squishySkin = this;
        if (!(room is CombatRoom))
            return;
        // In combat, silently apply the normal ability
        NormalAbility normalAbility = await PowerCmd.Apply<NormalAbility>(squishySkin.Owner.Creature, 1, squishySkin.Owner.Creature, (CardModel) null);
    }
}