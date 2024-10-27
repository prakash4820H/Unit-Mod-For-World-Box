using HarmonyLib;

namespace Unit
{
    [HarmonyPatch(typeof(Actor))]
    public class ThiefActorTraitInit
    {
        public static void Init()
        {
            ActorTrait thiefTrait = new ActorTrait
            {
                id = "thief_actor_trait",
                path_icon = "ui/Icons/icon_steal_items",
                group_id = UnitTraitGroup.Unit,
                action_special_effect = new WorldAction(ApplyTrait)
            };

            AssetManager.traits.add(thiefTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(thiefTrait.id, "Steals the strongest equipment on the map and gives it to the unit.");
            PlayerConfig.unlockTrait("thief_actor_trait");
        }

        public static bool ApplyTrait(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("thief_actor_trait"))
                return false;

            ThiefActorTrait component = actor.gameObject.GetComponent<ThiefActorTrait>();
            if (component == null)
            {
                component = actor.gameObject.AddComponent<ThiefActorTrait>();
            }
            return true;
        }
    }
}
