using HarmonyLib;

namespace Unit
{
    [HarmonyPatch(typeof(Actor))]
    public class DefensiveShieldTraitInit
    {
        public static void Init()
        {
            ActorTrait actorTrait = new ActorTrait
            {
                id = "DefensiveShield",
                path_icon = "ui/Icons/shield_icon.png", // Path to the icon for the trait
                group_id = UnitTraitGroup.Unit,
                can_be_given = true,
                action_special_effect = ApplyDefensiveShield
            };
            AssetManager.traits.add(actorTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Adds a defensive shield that prevents attacks.");
            PlayerConfig.unlockTrait("DefensiveShield");
        }

        public static bool ApplyDefensiveShield(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("DefensiveShield"))
            {
                return false;
            }

            DefensiveShieldEffect shieldEffect = actor.gameObject.GetComponent<DefensiveShieldEffect>();
            if (shieldEffect == null)
            {
                shieldEffect = actor.gameObject.AddComponent<DefensiveShieldEffect>();
                shieldEffect.Initialize(); // Ensure initialization is called
            }
            return true;
        }
    }
}