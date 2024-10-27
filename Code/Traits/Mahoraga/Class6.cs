using HarmonyLib;

[HarmonyPatch(typeof(Actor))]
public static class ActorOnTileChangePatch
{
    [HarmonyPatch("updatePos")]
    [HarmonyPostfix]
    public static void Postfix_updatePos(Actor __instance)
    {
        if (__instance.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = __instance.GetComponent<MahoragaEffect>();
            if (mahoragaEffect == null)
            {
                mahoragaEffect = __instance.gameObject.AddComponent<MahoragaEffect>();
                mahoragaEffect.Initialize();
            }
        }
    }
}
