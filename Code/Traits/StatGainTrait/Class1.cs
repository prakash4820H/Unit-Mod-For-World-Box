/*
 using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;

public class AbsorbStatsTrait : MonoBehaviour
{
    private Actor actor;

    void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
            return;
        }
    }

    void Update()
    {
        CheckForKills();
    }

    private void CheckForKills()
    {
        foreach (Actor enemy in MapBox.instance.units.getSimpleList())
        {
            if (IsEnemy(actor, enemy) && enemy.data.health <= 0 && !enemy.data.traits.Contains("absorbed"))
            {
                AbsorbStats(enemy);
                enemy.data.traits.Add("absorbed");
            }
        }
    }

    private bool IsEnemy(Actor actor, Actor potentialEnemy)
    {
        return actor.kingdom.isEnemy(potentialEnemy.kingdom);
    }

    private void AbsorbStats(Actor enemy)
    {
        actor.stats[S.health] += enemy.stats[S.health] / 2;
        actor.stats[S.damage] += enemy.stats[S.damage] / 2;
        actor.stats[S.speed] += enemy.stats[S.speed] / 2;
        actor.stats[S.accuracy] += enemy.stats[S.accuracy] / 2;
        actor.stats[S.attack_speed] += enemy.stats[S.attack_speed] / 2;
        actor.stats[S.knockback] += enemy.stats[S.knockback] / 2;
        actor.stats[S.targets] += enemy.stats[S.targets] / 2;
        actor.stats[S.area_of_effect] += enemy.stats[S.area_of_effect] / 2;
        actor.stats[S.size] += enemy.stats[S.size] / 2;
        actor.stats[S.range] += enemy.stats[S.range] / 2;
        actor.stats[S.critical_damage_multiplier] += enemy.stats[S.critical_damage_multiplier] / 2;
        actor.stats[S.scale] += enemy.stats[S.scale] / 2;
        actor.stats[S.mod_supply_timer] += enemy.stats[S.mod_supply_timer] / 2;
        actor.stats[S.fertility] += enemy.stats[S.fertility] / 2;
        actor.stats[S.armor] += enemy.stats[S.armor] / 2;
        actor.stats[S.dodge] += enemy.stats[S.dodge] / 2;
        actor.stats[S.diplomacy] += enemy.stats[S.diplomacy] / 2;
        actor.stats[S.max_age] += enemy.stats[S.max_age] / 2;
        actor.stats[S.max_children] += enemy.stats[S.max_children] / 2;
        actor.stats[S.knockback_reduction] += enemy.stats[S.knockback_reduction] / 2;

        actor.updateStats();
    }
}
*/