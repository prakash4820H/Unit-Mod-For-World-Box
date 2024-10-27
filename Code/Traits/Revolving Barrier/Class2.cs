using Unit;

public class BarrierTraitInit
{
    public static void Init()
    {
        ActorTrait barrierTrait = new ActorTrait
        {
            id = "barrier_trait",
            path_icon = "ui/Icons/icon_barrier", // Path to the icon
            group_id = UnitTraitGroup.Unit,
            type = TraitType.Positive,
            action_special_effect = ApplyBarrierTrait
        };
        AssetManager.traits.add(barrierTrait);
        LocalizationUtility.addTraitToLocalizedLibrary(barrierTrait.id, "Creates an invisible barrier around the actor, pushing enemies out.");
        PlayerConfig.unlockTrait("barrier_trait");
    }

    public static bool ApplyBarrierTrait(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        Actor actor = pTarget as Actor;
        if (actor == null || !actor.hasTrait("barrier_trait"))
        {
            return false;
        }
        BarrierTrait component = actor.gameObject.GetComponent<BarrierTrait>();
        if (component == null)
        {
            component = actor.gameObject.AddComponent<BarrierTrait>();
        }
        return true;
    }
}
