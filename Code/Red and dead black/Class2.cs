using HarmonyLib;

[HarmonyPatch(typeof(ActorBase), "updateDeadBlackAnimation")]
public static class Patch_UpdateDeadBlackAnimation
{
    static bool Prefix(Actor __instance, float pElapsed)
    {
        // If the actor's asset is "DragonSlayer", skip the blackening process
        if (__instance.asset.id == "DragonSlayer" || __instance.asset.id == "SoulStealer")
        {
            return false;  // Skip the original method
        }
        return true;  // Continue with the original method for other actors
    }
}