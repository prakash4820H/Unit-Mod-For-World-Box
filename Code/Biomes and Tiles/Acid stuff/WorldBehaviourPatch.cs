using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

[HarmonyPatch(typeof(WorldBehaviour))]
[HarmonyPatch("update")]
public static class WorldBehaviourPatch
{
    private static HashSet<WorldTile> _tilesToSpreadAcid => WorldBehaviourAcidTiles.GetTilesToSpread();
    private static HashSet<WorldTile> _tilesToSpreadBlood => WorldBehaviourBloodTiles.GetTilesToSpread();

    [HarmonyPrefix]
    public static bool PrefixUpdate(WorldBehaviour __instance, float pElapsed)
    {
        // Handle Acid Spreading Logic
        if (__instance._asset.id == "Acid_spreading" && !_tilesToSpreadAcid.Any(t => !t.Type.id.Equals("removed")))
        {
            return false;
        }

        // Handle Blood Spreading Logic
        if (__instance._asset.id == "Blood_spreading" && !_tilesToSpreadBlood.Any(t => !t.Type.id.Equals("removed")))
        {
            return false;
        }

        return true;
    }
}
