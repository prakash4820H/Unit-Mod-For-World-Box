using HarmonyLib;

[HarmonyPatch(typeof(Actor))]
public static class MahoragaStatusEffectPatch
{
    [HarmonyPatch("addStatusEffect")]
    [HarmonyPrefix]
    public static bool Prefix_addStatusEffect(Actor __instance, string pID, float pOverrideTimer)
    {
        if (__instance.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = __instance.GetComponent<MahoragaEffect>();
            if (mahoragaEffect != null)
            {
                if (mahoragaEffect.IsImmuneToStatusEffect(pID))
                {
                    // Debug.Log($"Status effect {pID} blocked due to immunity"); // Debug log
                    return false; // Skip the original method
                }
                else
                {
                    mahoragaEffect.RegisterStatusEffect(pID); // Register status effect
                }
            }
        }
        return true; // Execute the original method
    }
}
