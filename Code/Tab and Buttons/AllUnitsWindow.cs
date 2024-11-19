using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMS.Utils;

namespace Unit
{
    public class AllUnitsWindow : WindowListBase<PrefabUnitElement, Actor>
    {
        private List<Actor> _temp_list_actor = new List<Actor>();

        public static void init()
        {
            var window = Windows.CreateNewWindow("AllUnitsWindow", "Units");
            Debug.Log("AllUnitsWindow initialized.");
        }

        public override void create()
        {
            base.create();

            // Add sorting buttons
            addSortButton("ui/Icons/iconAge", "Sort by Age", () => current_sort = sortByAge);
            addSortButton("ui/Icons/iconKills", "Sort by Kills", () => current_sort = sortByKills);

            // Default sorting
            current_sort = sortByAge;
        }

        public override List<Actor> getObjects()
        {
            _temp_list_actor.Clear();

            foreach (Actor unit in World.world.units)
            {
                if (unit.isAlive())
                {
                    _temp_list_actor.Add(unit);
                }
            }
            return _temp_list_actor;
        }

        private void Update()
        {
            clear();
            show();
        }

        private static int sortByAge(Actor a1, Actor a2) => a2.getAge().CompareTo(a1.getAge());
        private static int sortByKills(Actor a1, Actor a2) => a2.data.kills.CompareTo(a1.data.kills);
    }
}
