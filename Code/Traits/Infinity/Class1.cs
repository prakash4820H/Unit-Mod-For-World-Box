using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Unit;
public class InfinityTrait : MonoBehaviour
{
    private Actor actor;
    private float infinityRange = 5f; // Range within which Infinity affects enemies
    private Actor currentTarget; // The enemy currently allowed inside Infinity
    private HashSet<Actor> immuneActors = new HashSet<Actor>();

    private void Start()
    {
        actor = GetComponent<Actor>();
        if (actor != null)
        {
            ApplyInfinity();
        }
    }

    private void Update()
    {
        if (actor != null)
        {
            ManageInfinity();
            EngageCurrentEnemy();
        }
    }

    public float GetInfinityRange()
    {
        return infinityRange;
    }

    private void ApplyInfinity()
    {
        actor.stats[S.damage] = 100f; // Customize based on Gojo's stats
        actor.stats[S.health] = 1000f;
        actor.stats[S.armor] = 100f;
        actor.stats[S.dodge] = 90f;
        actor.updateStats();
        actor.restoreHealth(actor.getMaxHealth()); // Restore health when stats are updated
    }

    public void AddImmuneActor(Actor immuneActor)
    {
        immuneActors.Add(immuneActor);
    }

    public bool IsActorImmune(Actor actorToCheck)
    {
        return immuneActors.Contains(actorToCheck);
    }
    private void ManageInfinity()
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, infinityRange * 2);
        foreach (Actor enemy in nearbyEnemies)
        {
            if (IsEnemy(enemy) && !IsActorImmune(enemy))
            {
                if (currentTarget == null || !currentTarget.isAlive())
                {
                    currentTarget = enemy;
                }

                if (enemy == currentTarget)
                {
                    float distance = Vector2.Distance(enemy.currentPosition, actor.currentPosition);
                    if (distance < infinityRange)
                    {
                        // Ensure the enemy's tile matches their current position
                        WorldTile expectedTile = MapBox.instance.GetTileSimple((int)enemy.currentPosition.x, (int)enemy.currentPosition.y);
                        if (enemy.currentTile != expectedTile)
                        {
                            enemy.currentTile = expectedTile;
                            enemy.updatePos();
                        }
                    }
                    else
                    {
                        Vector2 direction = (actor.currentPosition - enemy.currentPosition).normalized;
                        Vector2 newPosition = enemy.currentPosition + direction * Time.deltaTime * enemy.stats[S.speed];

                        // Clamp the new position within map boundaries
                        ClampToMap(ref newPosition);

                        enemy.currentPosition = newPosition;

                        // Update logical position based on new position
                        WorldTile newTile = MapBox.instance.GetTileSimple((int)newPosition.x, (int)newPosition.y);
                        enemy.currentTile = newTile;
                        enemy.updatePos();
                    }
                }
                else
                {
                    Vector2 direction = (enemy.currentPosition - actor.currentPosition).normalized;
                    Vector2 newPosition = actor.currentPosition + direction * (infinityRange + 1);

                    // Clamp the new position within map boundaries
                    ClampToMap(ref newPosition);

                    enemy.currentPosition = newPosition;

                    // Update logical position based on enforced distance
                    WorldTile newTile = MapBox.instance.GetTileSimple((int)newPosition.x, (int)newPosition.y);
                    enemy.currentTile = newTile;
                    enemy.updatePos();
                }
            }
        }
    }

    private void ClampToMap(ref Vector2 position)
    {
        // Use the class name to access static members
        position.x = Mathf.Clamp(position.x, 0, MapBox.width - 1);
        position.y = Mathf.Clamp(position.y, 0, MapBox.height - 1);
    }


    private void EngageCurrentEnemy()
    {
        if (currentTarget != null && currentTarget.isAlive() && Vector2.Distance(actor.currentPosition, currentTarget.currentPosition) <= infinityRange)
        {
            // Implement attack logic directly on the current target
            AttackCurrentTarget();
        }
    }

    private void AttackCurrentTarget()
    {
        if (currentTarget != null && currentTarget.isAlive())
        {
            // Check if the current target is immune to Infinity
            if (IsActorImmune(currentTarget))
            {
                return; // Skip damage application
            }

            // Calculate damage or apply attack logic
            currentTarget.getHit(actor.stats[S.damage], true, AttackType.Other, actor);
            currentTarget.updateStats();

            // Check if the target is defeated and reset if necessary
            if (!currentTarget.isAlive())
            {
                currentTarget = null; // Allow a new target after defeating the current one
            }
        }
    }

    private List<Actor> GetNearbyEnemies(Actor actor, float range)
    {
        List<Actor> enemies = new List<Actor>();
        List<Actor> allActors = MapBox.instance.units.getSimpleList();
        foreach (Actor otherActor in allActors)
        {
            if (Vector2.Distance(actor.currentPosition, otherActor.currentPosition) <= range && IsEnemy(otherActor))
            {
                enemies.Add(otherActor);
            }
        }
        return enemies;
    }

    private bool IsEnemy(Actor otherActor)
    {
        return actor.kingdom.isEnemy(otherActor.kingdom);
    }

    private void OnDestroy()
    {
        if (actor != null)
        {
            actor.updateStats(); // Reset stats or cleanup if necessary
        }
    }
}

[HarmonyPatch(typeof(Actor))]
public class InfinityTraitInit
{
    public static void Init()
    {
        ActorTrait trait = new ActorTrait
        {
            id = "infinity_trait",
            path_icon = "ui/Icons/infinity", // Customize the icon path
            group_id = UnitTraitGroup.Unit,
            action_special_effect = ApplyInfinity
        };

        AssetManager.traits.add(trait);
        LocalizationUtility.addTraitToLocalizedLibrary(trait.id, "Grants the Infinity ability, preventing enemies from touching the actor except for one target at a time.");
        PlayerConfig.unlockTrait("infinity_trait");
    }

    public static bool ApplyInfinity(BaseSimObject target = null, WorldTile tile = null)
    {
        Actor actor = target as Actor;
        if (actor == null || !actor.hasTrait("infinity_trait"))
        {
            return false;
        }
        InfinityTrait infinityTrait = actor.gameObject.GetComponent<InfinityTrait>();
        if (infinityTrait == null)
        {
            infinityTrait = actor.gameObject.AddComponent<InfinityTrait>();
        }
        return true;
    }
}
