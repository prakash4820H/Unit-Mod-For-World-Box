using HarmonyLib;
using Unit;

[HarmonyPatch(typeof(CombatActionLibrary))]
public class InfinityCombatPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("attackMeleeAction")]
    public static bool PrefixAttackMeleeAction(AttackData pData)
    {
        Actor attacker = pData.initiator as Actor;
        Actor target = pData.target as Actor;

        if (target != null && target.hasTrait("infinity_trait"))
        {
            InfinityTrait infinityTrait = target.GetComponent<InfinityTrait>();
            if (infinityTrait != null && !infinityTrait.IsActorImmune(attacker))
            {
                return false; // Prevent the melee attack from proceeding if not immune
            }
        }

        // Allow attack if Mahoraga is immune to Infinity
        if (attacker != null && attacker.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = attacker.GetComponent<MahoragaEffect>();
            if (mahoragaEffect != null && mahoragaEffect.IsImmuneToInfinity())
            {
                return true; // Allow attack as Mahoraga is immune to Infinity
            }
        }

        return true; // Allow normal execution otherwise
    }

    [HarmonyPrefix]
    [HarmonyPatch("attackRangeAction")]
    public static bool PrefixAttackRangeAction(AttackData pData)
    {
        Actor attacker = pData.initiator as Actor;
        Actor target = pData.target as Actor;

        if (target != null && target.hasTrait("infinity_trait"))
        {
            InfinityTrait infinityTrait = target.GetComponent<InfinityTrait>();
            if (infinityTrait != null && !infinityTrait.IsActorImmune(attacker))
            {
                return false; // Prevent the ranged attack from proceeding if not immune
            }
        }

        // Allow attack if Mahoraga is immune to Infinity
        if (attacker != null && attacker.hasTrait("Mahoraga"))
        {
            MahoragaEffect mahoragaEffect = attacker.GetComponent<MahoragaEffect>();
            if (mahoragaEffect != null && mahoragaEffect.IsImmuneToInfinity())
            {
                return true; // Allow attack as Mahoraga is immune to Infinity
            }
        }

        return true; // Allow normal execution otherwise
    }
}
