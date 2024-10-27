using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAndKillTrait : MonoBehaviour
{
    private Actor actor;
    private float radius = 10f; // Radius to affect nearby enemies

    private void Start()
    {
        actor = GetComponent<Actor>();
        ApplyEffect();
    }

    public bool ApplyEffect()
    {
        if (actor != null)
        {
            List<Actor> nearbyEnemies = GetNearbyEnemies();
            foreach (Actor enemy in nearbyEnemies)
            {
                StartCoroutine(FreezeAndKillEnemy(enemy));
            }
            return true;
        }
        return false;
    }

    private List<Actor> GetNearbyEnemies()
    {
        List<Actor> enemies = new List<Actor>();
        foreach (Actor potentialEnemy in MapBox.instance.units.getSimpleList())
        {
            if (potentialEnemy != actor && Vector2.Distance(potentialEnemy.currentPosition, actor.currentPosition) <= radius && actor.kingdom.isEnemy(potentialEnemy.kingdom))
            {
                enemies.Add(potentialEnemy);
            }
        }
        return enemies;
    }

    private IEnumerator FreezeAndKillEnemy(Actor enemy)
    {
        enemy.has_status_frozen = true;
        enemy.updateStats();
        yield return new WaitForSeconds(15f); // Freeze duration
        if (enemy.isAlive())
        {
            enemy.killHimself();
        }
    }

    private void OnDestroy()
    {
        // Cleanup if necessary
    }
}
