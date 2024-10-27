using ai.behaviours;
using HarmonyLib;
using UnityEngine;

namespace Unit
{
    public static class AdditionalPatches
    {
        [HarmonyPatch(typeof(CityBehProduceUnit), "produceNewCitizen")]
        [HarmonyPrefix]
        public static bool ProduceNewCitizenPrefix(ref bool __result, Building pBuilding, City pCity)
        {
            if (pCity == null)
            {
                Debug.LogError("produceNewCitizen: pCity is null");
                __result = false;
                return false;
            }

            if (pBuilding == null)
            {
                Debug.LogError("produceNewCitizen: pBuilding is null");
                __result = false;
                return false;
            }

            return true; // Continue with the original method
        }

        [HarmonyPatch(typeof(CityBehCheckPopPoints), "execute")]
        [HarmonyPostfix]
        public static void CityBehCheckPopPointsPostfix(City pCity)
        {
            if (pCity == null)
            {
                Debug.LogError("CityBehCheckPopPoints: pCity is null");
            }
        }
    }
}
