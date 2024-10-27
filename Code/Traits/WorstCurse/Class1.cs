public static class ActorExtensions
{
    public static void DieWithTrait(this Actor actor, Actor killer)
    {
        if (actor.hasTrait("main_unit_death_trait"))
        {
            HalveKillerStats(killer);
        }
    }

    private static void HalveKillerStats(Actor killer)
    {
        if (killer == null) return;

        killer.stats[S.damage] /= 2;
        killer.stats[S.health] /= 2;
        killer.stats[S.speed] /= 2;
        killer.stats[S.accuracy] /= 2;
        killer.stats[S.attack_speed] /= 2;
        killer.stats[S.knockback] /= 2;
        killer.stats[S.targets] /= 2;
        killer.stats[S.area_of_effect] /= 2;
        killer.stats[S.size] /= 2;
        killer.stats[S.range] /= 2;
        killer.stats[S.critical_damage_multiplier] /= 2;
        killer.stats[S.scale] /= 2;
        killer.stats[S.mod_supply_timer] /= 2;
        killer.stats[S.fertility] /= 2;
        killer.stats[S.armor] /= 2;
        killer.stats[S.dodge] /= 2;
        killer.stats[S.diplomacy] /= 2;
        killer.stats[S.max_age] /= 2;
        killer.stats[S.max_children] /= 2;
        killer.stats[S.knockback_reduction] /= 2;
        killer.updateStats();
    }
}
