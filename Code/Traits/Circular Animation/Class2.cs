using HarmonyLib;

namespace Unit
{
    [HarmonyPatch(typeof(Actor))]
    public class CircularSpriteTraitInit
    {
        public static void Init()
        {
            ActorTrait actorTrait = new ActorTrait
            {
                id = "DarkSouls",
                path_icon = "ui/Icons/GG.png",  // Path to the icon for the trait
                group_id = UnitTraitGroup.Unit,
                can_be_given = true,
                action_special_effect = ApplyCircularSprite
            };
            AssetManager.traits.add(actorTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Makes multiple sprites revolve around the unit.");
            PlayerConfig.unlockTrait("DarkSouls");
        }

        public static bool ApplyCircularSprite(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("DarkSouls"))
            {
                return false;
            }

            CircularSpriteEffect circularSpriteEffect = actor.gameObject.GetComponent<CircularSpriteEffect>();
            if (circularSpriteEffect == null)
            {
                actor.addTrait("strong_minded");
                circularSpriteEffect = actor.gameObject.AddComponent<CircularSpriteEffect>();
            }
            return true;
        }
    }
}