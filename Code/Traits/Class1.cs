public class EstablishKingdomTraitInit
{
    public static void Init()
    {
        ActorTrait actorTrait = new ActorTrait
        {
            id = "kingdom_builder",
            path_icon = "ui/Icons/iconKingdomBuilder",
            group_id = TraitGroup.special,
            action_special_effect = ApplyKingdomBuildingJob
        };
        AssetManager.traits.add(actorTrait);
        PlayerConfig.unlockTrait("kingdom_builder");
    }

    public static bool ApplyKingdomBuildingJob(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        Actor actor = pTarget as Actor;
        if (actor == null || !actor.hasTrait("kingdom_builder"))
        {
            return false;
        }

        // Access AiSystemActor and assign the job
        var aiSystem = new AiSystemActor(actor);
        aiSystem.setJob("establish_kingdom_job");  // Set the job using AiSystemActor

        return true;
    }
}
