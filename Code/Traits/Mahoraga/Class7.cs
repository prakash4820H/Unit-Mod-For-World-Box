using HarmonyLib;

[HarmonyPatch(typeof(Actor))]
public static class ActorGetHitPatch
{
    [HarmonyPatch("getHit")]
    [HarmonyPrefix]
    public static bool Prefix_getHit(Actor __instance, ref float pDamage, bool pFlash, AttackType pAttackType, BaseSimObject pAttacker, bool pSkipIfShake, bool pMetallicWeapon)
    {
        if (__instance.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = __instance.GetComponent<MahoragaEffect>();
            if (mahoragaEffect != null)
            {
                if (__instance.currentTile != null)
                {
                    if ((__instance.currentTile.Type == TileLibrary.mountains && (mahoragaEffect.IsImmuneToMountainDamage() || mahoragaEffect.IsPermanentlyImmuneToMountainDamage())) ||
                       (__instance.currentTile.Type == TileLibrary.lava0 && (mahoragaEffect.IsImmuneToLavaDamage() || mahoragaEffect.IsPermanentlyImmuneToLavaDamage())) ||
                       (__instance.currentTile.Type == TileLibrary.lava1 && (mahoragaEffect.IsImmuneToLavaDamage() || mahoragaEffect.IsPermanentlyImmuneToLavaDamage())) ||
                       (__instance.currentTile.Type == TileLibrary.lava2 && (mahoragaEffect.IsImmuneToLavaDamage() || mahoragaEffect.IsPermanentlyImmuneToLavaDamage())) ||
                       (__instance.currentTile.Type == TileLibrary.lava3 && (mahoragaEffect.IsImmuneToLavaDamage() || mahoragaEffect.IsPermanentlyImmuneToLavaDamage())))
                    {
                        // Skip the damage if the actor is immune to mountain or lava damage
                        return false;
                    }
                }
            }
        }
        return true; // Proceed with the original damage application
    }
}
