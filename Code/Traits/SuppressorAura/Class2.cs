/*
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Actor), "u7_checkTraitEffects")]
public class Patch_u7_checkTraitEffects
{
    [HarmonyPrefix]
    public static bool Prefix(Actor __instance)
    {
        if (__instance.update_done || World.world.getWorldTimeElapsedSince(__instance._timestamp_trait_effects) < 1f)
        {
            return true; // Allow the original method to run
        }

        __instance._timestamp_trait_effects = World.world.getCurWorldTime();

        // Check if the actor is within range of a suppressor aura
        if (IsActorInSuppressorAuraRange(__instance))
        {
            SuppressSpecialEffects(__instance); // Suppress effects for this actor
            return false; // Skip further processing for this actor
        }

        return true; // Allow the original method to run if no suppression
    }

    // Helper function to check if the actor is in suppressor aura range
    private static bool IsActorInSuppressorAuraRange(Actor actor)
    {
        float radius = 10f;
        World.world.getObjectsInChunks(actor.currentTile, (int)radius, MapObjectType.Actor);

        foreach (BaseSimObject obj in World.world.temp_map_objects)
        {
            Actor nearbyActor = obj as Actor;
            if (nearbyActor != null && nearbyActor.hasTrait("suppressor_aura"))
            {
                return true; // Actor is within suppressor aura range
            }
        }
        return false;
    }

    // Disable special effects for this specific actor
    private static void SuppressSpecialEffects(Actor actor)
    {
        foreach (ActorTrait trait in actor.s_special_effect_traits)
        {
            trait.action_special_effect = null; // Permanently disable special effects
        }
    }
}
*/