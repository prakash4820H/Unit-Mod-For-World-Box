public static class ExternalParasiteStatusEffect
{
    public static void Init()
    {
        StatusEffect statusEffect = new StatusEffect
        {
            id = "external_parasite_effect",
            name = "External Parasite",
            description = "Drains 3 health per second",
            duration = 10f,
            action = DrainHealth,
            action_interval = 1f,
            path_icon = "ui/Icons/iconExternalParasite"
        };
        AssetManager.status.add(statusEffect);
    }

    public static bool DrainHealth(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget is Actor targetActor)
        {
            targetActor.getHit(3f, pFlash: true, AttackType.Poison, pAttacker: null);
        }
        return true;
    }
}
