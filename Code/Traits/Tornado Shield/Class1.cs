using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class TornadoShieldTrait : MonoBehaviour
{
    private Actor actor;
    private float cooldownDuration = 10f; // Reduced cooldown duration
    private float lastAbilityTime = -30f;
    private float tornadoRadius = 3f;
    private float tornadoEffectRadius = 3f; // Radius for the damage effect
    private int tornadoCount = 4;
    private bool isActive = false;
    private Sprite[] tornadoSprites;
    private List<GameObject> activeTornadoes = new List<GameObject>();
    private float rotationSpeed = 30f; // Rotation speed in degrees per second

    private void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
        }
        LoadTornadoSprites();
    }

    private void Update()
    {
        if (!actor.isAlive())
        {
            ClearActiveTornadoes();
            isActive = false;
            return;
        }

        if (actor.data.health <= actor.getMaxHealth() / 2 && Time.time >= lastAbilityTime + cooldownDuration && !isActive)
        {
            StartCoroutine(ActivateTornadoShield());
            lastAbilityTime = Time.time;
        }
        if (isActive)
        {
            UpdateTornadoPositions();
        }
    }

    private IEnumerator ActivateTornadoShield()
    {
        isActive = true;
        List<Vector2> tornadoPositions = CalculateTornadoPositions();
        foreach (Vector2 pos in tornadoPositions)
        {
            WorldTile tile = World.world.GetTile((int)pos.x, (int)pos.y);
            if (tile != null)
            {
                SpawnTornado(tile);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(cooldownDuration);
        isActive = false;
        ClearActiveTornadoes();
    }

    private List<Vector2> CalculateTornadoPositions()
    {
        List<Vector2> positions = new List<Vector2>();
        float angleStep = 360f / tornadoCount;
        for (int i = 0; i < tornadoCount; i++)
        {
            float angle = i * angleStep;
            float x = actor.currentPosition.x + tornadoRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = actor.currentPosition.y + tornadoRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            positions.Add(new Vector2(x, y));
        }
        return positions;
    }

    private void SpawnTornado(WorldTile pTile)
    {
        GameObject tornado = new GameObject("Tornado");
        // Correctly set the position using Vector3, and set z to 0 or actor's z position
        tornado.transform.position = new Vector3(pTile.pos.x, pTile.pos.y, 0); // Or any appropriate z-value

        // Set the scale to decrease the size of the tornado
        float scaleFactor = 0.2f; // Adjust this value to the desired size
        tornado.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        SpriteRenderer spriteRenderer = tornado.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "EffectsTop"; // Ensure it is on the correct sorting layer
        StartCoroutine(AnimateTornado(spriteRenderer, 5f)); // Animate for 5 seconds
        StartCoroutine(TornadoEffectCoroutine(tornado, pTile, 5f)); // Set duration to 5 seconds
        activeTornadoes.Add(tornado);
    }

    private IEnumerator AnimateTornado(SpriteRenderer spriteRenderer, float duration)
    {
        float elapsedTime = 0f;
        int frameIndex = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (spriteRenderer == null || spriteRenderer.gameObject == null)
            {
                yield break; // Exit the coroutine if the sprite renderer or its game object is destroyed
            }

            if (tornadoSprites[frameIndex] != null)
            {
                spriteRenderer.sprite = tornadoSprites[frameIndex];
            }
            else
            {
                Debug.LogError("Tornado sprite is null at frame index: " + frameIndex);
            }
            frameIndex = (frameIndex + 1) % tornadoSprites.Length;
            yield return new WaitForSeconds(0.1f); // Adjust frame rate as needed
        }
        if (spriteRenderer != null)
        {
            Destroy(spriteRenderer.gameObject);
        }
    }

    private IEnumerator TornadoEffectCoroutine(GameObject tornado, WorldTile pTile, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (!actor.isAlive())
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            if (tornado == null)
            {
                yield break; // Exit the coroutine if the tornado object is destroyed
            }

            ApplyTornadoEffect(pTile);
            yield return null;
        }
        if (tornado != null)
        {
            Destroy(tornado);
        }
    }

    private void ApplyTornadoEffect(WorldTile pTile)
    {
        List<Actor> actorsInRadius = MapActionExtensions.GetActorsInRadius(pTile, tornadoEffectRadius);
        foreach (Actor enemy in actorsInRadius)
        {
            if (enemy != this.actor && enemy.kingdom != this.actor.kingdom)
            {
                float damage = 10f; // Damage value
                enemy.getHit(damage, true, AttackType.Other, this.actor); // Apply damage

                // Transfer the same amount of health to the actor
                actor.data.health += (int)damage;
                if (actor.data.health > actor.getMaxHealth())
                {
                    actor.data.health = actor.getMaxHealth();
                }

                actor.updateStats(); // Ensure the actor's stats are updated
            }
        }
    }


    private void UpdateTornadoPositions()
    {
        float angleStep = 360f / tornadoCount;
        for (int i = 0; i < activeTornadoes.Count; i++)
        {
            if (activeTornadoes[i] != null) // Check if the tornado GameObject is not null
            {
                float angle = Time.time * rotationSpeed + i * angleStep;
                float x = actor.currentPosition.x + tornadoRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = actor.currentPosition.y + tornadoRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
                activeTornadoes[i].transform.position = new Vector3(x, y, 0); // Update position
            }
        }
    }

    private void ClearActiveTornadoes()
    {
        foreach (GameObject tornado in activeTornadoes)
        {
            if (tornado != null)
            {
                Destroy(tornado);
            }
        }
        activeTornadoes.Clear();
    }

    private void LoadTornadoSprites()
    {
        tornadoSprites = new Sprite[4];
        tornadoSprites[0] = Resources.Load<Sprite>("effects/tornado/tornado1");
        tornadoSprites[1] = Resources.Load<Sprite>("effects/tornado/tornado2");
        tornadoSprites[2] = Resources.Load<Sprite>("effects/tornado/tornado3");
        tornadoSprites[3] = Resources.Load<Sprite>("effects/tornado/tornado4");

        for (int i = 0; i < tornadoSprites.Length; i++)
        {
            if (tornadoSprites[i] == null)
            {
                Debug.LogError($"Failed to load sprite: effects/tornado/tornado{i + 1}");
            }
        }
    }
}

[HarmonyPatch(typeof(Actor))]
public class TornadoShieldTraitInit
{
    public static void Init()
    {
        ActorTrait actorTrait = new ActorTrait
        {
            id = "tornado_shield",
            path_icon = "ui/Icons/tornadoshield.png",
            group_id = UnitTraitGroup.Unit,
            type = TraitType.Positive,
            action_special_effect = ApplyTrait
        };
        actorTrait.base_stats[S.health] = 500f;
        AssetManager.traits.add(actorTrait);
        LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Spawns tornadoes around the unit, damaging enemies and preventing them from approaching.");
        PlayerConfig.unlockTrait("tornado_shield");
    }

    public static bool ApplyTrait(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        Actor actor = pTarget as Actor;
        if (actor == null || !actor.hasTrait("tornado_shield"))
        {
            return false;
        }
        TornadoShieldTrait component = actor.gameObject.GetComponent<TornadoShieldTrait>();
        if (component == null)
        {
            component = actor.gameObject.AddComponent<TornadoShieldTrait>();
        }
        return true;
    }
}