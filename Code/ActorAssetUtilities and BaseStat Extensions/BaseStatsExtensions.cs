using UnityEngine;
public static class BaseStatsExtension
{
    public static void CopyFrom(this BaseStats destination, BaseStats source)
    {
        if (source == null || destination == null)
        {
            Debug.LogError("Source or destination BaseStats is null");
            return;
        }
        destination[S.damage] = source[S.damage];
        destination[S.health] = source[S.health];
        destination[S.speed] = source[S.speed];
        destination[S.accuracy] = source[S.accuracy];
        destination[S.attack_speed] = source[S.attack_speed];
        destination[S.knockback] = source[S.knockback];
        destination[S.targets] = source[S.targets];
        destination[S.area_of_effect] = source[S.area_of_effect];
        destination[S.size] = source[S.size];
        destination[S.range] = source[S.range];
        destination[S.critical_damage_multiplier] = source[S.critical_damage_multiplier];
        destination[S.scale] = source[S.scale];
        destination[S.mod_supply_timer] = source[S.mod_supply_timer];
        destination[S.fertility] = source[S.fertility];
        destination[S.armor] = source[S.armor];
        destination[S.dodge] = source[S.dodge];
        destination[S.diplomacy] = source[S.diplomacy];
        destination[S.max_age] = source[S.max_age];
        destination[S.max_children] = source[S.max_children];
        destination[S.knockback_reduction] = source[S.knockback_reduction];
    }

    public static BaseStats Clone(this BaseStats source)
    {
        if (source == null)
        {
            Debug.LogError("Source BaseStats is null");
            return null;
        }
        BaseStats clone = new BaseStats();
        clone.CopyFrom(source);
        return clone;
    }
}

