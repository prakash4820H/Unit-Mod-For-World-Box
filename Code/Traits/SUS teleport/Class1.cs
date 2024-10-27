using HarmonyLib;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class TeleportTrait : MonoBehaviour
{
    private Actor actor;
    private float teleportCooldown = 1f; // Time between teleports
    private float lastTeleportTime = 0f;
    private float teleportDistance = 5f; // Distance for evasive teleportation
    private float healthRestoreRate = 5f; // Health restored per second during evasive teleportation
    private System.Random random = new System.Random(); // Random number generator

    private void Start()
    {
        actor = GetComponent<Actor>();
    }

    private void Update()
    {
        if (actor == null || !actor.isAlive()) return;

        float healthPercentage = actor.data.health / actor.getMaxHealth();

        if (healthPercentage < 0.3f)
        {
            // Random teleportation with attacks when health is below 30%
            if (Time.time - lastTeleportTime >= teleportCooldown)
            {
                RandomTeleportAndAttack();
                lastTeleportTime = Time.time;
            }
        }
        else if (healthPercentage < 0.5f)
        {
            // Teleport barrage with health restoration when health is below 50%
            if (Time.time - lastTeleportTime >= teleportCooldown)
            {
                TeleportBarrageAndRestoreHealth();
                lastTeleportTime = Time.time;
            }
        }
        else
        {
            // Aggressive teleportation to the closest enemy when health is above 50%
            if (Time.time - lastTeleportTime >= teleportCooldown)
            {
                AggressiveTeleportAndAttack();
                lastTeleportTime = Time.time;
            }
        }
    }

    private void AggressiveTeleportAndAttack()
    {
        Actor closestEnemy = FindClosestEnemy();
        if (closestEnemy == null) return;

        // Ensure the enemy is within range to attack before teleporting
        float distanceToEnemy = Vector2.Distance(actor.currentPosition, closestEnemy.currentPosition);
        if (distanceToEnemy > teleportDistance)
        {
            Vector2 directionToEnemy = (closestEnemy.currentPosition - actor.currentPosition).normalized;
            Vector2 newPosition = actor.currentPosition + directionToEnemy * (teleportDistance - 1); // Maintain a gap

            if (IsPositionValid(newPosition))
            {
                actor.currentPosition = newPosition;
                actor.updatePos();
                SynchronizePosition(); // Correct any desync after teleportation
            }
        }
        else
        {
            // Perform attack logic here
            AttackEnemy(closestEnemy);
        }
    }

    private void AttackEnemy(Actor enemy)
    {
        if (enemy != null && enemy.isAlive())
        {
            // Inflict damage to the enemy
            enemy.data.health -= (int)actor.stats[S.damage];
            enemy.updateStats(); // Update enemy stats after being hit

            // Check if the enemy should die
            if (enemy.data.health <= 0)
            {
                KillEnemy(enemy);
            }
        }
    }

    private void KillEnemy(Actor enemy)
    {
        if (enemy.isAlive())
        {
            enemy.killHimself(); // Use this method to kill the enemy
            actor.data.kills++;
            actor.addExperience(10); // Award experience for the kill
        }
    }


    private void RandomTeleportAndAttack()
    {
        Actor closestEnemy = FindClosestEnemy();
        if (closestEnemy == null) return;

        List<Vector2> teleportPositions = CalculateTeleportPositions(closestEnemy);

        // Choose a random direction for teleportation
        int randomIndex = random.Next(teleportPositions.Count);
        Vector2 randomPosition = teleportPositions[randomIndex];

        if (IsPositionValid(randomPosition))
        {
            actor.currentPosition = randomPosition;
            actor.updatePos();
            SynchronizePosition(); // Correct any desync after teleportation

            // Perform attack logic
            AttackEnemy(closestEnemy);
        }
    }

    private void TeleportBarrageAndRestoreHealth()
    {
        Actor closestEnemy = FindClosestEnemy();
        if (closestEnemy == null) return;

        List<Vector2> teleportPositions = CalculateTeleportPositions(closestEnemy);

        // Rotate through positions for teleport barrage
        int directionIndex = (int)((Time.time / teleportCooldown) % teleportPositions.Count);
        Vector2 newPosition = teleportPositions[directionIndex];

        if (IsPositionValid(newPosition))
        {
            actor.currentPosition = newPosition;
            actor.updatePos();
            SynchronizePosition(); // Correct any desync after teleportation

            // Restore health when teleporting around the enemy
            actor.restoreHealth((int)(healthRestoreRate * teleportCooldown));
        }
    }


    private Actor FindClosestEnemy()
    {
        List<Actor> enemies = MapBox.instance.units.getSimpleList();
        Actor closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            if (IsEnemy(enemy))
            {
                float distance = Vector2.Distance(actor.currentPosition, enemy.currentPosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    private List<Vector2> CalculateTeleportPositions(Actor enemy)
    {
        Vector2 enemyPos = enemy.currentPosition;
        return new List<Vector2>
        {
            enemyPos + new Vector2(teleportDistance, 0),  // Right
            enemyPos + new Vector2(-teleportDistance, 0), // Left
            enemyPos + new Vector2(0, teleportDistance),  // Up
            enemyPos + new Vector2(0, -teleportDistance), // Down
            enemyPos + new Vector2(teleportDistance, teleportDistance),  // Front-Right
            enemyPos + new Vector2(-teleportDistance, teleportDistance), // Front-Left
            enemyPos + new Vector2(teleportDistance, -teleportDistance), // Back-Right
            enemyPos + new Vector2(-teleportDistance, -teleportDistance) // Back-Left
        };
    }

    private bool IsPositionValid(Vector2 position)
    {
        WorldTile tile = MapBox.instance.GetTileSimple((int)position.x, (int)position.y);
        if (tile == null) return false;

        // Ensure the position is valid and walkable
        return tile.Type != TileLibrary.mountains && tile.Type != TileLibrary.lava0 && tile.Type != TileLibrary.lava1 &&
               tile.Type != TileLibrary.lava2 && tile.Type != TileLibrary.lava3;
    }

    private bool IsEnemy(Actor otherActor)
    {
        return actor.kingdom.isEnemy(otherActor.kingdom);
    }

    private void SynchronizePosition()
    {
        WorldTile lastTile = MapBox.instance.GetTileSimple((int)actor.currentPosition.x, (int)actor.currentPosition.y);
        actor.findCurrentTile();

        if (actor.currentTile != lastTile)
        {
            Debug.Log("Desync detected: Correcting position.");

            actor.currentPosition = new Vector2(actor.currentTile.pos.x, actor.currentTile.pos.y);
            actor.updatePos();
        }
    }
}

[HarmonyPatch(typeof(Actor))]
public class TeleportationTraitInit
{
    public static void Init()
    {
        ActorTrait trait = new ActorTrait
        {
            id = "teleportation_trait",
            path_icon = "ui/Icons/teleportation_trait_icon", // Customize icon path
            group_id = UnitTraitGroup.Unit,
            action_special_effect = ApplyTeleportation
        };

        AssetManager.traits.add(trait);
        LocalizationUtility.addTraitToLocalizedLibrary(trait.id, "Teleports to nearby enemies or evades when health is low.");
        PlayerConfig.unlockTrait("teleportation_trait");
    }

    public static bool ApplyTeleportation(BaseSimObject target = null, WorldTile tile = null)
    {
        Actor actor = target as Actor;
        if (actor == null || !actor.hasTrait("teleportation_trait"))
        {
            return false;
        }
        TeleportTrait teleportTrait = actor.gameObject.GetComponent<TeleportTrait>();
        if (teleportTrait == null)
        {
            teleportTrait = actor.gameObject.AddComponent<TeleportTrait>();
        }
        return true;
    }
}
