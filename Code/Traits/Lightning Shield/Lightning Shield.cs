using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class LightningShieldTrait : MonoBehaviour
{
    private Actor actor;
    private float cooldownDuration = 10f; // Reduced cooldown duration
    private float lastAbilityTime = -30f;
    private float lightningRadius = 3f;
    private int lightningStrikes = 5;
    private bool isActive = false;
    private float lightningEffectRadius = 3f; // Radius for the damage effect


    private void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
        }
    }

    private void Update()
    {
        if (actor.data.health <= actor.getMaxHealth() / 2 && Time.time >= lastAbilityTime + cooldownDuration && !isActive)
        {
            StartCoroutine(ActivateLightningShield());
            lastAbilityTime = Time.time;
        }
    }

    private IEnumerator ActivateLightningShield()
    {
        isActive = true;
        RestoreHealthAboveHalf(); // Add this line to restore health
        List<Vector2> strikePositions = CalculateLightningPositions();
        foreach (Vector2 pos in strikePositions)
        {
            WorldTile tile = World.world.GetTile((int)pos.x, (int)pos.y);
            if (tile != null)
            {
                SpawnLightning(tile);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(cooldownDuration);
        isActive = false;
    }

    private void RestoreHealthAboveHalf()
    {
        int halfHealth = (int)(actor.getMaxHealth() / 2);
        if (actor.data.health <= halfHealth)
        {
            actor.data.health = halfHealth + 1; // Restore health above half
        }
    }


    private List<Vector2> CalculateLightningPositions()
    {
        List<Vector2> positions = new List<Vector2>();
        float angleStep = 360f / lightningStrikes;
        for (int i = 0; i < lightningStrikes; i++)
        {
            float angle = i * angleStep;
            float x = actor.currentPosition.x + lightningRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = actor.currentPosition.y + lightningRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            positions.Add(new Vector2(x, y));
        }
        return positions;
    }

    private void SpawnLightning(WorldTile pTile)
    {
        BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_small", pTile, 0.25f);
        if (baseEffect != null)
        {
            int pRad = 1;
            MapActionExtensions.DamageEnemiesInRadius(pTile, pRad, AttackType.Fire, actor, lightningEffectRadius);
            RepelEnemies(pTile, pRad);
            baseEffect.sprRenderer.flipX = Toolbox.randomBool();
        }
    }


    private void ApplyEffectToEnemy(Actor enemy)
    {
        float damage = 10f; // Damage value
        enemy.getHit(damage, true, AttackType.Other, this.actor);

        actor.data.health += (int)damage;
        if (actor.data.health > actor.getMaxHealth())
        {
            actor.data.health = actor.getMaxHealth();
        }

        actor.updateStats(); // Ensure the actor's stats are updated
    }

    private void RepelEnemies(WorldTile centerTile, float radius)
    {
        List<Actor> enemiesInRadius = MapActionExtensions.GetActorsInRadius(centerTile, radius);
        foreach (Actor enemy in enemiesInRadius)
        {
            if (enemy != actor && enemy.kingdom != actor.kingdom)
            {
                Vector3 enemyPos = enemy.currentPosition; // Ensure enemy.currentPosition is treated as Vector3
                Vector3 centerPos = new Vector3(centerTile.pos.x, centerTile.pos.y, enemyPos.z); // Match z-value with enemyPos
                Vector3 repelDirection = (enemyPos - centerPos).normalized;
                Vector3 repelOffset = repelDirection * radius;
                enemy.currentPosition = enemyPos + repelOffset; // Use Vector3 for the operation
                enemy.updatePos(); // Ensure position update is called
            }
        }
    }
}

public static class MapActionExtensions
{
    public static void DamageEnemiesInRadius(WorldTile pTile, float radius, AttackType damageType, Actor sourceActor, float damageRadius)
    {
        List<Actor> actorsInRadius = GetActorsInRadius(pTile, radius);
        foreach (Actor actor in actorsInRadius)
        {
            if (actor != sourceActor && actor.kingdom != sourceActor.kingdom &&
                Vector3.Distance(new Vector3(pTile.pos.x, pTile.pos.y, 0), actor.currentPosition) <= damageRadius)
            {
                actor.getHit(25f, true, damageType, sourceActor);  // Adjust damage to match tornado effect

                // Transfer the same amount of health to the source actor
                sourceActor.data.health += 25; // Using the same damage value for health transfer
                if (sourceActor.data.health > sourceActor.getMaxHealth())
                {
                    sourceActor.data.health = sourceActor.getMaxHealth();
                }

                sourceActor.updateStats(); // Ensure the source actor's stats are updated
            }
        }
    }


    public static List<Actor> GetActorsInRadius(WorldTile centerTile, float radius)
    {
        List<Actor> actorsInRadius = new List<Actor>();
        foreach (Actor actor in World.world.units)
        {
            if (Toolbox.DistVec3(new Vector3(centerTile.pos.x, centerTile.pos.y, 0), actor.currentPosition) <= radius)
            {
                actorsInRadius.Add(actor);
            }
        }
        return actorsInRadius;
    }
}

[HarmonyPatch(typeof(Actor))]
public class LightningShieldTraitInit
{
    public static void Init()
    {
        ActorTrait actorTrait = new ActorTrait
        {
            id = "lightning_shield",
            path_icon = "ui/Icons/thundershield.png",
            group_id = UnitTraitGroup.Unit,
            type = TraitType.Positive,
            action_special_effect = ApplyTrait
        };
        actorTrait.base_stats[S.health] = 500f;
        AssetManager.traits.add(actorTrait);
        LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Spawns lightning around the unit, damaging enemies and preventing them from approaching.");
        PlayerConfig.unlockTrait("lightning_shield");
    }

    public static bool ApplyTrait(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        Actor actor = pTarget as Actor;
        if (actor == null || !actor.hasTrait("lightning_shield"))
        {
            return false;
        }
        LightningShieldTrait component = actor.gameObject.GetComponent<LightningShieldTrait>();
        if (component == null)
        {
            component = actor.gameObject.AddComponent<LightningShieldTrait>();
        }
        return true;
    }
}
