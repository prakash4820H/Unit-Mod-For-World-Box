using HarmonyLib;
using System;

[HarmonyPatch(typeof(Actor), "addExperience")]
public static class ActorLevelCapPatch
{
    [HarmonyPrefix]
    public static bool AddExperiencePrefix(Actor __instance, ref int pValue)
    {
        if (!__instance.asset.canLevelUp || !__instance.isAlive())
        {
            return false; 
        }

        int maxLevel = 100;
        int baseMaxLevel = 10;
        Culture culture = __instance.getCulture();
        if (culture != null)
        {
            baseMaxLevel += culture.getMaxLevelBonus();
        }
        baseMaxLevel += (int)__instance.kingdom.stats.bonus_max_unit_level.value;
        maxLevel = Math.Max(maxLevel, baseMaxLevel);
        if (__instance.data.level >= maxLevel)
        {
            return false;
        }
        __instance.data.experience += pValue;
        while (__instance.data.experience >= __instance.getExpToLevelup())
        {
            __instance.data.experience -= __instance.getExpToLevelup();
            __instance.data.level++;
            if (__instance.data.level >= maxLevel)
            {
                __instance.data.experience = __instance.getExpToLevelup();
                break;
            }
            if (__instance.data.level == maxLevel && __instance.asset.flag_turtle)
            {
                AchievementLibrary.achievementNinjaTurtle.check();
            }
            __instance.setStatsDirty();
            __instance.event_full_heal = true;
        }

        return false; 
    }
}
