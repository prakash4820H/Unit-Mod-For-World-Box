using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public static class AriseShadowTraitInit
{
    public static void Init()
    {
        ActorTrait trait = new ActorTrait
        {
            id = "arise_shadow_trait",
            path_icon = "ui/Icons/shadow_arise.png",
            group_id = UnitTraitGroup.Unit,
            action_special_effect = ApplyTrait
        };
        AssetManager.traits.add(trait);
        LocalizationUtility.addTraitToLocalizedLibrary(trait.id, "Summon shadows from slain enemies.");
        PlayerConfig.unlockTrait("arise_shadow_trait");
    }

    public static bool ApplyTrait(BaseSimObject pSelf, WorldTile pTile = null)
    {
        Actor actor = pSelf as Actor;
        if (actor == null || !actor.hasTrait("arise_shadow_trait"))
        {
            return false;
        }
        AriseShadowTrait component = actor.gameObject.GetComponent<AriseShadowTrait>();
        if (component == null)
        {
            actor.addTrait("strong_minded");
            component = actor.gameObject.AddComponent<AriseShadowTrait>();
        }
        return true;
    }
}

public class AriseShadowTrait : MonoBehaviour
{
    private Actor actor;
    private Dictionary<Actor, Actor> shadowMinions = new Dictionary<Actor, Actor>();
    private HashSet<Actor> processedEnemies = new HashSet<Actor>();
    private float enemyDetectionRadius = 15f;
    private Color[] shadowColors = new Color[] {
        new Color(0.059f, 0.106f, 0.153f),  // #0f1b27
        new Color(0.071f, 0.153f, 0.188f),  // #122730
        new Color(0.239f, 0.408f, 0.475f),  // #3d6879
        new Color(0.929f, 0.988f, 1.0f)     // #edfcff
    };

    void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
            return;
        }
        if (actor.GetComponent<AriseShadowTrait>() == null)
        {
            actor.gameObject.AddComponent<AriseShadowTrait>();
        }
    }

    void Update()
    {
        HandleShadowVisibility();
        HandleShadowFollowing();
        CleanupDeadShadows();
        Actor killedEnemy = CheckForKilledEnemy();
        if (killedEnemy != null)
        {
            StartCoroutine(CollectShadowRoutine(killedEnemy));
        }
    }

    private Actor CheckForKilledEnemy()
    {
        foreach (Actor unit in MapBox.instance.units.getSimpleList())
        {
            if (!unit.isAlive() && !processedEnemies.Contains(unit))
            {
                processedEnemies.Add(unit);
                return unit;
            }
        }
        return null;
    }

    private IEnumerator CollectShadowRoutine(Actor enemy)
    {
        if (!shadowMinions.ContainsKey(enemy))
        {
            actor.has_status_frozen = true;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1.5f);
                if (TryCollectShadow(enemy))
                {
                    Actor shadow = SpawnShadowMinion(enemy);
                    if (shadow != null)
                    {
                        StartCoroutine(InitialVisibility(shadow));
                    }
                    break;
                }
            }
            actor.has_status_frozen = false;
        }
    }

    private bool TryCollectShadow(Actor enemy)
    {
        if (enemy == null || enemy.asset == null)
        {
            Debug.LogError("Attempted to collect shadow from a null or invalid enemy.");
            return false;
        }
        if (enemy.data.level <= actor.data.level)
        {
            return true;
        }
        else
        {
            return UnityEngine.Random.Range(0, 1) > 0.5f;
        }
    }


    private Actor SpawnShadowMinion(Actor enemy)
    {
        Actor shadow = MapBox.instance.units.createNewUnit(enemy.asset.id, actor.currentTile);
        if (shadow == null)
        {
            Debug.LogError("Failed to create shadow.");
            return null;
        }

        CopyEnemyPropertiesToShadow(enemy, shadow);
        shadowMinions[enemy] = shadow;
        StartCoroutine(ColorCycle(shadow));
        return shadow;
    }

    private void CopyEnemyPropertiesToShadow(Actor enemy, Actor shadow)
    {
        shadow.data.name = enemy.data.name + " Shadow";
        shadow.kingdom = actor.kingdom;
        shadow.updateStats();
    }

    private IEnumerator InitialVisibility(Actor shadow)
    {
        SetShadowVisibility(shadow, true);
        yield return new WaitForSeconds(3);
        SetShadowVisibility(shadow, false);
    }

    private IEnumerator ColorCycle(Actor shadow)
    {
        int colorIndex = 0;
        SpriteRenderer renderer = shadow.GetComponent<SpriteRenderer>();

        if (renderer == null)
        {
            Debug.LogWarning("ColorCycle cannot run because the SpriteRenderer is missing.");
            yield break;
        }

        while (shadow != null && shadow.gameObject != null && shadow.gameObject.activeSelf && shadow.isAlive())
        {
            for (int i = 0; i < shadowColors.Length; i++)
            {
                if (shadow._show_shadow)
                {
                    Color startColor = renderer.color;
                    Color endColor = shadowColors[i];
                    float duration = 1f;
                    float elapsed = 0f;

                    while (elapsed < duration)
                    {
                        if (shadow == null || !shadow._show_shadow || !shadow.isAlive())
                        {
                            yield break;
                        }

                        renderer.color = Color.Lerp(startColor, endColor, elapsed / duration);
                        elapsed += Time.deltaTime;
                        yield return null;
                    }

                    renderer.color = endColor;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void SetShadowVisibility(Actor shadow, bool isVisible)
    {
        if (shadow == null || shadow.gameObject == null)
        {
            Debug.LogError("Shadow or its GameObject is null.");
            return;
        }

        SpriteRenderer renderer = shadow.GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            Debug.LogWarning("SpriteRenderer component missing on shadow.");
            return;
        }

        renderer.enabled = isVisible;
        renderer.gameObject.SetActive(isVisible);
        shadow._show_shadow = isVisible;

        // Toggle weapon visibility
        SetWeaponVisibility(shadow, isVisible);
    }

    private void SetWeaponVisibility(Actor shadow, bool isVisible)
    {
        if (shadow.equipment != null && shadow.equipment.weapon != null)
        {
            var weapon = shadow.equipment.weapon.data;
            if (weapon != null)
            {
                string weaponId = weapon.id;
                Transform weaponTransform = shadow.transform.Find(weaponId);
                if (weaponTransform != null)
                {
                    SpriteRenderer weaponRenderer = weaponTransform.GetComponent<SpriteRenderer>();
                    if (weaponRenderer != null)
                    {
                        weaponRenderer.enabled = isVisible;
                    }
                }
            }
        }
    }

    private void HandleShadowVisibility()
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, enemyDetectionRadius);
        bool hasNearbyEnemies = nearbyEnemies.Count > 0;

        foreach (var shadow in shadowMinions.Values)
        {
            if (shadow != null)
            {
                SetShadowVisibility(shadow, hasNearbyEnemies);
            }
        }
    }

    private List<Actor> GetNearbyEnemies(Actor mainActor, float radius)
    {
        List<Actor> enemies = new List<Actor>();
        foreach (Actor unit in MapBox.instance.units.getSimpleList())
        {
            if (Vector2.Distance(mainActor.currentPosition, unit.currentPosition) <= radius && IsEnemy(mainActor, unit))
            {
                enemies.Add(unit);
            }
        }
        return enemies;
    }

    private bool IsEnemy(Actor actor, Actor potentialEnemy)
    {
        return actor.kingdom.isEnemy(potentialEnemy.kingdom);
    }

    private void HandleShadowFollowing()
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, enemyDetectionRadius);
        bool hasNearbyEnemies = nearbyEnemies.Count > 0;

        foreach (var shadow in shadowMinions.Values)
        {
            if (shadow == null || shadow.gameObject == null)
            {
                Debug.LogWarning("Attempting to handle a null or destroyed shadow.");
                continue;
            }

            if (!hasNearbyEnemies)
            {
                shadow.beh_actor_target = actor;
                shadow.goTo(actor.currentTile, pPathOnWater: false, pWalkOnBlocks: false);
            }
            else
            {
                shadow.beh_actor_target = null;
            }
        }
    }

    private void CleanupDeadShadows()
    {
        List<Actor> toRemove = new List<Actor>();

        foreach (var kvp in shadowMinions)
        {
            if (kvp.Value == null || !kvp.Value.isAlive())
            {
                toRemove.Add(kvp.Key);
            }
        }

        foreach (Actor deadShadow in toRemove)
        {
            if (shadowMinions.ContainsKey(deadShadow))
            {
                shadowMinions.Remove(deadShadow);
            }
        }
    }
}
