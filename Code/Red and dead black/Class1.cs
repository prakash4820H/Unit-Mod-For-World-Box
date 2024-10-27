using HarmonyLib;

[HarmonyPatch(typeof(Actor), "getHit")]
public static class Patch_Actor_getHit
{

    static bool Prefix(Actor __instance, float pDamage, bool pFlash = true, AttackType pAttackType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true, bool pMetallicWeapon = false)
    {
        if (__instance.asset.id == "DragonSlayer")
        {
            __instance.data.health -= (int)pDamage;
            __instance.timer_action = 0.002f;
            if (__instance.data.health <= 0)
            {
                __instance.killHimself();
            }
            return false;
        }
        return true;
    }
}
