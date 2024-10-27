using Unit;

public static class FreezeAndKillTraitInit
{
    public static void Init()
    {
        ActorTrait actorTrait = new ActorTrait();
        actorTrait.id = "freeze_and_kill";
        actorTrait.path_icon = "ui/Icons/icon_freeze_and_kill";
        actorTrait.group_id = UnitTraitGroup.Unit;
        actorTrait.action_special_effect = ApplyFreezeAndKill;
        AssetManager.traits.add(actorTrait);
        LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Freezes nearby enemies for 15 seconds, then kills them.");
        PlayerConfig.unlockTrait("freeze_and_kill");
    }

    public static bool ApplyFreezeAndKill(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        Actor actor = pTarget as Actor;
        if (actor == null || !actor.hasTrait("freeze_and_kill"))
        {
            return false;
        }
        FreezeAndKillTrait component = actor.gameObject.GetComponent<FreezeAndKillTrait>();
        if (component == null)
        {
            component = actor.gameObject.AddComponent<FreezeAndKillTrait>();
        }
        return component.ApplyEffect();
    }
}
