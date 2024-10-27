/*
using HarmonyLib;
using Unit;

[HarmonyPatch(typeof(BaseSimObject), "updateStats")]
public static class PatchUpdateStats
{
    public static void Prefix(BaseSimObject __instance)
    {
        Actor actor = __instance as Actor;
        if (actor != null && actor.hasTrait("stat_absorb_trait"))
        {
            // Prevent resetting stats if the actor has the stat_absorb_trait
            StatAbsorbTraitHelper.SkipStatsReset = true;
        }
    }

    public static void Postfix(BaseSimObject __instance)
    {
        StatAbsorbTraitHelper.SkipStatsReset = false;
    }

    public static class StatAbsorbTraitHelper
    {
        public static bool SkipStatsReset { get; set; } = false;
    }

}
*/