using HarmonyLib;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class NearbyEnemyTrait : MonoBehaviour
{
    private Actor actor;
    private bool isStatsDoubled = false;
    private float checkInterval = 1f;
    private float nextCheckTime = 0f;
    private BaseStats originalStats;
    private float detectionRange = 10f; // Range to check for enemies

    private void Start()
    {
        actor = GetComponent<Actor>();
        if (actor != null)
        {
            originalStats = new BaseStats();
            originalStats.CopyFrom(actor.stats);
        }
    }

    private void Update()
    {
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            CheckNearbyEnemies();
        }
    }

    private void CheckNearbyEnemies()
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies();
        if (nearbyEnemies.Count > 0 && !isStatsDoubled)
        {
            DoubleStats();
        }
        else if (nearbyEnemies.Count == 0 && isStatsDoubled)
        {
            RevertStats();
        }
    }

    private List<Actor> GetNearbyEnemies()
    {
        List<Actor> nearbyEnemies = new List<Actor>();
        foreach (Actor otherActor in MapBox.instance.units.getSimpleList())
        {
            if (otherActor != actor && IsEnemy(otherActor))
            {
                float distance = Vector2.Distance(actor.currentPosition, otherActor.currentPosition);
                if (distance <= detectionRange)
                {
                    nearbyEnemies.Add(otherActor);
                }
            }
        }
        return nearbyEnemies;
    }

    private bool IsEnemy(Actor otherActor)
    {
        return actor.kingdom.isEnemy(otherActor.kingdom);
    }

    private void DoubleStats()
    {
        actor.stats[S.damage] *= 2;
        actor.stats[S.health] *= 2;
        actor.stats[S.speed] *= 2;
        actor.stats[S.accuracy] *= 2;
        actor.stats[S.range] *= 2;
        actor.stats[S.armor] *= 2;
        actor.stats[S.size] *= 2;
        actor.stats[S.fertility] *= 2;
        actor.stats[S.dodge] *= 2;
        actor.stats[S.intelligence] *= 2;
        actor.stats[S.diplomacy] *= 2;
        actor.stats[S.stewardship] *= 2;
        actor.stats[S.warfare] *= 2;
        actor.stats[S.critical_chance] *= 2;
        actor.stats[S.critical_damage_multiplier] *= 2;
        actor.stats[S.area_of_effect] *= 2;
        actor.stats[S.knockback] *= 2;
        actor.stats[S.knockback_reduction] *= 2;
        actor.stats[S.max_age] *= 2;
        actor.stats[S.damage_range] *= 2;

        // Apply the same for other relevant stats
        actor.updateStats();
        isStatsDoubled = true;
    }

    private void RevertStats()
    {
        actor.stats.CopyFrom(originalStats);
        actor.updateStats();
        isStatsDoubled = false;
    }
}

// Trait Initialization
[HarmonyPatch(typeof(Actor))]
public class NearbyEnemyTraitInit
{
    public static void Init()
    {
        ActorTrait actorTrait = new ActorTrait();
        actorTrait.id = "nearby_enemy_trait";
        actorTrait.path_icon = "ui/Icons/icon_nearby_enemy_trait";
        actorTrait.group_id = UnitTraitGroup.Unit;
        actorTrait.action_special_effect = ApplySpecialEffect;
        ActorTrait actorTrait2 = actorTrait;
        actorTrait2.base_stats[S.health] = 100f;
        AssetManager.traits.add(actorTrait2);
        LocalizationUtility.addTraitToLocalizedLibrary(actorTrait2.id, "Doubles the actor's stats when enemies are nearby.");
        PlayerConfig.unlockTrait("nearby_enemy_trait");
    }

    public static bool ApplySpecialEffect(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        Actor actor = pTarget as Actor;
        if (actor == null || !actor.hasTrait("nearby_enemy_trait"))
        {
            return false;
        }
        NearbyEnemyTrait nearbyEnemyTrait = actor.gameObject.GetComponent<NearbyEnemyTrait>();
        if (nearbyEnemyTrait == null)
        {
            nearbyEnemyTrait = actor.gameObject.AddComponent<NearbyEnemyTrait>();
        }
        return true;
    }
}
