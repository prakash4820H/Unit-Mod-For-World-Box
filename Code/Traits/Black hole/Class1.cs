using System.Collections.Generic;
using UnityEngine;

public class FrozenBarrierTrait : MonoBehaviour
{
    private Actor actor;
    private float barrierRadius = 5f; // Radius of the barrier
    private float suckingRadius = 100f; // Radius of the sucking zone
    private float suctionSpeed = 25f; // Speed at which enemies are sucked into the barrier
    private GameObject blackBarrier; // The black barrier GameObject
    private ParticleSystem particleSystem; // The particle system for visual effects

    private void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
            return;
        }

        // Apply frozen status
        actor.has_status_frozen = true;

        // Create the black barrier
        CreateBlackBarrier();

        // Initialize barrier effect
        InvokeRepeating(nameof(CheckAndSuckEnemies), 0f, 0.1f); // Check every 0.1 seconds
    }

    void Update()
    {
        if (!actor.hasTrait("frozen_barrier_trait")) // Check if the trait is removed
        {
            CleanupBarrier();
            Destroy(this); // Remove the component if the trait is not present
            return;
        }
    }

    private void CreateBlackBarrier()
    {
        blackBarrier = new GameObject("BlackBarrier");
        blackBarrier.transform.SetParent(actor.transform);
        blackBarrier.transform.localPosition = Vector3.zero;

        SpriteRenderer sr = blackBarrier.AddComponent<SpriteRenderer>();
        sr.sprite = CreateGradientCircleSprite();
        sr.sortingLayerName = "EffectsTop"; // Ensure it's rendered above other objects
        sr.sortingOrder = 10; // Ensure it's rendered above other objects
        sr.drawMode = SpriteDrawMode.Sliced;
        sr.size = new Vector2(barrierRadius * 2, barrierRadius * 2);

        // Adjust the transform to make the sphere slightly larger than the barrier
        float scaleFactor = 1.7f; // Increase the factor to make the sphere larger
        blackBarrier.transform.localScale = new Vector3(barrierRadius * 2 * scaleFactor, barrierRadius * 2 * scaleFactor, barrierRadius * 2 * scaleFactor); // Set the sphere size to be larger

        // Add rotation to the sphere
        blackBarrier.AddComponent<Rotator>().rotationSpeed = 30f;

        // Create and configure the particle system
        CreateParticleSystem();
    }

    private Sprite CreateGradientCircleSprite()
    {
        int size = 256; // Size of the texture
        Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);

        Color[] colors = new Color[size * size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(size / 2, size / 2));
                float alpha = Mathf.Clamp01(1 - (distance / (size / 2)));
                colors[y * size + x] = new Color(0, 0, 0, alpha);
            }
        }

        texture.SetPixels(colors);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    private void CreateParticleSystem()
    {
        particleSystem = blackBarrier.AddComponent<ParticleSystem>();

        var main = particleSystem.main;
        main.startSize = 0.2f;
        main.startLifetime = 2f; // Adjusted lifetime to give particles time to travel
        main.startSpeed = 1f; // Adjusted speed for a more noticeable effect
        main.maxParticles = 200; // Limit the number of particles

        var emission = particleSystem.emission;
        emission.rateOverTime = 50;

        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = suckingRadius; // Start particles at the sucking radius

        var colorOverLifetime = particleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f), new GradientColorKey(Color.black, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colorOverLifetime.color = gradient;

        var sizeOverLifetime = particleSystem.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 0.0f);
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1.0f, curve);

        var velocityOverLifetime = particleSystem.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.Local;
        velocityOverLifetime.radial = new ParticleSystem.MinMaxCurve(-suckingRadius / main.startLifetime.constant); // Negative to attract particles towards the center

        // Assign a simple black material created programmatically
        ParticleSystemRenderer renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = CreateBlackMaterial();
        renderer.sortingLayerName = "EffectsTop"; // Set the sorting layer to "EffectsTop"

        particleSystem.Play();
    }


    private Material CreateBlackMaterial()
    {
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        blackTexture.Apply();

        Shader shader = Shader.Find("Sprites/Default");
        if (shader == null)
        {
            Debug.LogError("Shader not found!");
            return null;
        }

        Material blackMaterial = new Material(shader);
        blackMaterial.mainTexture = blackTexture;
        blackMaterial.color = Color.black;

        return blackMaterial;
    }


    private void CheckAndSuckEnemies()
    {
        if (actor == null)
        {
            return;
        }

        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, suckingRadius);
        foreach (Actor enemy in nearbyEnemies)
        {
            if (enemy != null && enemy.isAlive())
            {
                float distance = Vector2.Distance(actor.currentPosition, enemy.currentPosition);

                if (distance <= barrierRadius)
                {
                    ActionLibrary.removeUnit(enemy);
                }
                else
                {
                    // Calculate the pull strength based on the distance
                    float pullStrength = Mathf.Clamp01((suckingRadius - distance) / suckingRadius);
                    float adjustedSuctionSpeed = suctionSpeed * pullStrength;

                    // Suck the enemy towards the barrier
                    Vector2 directionToBarrier = (actor.currentPosition - enemy.currentPosition).normalized;
                    enemy.currentPosition += directionToBarrier * adjustedSuctionSpeed * Time.deltaTime;

                    // Ensure the enemy's position is updated
                    WorldTile lastTile = enemy.currentTile;
                    enemy.findCurrentTile();

                    if (enemy.currentTile != lastTile)
                    {
                        enemy.setCurrentTile(enemy.currentTile);
                    }

                    enemy.updatePos();
                }
            }
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

    public void RemoveTrait()
    {
        // Custom logic for removing the trait
        CleanupBarrier();
        Destroy(this); // Remove this component from the actor
    }

    private void OnDestroy()
    {
        CleanupBarrier();

        if (blackBarrier != null)
        {
            Destroy(blackBarrier);
        }
    }
    private void CleanupBarrier()
    {
        if (blackBarrier != null)
        {
            Destroy(blackBarrier);
        }

        if (particleSystem != null)
        {
            particleSystem.Stop();
            Destroy(particleSystem.gameObject);
        }
    }
}


public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}