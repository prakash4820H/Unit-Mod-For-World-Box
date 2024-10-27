using Unit;

public static class ShadowFriendTraitInit
{
    public static void Init()
    {
        ActorTrait trait = new ActorTrait
        {
            id = "shadow_friend_trait",
            path_icon = "ui/Icons/shadow1.png",
            group_id = UnitTraitGroup.Unit,
            action_special_effect = ApplyTrait
        };
        AssetManager.traits.add(trait);
        LocalizationUtility.addTraitToLocalizedLibrary(trait.id, "Spawns a shadow friend after killing an enemy.");
        PlayerConfig.unlockTrait("shadow_friend_trait");
    }

    public static bool ApplyTrait(BaseSimObject pSelf, WorldTile pTile = null)
    {
        Actor actor = pSelf as Actor;
        if (actor == null || !actor.hasTrait("shadow_friend_trait"))
        {
            return false;
        }
        ShadowFriendTrait component = actor.gameObject.GetComponent<ShadowFriendTrait>();
        if (component == null)
        {
            actor.addTrait("strong_minded");
            component = actor.gameObject.AddComponent<ShadowFriendTrait>();
        }
        return true;
    }

}