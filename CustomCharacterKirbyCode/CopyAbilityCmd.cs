using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;

namespace CustomCharacterKirby.CustomCharacterKirbyCode;

public static class CopyAbilityCmd
{
    private static readonly Dictionary<PlayerCombatState, CopyAbility> Abilities = [];

    public static event Action<PlayerCombatState, CopyAbility?, CopyAbility?>? AbilityChanged;

    public static CopyAbility? GetCurrent(PlayerCombatState combatState)
    {
        return Abilities.GetValueOrDefault(combatState);
    }

    public static void SetCurrent(PlayerCombatState? combatState, CopyAbility? ability)
    {
        ArgumentNullException.ThrowIfNull(combatState);
        
        var oldAbility = GetCurrent(combatState);

        if (ReferenceEquals(oldAbility, ability))
            return;

        if (ability == null)
            Abilities.Remove(combatState);
        else
            Abilities[combatState] = ability;
        
        AbilityChanged?.Invoke(
            combatState,
            oldAbility,
            ability
        );
    }

    public static void Clear(PlayerCombatState combatState)
    {
        SetCurrent(combatState, null);
    }
}