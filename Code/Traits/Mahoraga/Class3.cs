using HarmonyLib;

[HarmonyPatch(typeof(CombatActionLibrary))]
public static class MahoragaCombatPatch
{
    [HarmonyPatch("attackMeleeAction")]
    [HarmonyPrefix]
    public static bool Prefix_attackMeleeAction(AttackData pData)
    {
        Actor target = pData.target as Actor; // Extract the target from AttackData
        if (target != null && target.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = target.GetComponent<MahoragaEffect>();
            if (mahoragaEffect != null)
            {
                if (mahoragaEffect.IsImmuneToMelee())
                {
                    //      Debug.Log("Melee attack blocked due to immunity"); // Debug log
                    return false; // Skip the original method
                }
                else
                {
                    mahoragaEffect.RegisterHit(true); // Register melee hit
                }
            }
        }
        return true; // Execute the original method
    }

    [HarmonyPatch("attackRangeAction")]
    [HarmonyPrefix]
    public static bool Prefix_attackRangeAction(AttackData pData)
    {
        Actor target = pData.target as Actor; // Extract the target from AttackData
        if (target != null && target.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = target.GetComponent<MahoragaEffect>();
            if (mahoragaEffect != null)
            {
                if (mahoragaEffect.IsImmuneToRanged())
                {
                    //        Debug.Log("Ranged attack blocked due to immunity"); // Debug log
                    return false; // Skip the original method
                }
                else
                {
                    mahoragaEffect.RegisterHit(false); // Register ranged hit
                }
            }
        }
        return true; // Execute the original method
    }
}
