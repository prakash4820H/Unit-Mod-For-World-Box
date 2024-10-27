using HarmonyLib;


namespace Unit
{
    [HarmonyPatch(typeof(Actor))]
    public class MahoragaTraitInit
    {
        public static void Init()
        {
            ActorTrait actorTrait = new ActorTrait
            {
                id = "Mahoraga",
                path_icon = "ui/Icons/mahoraga_icon1.png", // Path to the icon for the trait
                group_id = UnitTraitGroup.Unit,
                can_be_given = true,
                type = TraitType.Positive,
                action_special_effect = ApplyMahoraga
            };
            actorTrait.base_stats[S.health] = 500f;
            actorTrait.base_stats[S.knockback_reduction] = 0.5f;
            AssetManager.traits.add(actorTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Adapt to melee or ranged attacks, gaining immunity after consecutive hits. Also becomes permanently immune to status effects that last more than 3 seconds.");
            PlayerConfig.unlockTrait("Mahoraga");
        }

        public static bool ApplyMahoraga(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("Mahoraga"))
            {
                return false;
            }

            MahoragaEffect mahoragaEffect = actor.gameObject.GetComponent<MahoragaEffect>();
            if (mahoragaEffect == null)
            {
                mahoragaEffect = actor.gameObject.AddComponent<MahoragaEffect>();
                mahoragaEffect.Initialize(); // Ensure initialization is called
            }
            return true;
        }
    }
}