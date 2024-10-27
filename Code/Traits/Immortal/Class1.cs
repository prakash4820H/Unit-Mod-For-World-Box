using HarmonyLib;
using UnityEngine;

namespace Unit
{
    [HarmonyPatch]
    public static class CityPatches
    {
        [HarmonyPatch(typeof(City), "spawnPopPoint")]
        [HarmonyPrefix]
        public static bool SpawnPopPointPrefix(ref Actor __result, ActorData pData, WorldTile pTile)
        {
            if (pData == null)
            {
                Debug.LogError("spawnPopPoint: pData is null");
                __result = null;
                return false; // Skip original method
            }

            if (pTile == null)
            {
                Debug.LogError("spawnPopPoint: pTile is null");
                __result = null;
                return false; // Skip original method
            }

            if (string.IsNullOrEmpty(pData.id))
            {
                pData.id = World.world.mapStats.getNextId("unit");
            }

            ActorAsset originalActorAsset = AssetManager.actor_library.get(pData.asset_id);
            if (originalActorAsset == null)
            {
                Debug.LogError($"spawnPopPoint: originalActorAsset is null for asset_id {pData.asset_id}");
                __result = null;
                return false; // Skip original method
            }

            ActorAsset actorAsset = originalActorAsset;
            if ((float)pData.getAge() < (float)actorAsset.years_to_grow_to_adult)
            {
                pData.asset_id = pData.asset_id.Replace("unit_", "baby_");
                pData.profession = UnitProfession.Baby;
                actorAsset = AssetManager.actor_library.get(pData.asset_id);

                if (actorAsset == null)
                {
                    Debug.LogError($"spawnPopPoint: baby actorAsset is null for asset_id {pData.asset_id}");
                    pData.asset_id = originalActorAsset.id; // Revert to the original asset ID
                    actorAsset = originalActorAsset;
                }
            }

            for (int i = 0; i < actorAsset.traits.Count; i++)
            {
                string item = actorAsset.traits[i];
                if (!pData.traits.Contains(item))
                {
                    pData.traits.Add(item);
                }
            }

            return true; // Continue with the original method
        }


        [HarmonyPatch(typeof(ActorData), "updateAge", new[] { typeof(string) })]
        [HarmonyPrefix]
        public static bool UpdateAgePrefix(ActorData __instance, ref bool __result, string pAssetID)
        {
            if (__instance == null)
            {
                Debug.LogError("updateAge: ActorData instance is null");
                __result = false;
                return false; // Skip original method
            }

            if (string.IsNullOrEmpty(pAssetID))
            {
                Debug.LogError("updateAge: pAssetID is null or empty");
                __result = false;
                return false; // Skip original method
            }

            ActorAsset actorAsset = AssetManager.actor_library.get(pAssetID);
            if (actorAsset == null)
            {
                Debug.LogError($"updateAge: actorAsset is null for pAssetID {pAssetID}");
                __result = false;
                return false; // Skip original method
            }

            return true; // Continue with the original method
        }
    }
}
