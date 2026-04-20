using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class CracklerPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool AllowNegative => false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4, ValueProp.Move)];

    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        CracklerPower power = this;
        if (side != power.Owner.Side) return;
        
        power.Flash();
        var enemies = power.CombatState.HittableEnemies;
        var r = Rng.Chaotic.NextInt(0, enemies.Count);
        var target = enemies[r];
        await CreatureCmd.Damage(choiceContext, target, power.DynamicVars.Damage, power.Owner);
        
        await PowerCmd.Decrement(power);
    }
    

    public void SetDamage(decimal newDamage)
    {
        this.AssertMutable();
        this.DynamicVars.Damage.BaseValue = newDamage;
    }
}