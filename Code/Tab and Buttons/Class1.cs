using System.Collections.Generic;
using UnityEngine;

public class WindowAliveUnits : WindowListBase<PrefabUnitElement, Actor>
{
    private List<Actor> _temp_list_actor = new List<Actor>();
    private const string SCREEN_ID = "window_alive_units";

    // Updated access modifier and delegate usage for compatibility
    public override void create()
    {
        base.create();

        addSortButton("ui/Icons/iconAge", "sort_by_age", delegate { current_sort = sortByAge; });
        addSortButton("ui/Icons/iconLevels", "sort_by_level", delegate { current_sort = sortByLevel; });
        addSortButton("ui/Icons/iconSkulls", "sort_by_kills", delegate { current_sort = sortByKills; });
        addSortButton("ui/Icons/iconKingdom", "sort_by_kingdom", delegate { current_sort = sortByKingdom; });
    }

    public override List<Actor> getObjects()
    {
        _temp_list_actor.Clear();
        foreach (Actor unit in World.world.units)
        {
            if (unit.isAlive())  // Include only alive units
            {
                _temp_list_actor.Add(unit);
            }
        }
        return _temp_list_actor;
    }

    // Sorting functions using nested data attributes
    public static int sortByAge(Actor pActor1, Actor pActor2)
    {
        return pActor1.data.age.CompareTo(pActor2.data.age);
    }

    public static int sortByLevel(Actor pActor1, Actor pActor2)
    {
        return pActor1.data.level.CompareTo(pActor2.data.level);
    }

    public static int sortByKills(Actor pActor1, Actor pActor2)
    {
        return pActor1.data.kills.CompareTo(pActor2.data.kills);
    }

    public static int sortByKingdom(Actor pActor1, Actor pActor2)
    {
        return pActor1.kingdom.CompareTo(pActor2.kingdom);
    }
}
