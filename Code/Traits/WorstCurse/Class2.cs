using HarmonyLib;
using System;

namespace Unit
{
    public class MainUnitDeathTraitInit
    {
        public static void Init()
        {
            ActorTrait mainUnitDeathTrait = new ActorTrait
            {
                id = "main_unit_death_trait",
                path_icon = "ui/Icons/icon_main_unit_death",
                group_id = UnitTraitGroup.Unit
            };
            AssetManager.traits.add(mainUnitDeathTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(mainUnitDeathTrait.id, "If the main unit gets killed, the enemy who killed the main unit gets all his stats halved.");
            PlayerConfig.unlockTrait("main_unit_death_trait");

            Harmony.CreateAndPatchAll(typeof(MainUnitDeathTraitInit));
        }

        [HarmonyPatch(typeof(Actor), "getHit")]
        [HarmonyPostfix]
        public static void OnGetHitPatch(Actor __instance, float pDamage, bool pFlash, AttackType pAttackType, BaseSimObject pAttacker, bool pSkipIfShake, bool pMetallicWeapon)
        {
            if (__instance.data.health <= 0)
            {
                __instance.DieWithTrait(pAttacker as Actor);
            }
        }

        public static T GetFieldValue<T>(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (field == null)
            {
                throw new Exception($"Field '{fieldName}' not found in type '{obj.GetType()}'");
            }
            return (T)field.GetValue(obj);
        }
    }
}
