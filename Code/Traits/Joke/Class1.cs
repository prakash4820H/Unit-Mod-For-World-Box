using Unit;

public static class WhiteScreenTraitInit
{
    public static void Init()
    {
        ActorTrait actorTrait = new ActorTrait();
        actorTrait.id = "idk";
        actorTrait.path_icon = "ui/Icons/icon_white_screen";
        actorTrait.group_id = UnitTraitGroup.Unit;
        actorTrait.action_special_effect = ApplyWhiteScreen;
        AssetManager.traits.add(actorTrait);
        LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Never Press the key 'O' ");
        PlayerConfig.unlockTrait("idk");
    }

    private static bool ApplyWhiteScreen(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        Actor actor = pTarget as Actor;
        if (actor == null || !actor.hasTrait("idk"))
        {
            return false;
        }
        WhiteScreenTrait component = actor.gameObject.GetComponent<WhiteScreenTrait>();
        if (component == null)
        {
            component = actor.gameObject.AddComponent<WhiteScreenTrait>();
            component.actor = actor;
        }
        return true;
    }
}
