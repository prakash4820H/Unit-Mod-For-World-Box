namespace Unit
{
    public class FrozenBarrierTraitInit
    {
        public static void Init()
        {
            ActorTrait frozenBarrierTrait = new ActorTrait
            {
                id = "frozen_barrier_trait",
                path_icon = "ui/Icons/icon_frozen_barrier_trait", // Path to the trait icon
                group_id = UnitTraitGroup.Unit,
                action_special_effect = ApplyFrozenBarrierEffect
            };
            AssetManager.traits.add(frozenBarrierTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(frozenBarrierTrait.id, "Applies a frozen barrier effect to an actor, sucking in and killing nearby enemies.");
            PlayerConfig.unlockTrait("frozen_barrier_trait");
        }

        public static bool ApplyFrozenBarrierEffect(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("frozen_barrier_trait"))
            {
                return false;
            }
            FrozenBarrierTrait frozenBarrierTrait = actor.gameObject.GetComponent<FrozenBarrierTrait>();
            if (frozenBarrierTrait == null)
            {
                frozenBarrierTrait = actor.gameObject.AddComponent<FrozenBarrierTrait>();
            }
            return true;
        }
    }
}