using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class FriendHeartPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override bool ShouldPlayVfx => false;

    public override Creature ModifyUnblockedDamageTarget(Creature target, Decimal _, ValueProp props, Creature? __)
    {
        return target != this.Owner.PetOwner?.Creature || this.Owner.IsDead || !props.IsPoweredAttack() ? target : this.Owner;
    }

    public override bool ShouldAllowHitting(Creature creature) => creature.IsAlive;

    public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
    {
        return creature != this.Owner;
    }

    public override bool ShouldPowerBeRemovedAfterOwnerDeath() => false;
}