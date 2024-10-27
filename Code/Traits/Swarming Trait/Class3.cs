using HarmonyLib;

[HarmonyPatch(typeof(CombatActionLibrary))]
public static class DisableAttacksPatch
{
    [HarmonyPatch("attackMeleeAction")]
    [HarmonyPrefix]
    public static bool Prefix_attackMeleeAction(AttackData pData)
    {
        Actor target = pData.target as Actor; // Extract the target from AttackData
        if (target != null && target.hasTrait("DefensiveShield"))
        {
            return false; // Skip the original method
        }
        return true; // Execute the original method
    }

    [HarmonyPatch("attackRangeAction")]
    [HarmonyPrefix]
    public static bool Prefix_attackRangeAction(AttackData pData)
    {
        Actor target = pData.target as Actor; // Extract the target from AttackData
        if (target != null && target.hasTrait("DefensiveShield"))
        {
            return false; // Skip the original method
        }
        return true; // Execute the original method
    }
}
