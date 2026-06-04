using HarmonyLib;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Combat;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Helpers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Patches;

[HarmonyPatch(typeof(NCombatUi), nameof(NCombatUi.Activate))]
public static class NCombatUiActivatePatch
{
    static void Postfix(NCombatUi __instance, CombatState state)
    {
        Player? me = LocalContext.GetMe(state);
        if (me == null) return;

        var display = NCopyAbilityDisplay.Create(me);
        __instance.EnergyCounterContainer.AddChildSafely(display);
    }
}