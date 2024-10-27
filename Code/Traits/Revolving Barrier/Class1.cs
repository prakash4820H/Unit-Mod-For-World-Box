using System.Collections.Generic;
using UnityEngine;

public class BarrierTrait : MonoBehaviour
{
    private Actor actor;
    private float barrierRadius = 5.0f; // Example radius
    private float revolutionSpeed = 5f; // Speed at which enemies revolve around the barrier
    private float selfRotationSpeed = 25.0f; // Speed at which enemies rotate around themselves
    private WorldTile lastTile;

    private void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
            return;
        }

        // Initialize barrier effect
        InvokeRepeating(nameof(CheckAndRevolveEnemies), 0f, 0.1f); // Check every 0.1 seconds
    }

    private void CheckAndRevolveEnemies()
    {
        if (actor == null)
        {
            return;
        }

        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, barrierRadius);
        foreach (Actor enemy in nearbyEnemies)
        {
            RevolveAroundBarrier(enemy);
            RotateSelf(enemy);
            MovementStuff(enemy); // Ensure position synchronization
        }
    }

    private List<Actor> GetNearbyEnemies(Actor currentActor, float range)
    {
        List<Actor> nearbyEnemies = new List<Actor>();
        foreach (Actor otherActor in MapBox.instance.units) // Assuming MapBox.instance.units contains all actors
        {
            if (otherActor == currentActor)
            {
                continue; // Skip the current actor
            }

            float distance = Vector2.Distance(currentActor.currentPosition, otherActor.currentPosition);
            if (distance <= range && currentActor.kingdom.isEnemy(otherActor.kingdom))
            {
                nearbyEnemies.Add(otherActor);
            }
        }
        return nearbyEnemies;
    }

    private void RevolveAroundBarrier(Actor enemy)
    {
        Vector2 direction = (enemy.currentPosition - actor.currentPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) + revolutionSpeed * Time.deltaTime; // Update angle for revolution
        Vector2 newPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * barrierRadius + actor.currentPosition;
        enemy.currentPosition = newPosition;

        // Rotate the sprite to face the direction of movement
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    private void RotateSelf(Actor enemy)
    {
        // Rotate the enemy around its own center
        enemy.transform.Rotate(Vector3.forward, selfRotationSpeed * Time.deltaTime);
    }

    private void MovementStuff(Actor controlledActor)
    {
        int mapWidth = MapBox.instance.tilesMap.GetLength(0); // Assuming tiles is a 2D array
        int mapHeight = MapBox.instance.tilesMap.GetLength(1); // Assuming tiles is a 2D array

        // Ensure the position is within valid bounds
        if (controlledActor.currentPosition.x >= 0 && controlledActor.currentPosition.y >= 0 &&
            controlledActor.currentPosition.x < mapWidth && controlledActor.currentPosition.y < mapHeight)
        {
            lastTile = MapBox.instance.GetTileSimple((int)controlledActor.currentPosition.x, (int)controlledActor.currentPosition.y);
            controlledActor.findCurrentTile();

            if (controlledActor.currentTile != lastTile)
            {
                Debug.Log("not again");
            }
        }
        else
        {
            Debug.LogError("Controlled actor position is out of bounds.");
        }
    }
}
