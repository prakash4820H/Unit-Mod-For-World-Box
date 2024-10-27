/*
using System.Collections.Generic;
using UnityEngine;

public class StatAbsorbTrait : MonoBehaviour
{
    private Actor actor;
    private HashSet<Actor> absorbedEnemies; // Track absorbed enemies
    private int lastLevel;
    private Dictionary<string, float> absorbedStats; // Track absorbed stats

    void Start()
    {
        actor = GetComponent<Actor>();
        absorbedEnemies = new HashSet<Actor>();
        absorbedStats = new Dictionary<string, float>();
        lastLevel = actor.data.level;

        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
            return;
        }

        if (actor.stats == null)
        {
            Debug.LogError("Actor stats is null.");
            actor.stats = new BaseStats(); // Initialize if not present
        }
    }

    void Update()
    {
        CheckEnemiesHealth();
        CheckLevelUp();
    }

    private void CheckEnemiesHealth()
    {
        foreach (Actor enemy in MapBox.instance.units.getSimpleList())
        {
            if (IsEnemy(actor, enemy) && enemy.data.health <= 0 && !absorbedEnemies.Contains(enemy))
            {
                AbsorbStats(enemy);
                absorbedEnemies.Add(enemy); // Mark the enemy as absorbed
            }
        }
    }

    private void CheckLevelUp()
    {
        if (actor.data.level > lastLevel)
        {
            ReapplyAbsorbedStats();
            lastLevel = actor.data.level;
        }
    }

    private bool IsEnemy(Actor actor, Actor potentialEnemy)
    {
        return actor.kingdom.isEnemy(potentialEnemy.kingdom);
    }

    private void AbsorbStats(Actor enemy)
    {
        float scaleFactor = 0.5f; // Absorb half of the enemy's stats

        Debug.Log($"Absorbing stats from enemy {enemy.data.name} with {enemy.stats.get(S.health)} health.");

        float healthAbsorbed = enemy.stats[S.health] * scaleFactor;

        Debug.Log($"Main unit before absorption: Health = {actor.stats.get(S.health)}, Damage = {actor.stats.get(S.damage)}");

        UpdateAbsorbedStats(S.damage, enemy.stats[S.damage] * scaleFactor);
        UpdateAbsorbedStats(S.health, healthAbsorbed);
        UpdateAbsorbedStats(S.speed, enemy.stats[S.speed] * scaleFactor);
        UpdateAbsorbedStats(S.accuracy, enemy.stats[S.accuracy] * scaleFactor);
        UpdateAbsorbedStats(S.attack_speed, enemy.stats[S.attack_speed] * scaleFactor);
        UpdateAbsorbedStats(S.knockback, enemy.stats[S.knockback] * scaleFactor);
        UpdateAbsorbedStats(S.targets, enemy.stats[S.targets] * scaleFactor);
        UpdateAbsorbedStats(S.area_of_effect, enemy.stats[S.area_of_effect] * scaleFactor);
        UpdateAbsorbedStats(S.size, enemy.stats[S.size] * scaleFactor);
        UpdateAbsorbedStats(S.range, enemy.stats[S.range] * scaleFactor);
        UpdateAbsorbedStats(S.critical_damage_multiplier, enemy.stats[S.critical_damage_multiplier] * scaleFactor);
        UpdateAbsorbedStats(S.scale, enemy.stats[S.scale] * scaleFactor);
        UpdateAbsorbedStats(S.mod_supply_timer, enemy.stats[S.mod_supply_timer] * scaleFactor);
        UpdateAbsorbedStats(S.fertility, enemy.stats[S.fertility] * scaleFactor);
        UpdateAbsorbedStats(S.armor, enemy.stats[S.armor] * scaleFactor);
        UpdateAbsorbedStats(S.dodge, enemy.stats[S.dodge] * scaleFactor);
        UpdateAbsorbedStats(S.diplomacy, enemy.stats[S.diplomacy] * scaleFactor);
        UpdateAbsorbedStats(S.max_age, enemy.stats[S.max_age] * scaleFactor);
        UpdateAbsorbedStats(S.max_children, enemy.stats[S.max_children] * scaleFactor);
        UpdateAbsorbedStats(S.knockback_reduction, enemy.stats[S.knockback_reduction] * scaleFactor);

        actor.updateStats();

        Debug.Log($"Main unit after absorption: Health = {actor.stats.get(S.health)}, Damage = {actor.stats.get(S.damage)}");
    }

    private void UpdateAbsorbedStats(string stat, float value)
    {
        if (!absorbedStats.ContainsKey(stat))
        {
            absorbedStats[stat] = 0;
        }
        absorbedStats[stat] += value;
        actor.stats[stat] += value;
    }

    private void ReapplyAbsorbedStats()
    {
        Debug.Log("Reapplying absorbed stats due to level up.");

        foreach (var entry in absorbedStats)
        {
            actor.stats[entry.Key] += entry.Value; // Use the string key
        }

        actor.updateStats();

        Debug.Log($"Main unit after reapplying stats: Health = {actor.stats.get(S.health)}, Damage = {actor.stats.get(S.damage)}");
    }
}
*/