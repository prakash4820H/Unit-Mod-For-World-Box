using HarmonyLib;

[HarmonyPatch(typeof(ActorBase))]
public static class MahoragaTraitAdaptationPatch
{
    [HarmonyPatch("addTrait", typeof(string), typeof(bool))]
    [HarmonyPrefix]
    public static bool Prefix_addTrait(ActorBase __instance, string pTrait, bool pRemoveOpposites)
    {
        if (__instance.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = __instance.GetComponent<MahoragaEffect>();
            if (mahoragaEffect != null && mahoragaEffect.IsTraitAdapted(pTrait))
            {
                //     Debug.Log($"Trait {pTrait} blocked due to adaptation");
                return false; // Skip the original method
            }
        }
        return true; // Execute the original method
    }
}
