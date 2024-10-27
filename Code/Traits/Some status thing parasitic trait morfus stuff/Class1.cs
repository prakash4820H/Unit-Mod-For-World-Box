using HarmonyLib;
using Unit;

[HarmonyPatch(typeof(Actor))]
public class ExternalParasiteTraitInit
{
    public static void Init()
    {
        ActorTrait actorTrait = new ActorTrait
        {
            id = "external_parasite",
            path_icon = "ui/Icons/icon_external_parasite",
            group_id = TraitGroup.body,
            action_attack_target = ApplyStatusEffect
        };
        AssetManager.traits.add(actorTrait);
        LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Attacking an enemy applies a status effect that makes the actor invisible and drains the enemy's health.");
        PlayerConfig.unlockTrait("external_parasite");
    }

    public static bool ApplyStatusEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
    {
        Actor actor = pSelf as Actor;
        Actor target = pTarget as Actor;
        if (actor == null || target == null || !actor.hasTrait("external_parasite"))
        {
            return false;
        }

        target.addStatusEffect("external_parasite_effect", 10f);
        ExternalParasiteInvisibility.ApplyTo(actor);

        return true;
    }
}
