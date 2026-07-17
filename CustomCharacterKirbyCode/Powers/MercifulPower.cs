using CustomCharacterKirby.CustomCharacterKirbyCode.Encounters;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class MercifulPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override bool AllowNegative => false;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        MercifulPower power = this;
        if (side == CombatSide.Player) return;
        
        var owner = power.Owner;
        // Get all players
        var players = CombatState.Creatures.Where(creature => creature.Side == CombatSide.Player);
        var intendedAttack = owner.Monster.NextMove.Intents.OfType<AttackIntent>().FirstOrDefault();
        if (intendedAttack == null) return;
        
        var totalDamage = intendedAttack.GetTotalDamage(players, owner);
        // Foreach player, check if the intended attack will kill them
        foreach (var player in players)
        {
            if (player.CurrentHp + player.Block > totalDamage) continue;
            // If the attack would kill any player, end the fight
            if (owner.CombatState.Encounter is MetaKnightEventEncounter encounter)
                encounter.LostDuel = true;
            await CreatureCmd.Escape(power.Owner);
        }
    }
}