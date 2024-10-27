using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class LightManipulationTraitInit
    {
        public static void Init()
        {
            Debug.Log("Initializing Light Manipulation Trait");

            ActorTrait lightManipulationTrait = new ActorTrait
            {
                id = "light_manipulation_trait",
                path_icon = "ui/Icons/icon_light_manipulation",
                group_id = UnitTraitGroup.Unit,
                action_attack_target = new AttackAction(LightManipulationAttack)
            };
            AssetManager.traits.add(lightManipulationTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(lightManipulationTrait.id, "Characters can create blinding flashes of light to disorient enemies.");
            PlayerConfig.unlockTrait("light_manipulation_trait");

            Harmony.CreateAndPatchAll(typeof(LightManipulationTraitInit));

            Debug.Log("Light Manipulation Trait Initialized");
        }

        // Define the behavior of the light manipulation trait
        public static bool LightManipulationAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            Debug.Log("Executing Light Manipulation Attack");

            Actor attacker = pSelf as Actor;
            Actor target = pTarget as Actor;

            if (attacker == null || target == null)
                return false;

            // Find enemies in range to disorient
            List<Actor> enemiesInRange = FindEnemiesInRange(attacker, 5); // 5 is the range of the blinding flash
            CreateBlindingFlash(attacker, enemiesInRange);

            return true;
        }

        public static List<Actor> FindEnemiesInRange(Actor actor, float range)
        {
            List<Actor> enemiesInRange = new List<Actor>();
            foreach (var enemy in MapBox.instance.units)
            {
                if (Vector3.Distance(actor.transform.position, enemy.transform.position) <= range && enemy.kingdom != actor.kingdom)
                {
                    enemiesInRange.Add(enemy);
                }
            }
            return enemiesInRange;
        }
        public static void CreateBlindingFlash(Actor actor, List<Actor> enemiesInRange)
        {
            Debug.Log("Creating Blinding Flash");

            // Create visual light effect
            GameObject lightEffect = new GameObject("BlindingFlash");
            Light light = lightEffect.AddComponent<Light>();
            light.type = LightType.Point;
            light.range = 10f;
            light.intensity = 8f;
            light.color = Color.white;
            light.transform.position = actor.transform.position + new Vector3(0, 1, 0);  // Position slightly above the actor

            // Add a particle system for a more dramatic effect
            var particleSystem = lightEffect.AddComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startColor = Color.white;
            main.startLifetime = 0.5f;
            main.startSpeed = 10f;  // Increase speed for more dramatic effect
            main.startSize = 0.5f;  // Decrease size for more concentrated particles
            main.maxParticles = 1000;  // Ensure enough particles are generated
            main.simulationSpace = ParticleSystemSimulationSpace.World;  // Ensure particles are positioned in world space

            var emission = particleSystem.emission;
            emission.rateOverTime = 500;  // Increase emission rate

            var shape = particleSystem.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 2f;

            // Ensure the particle system uses a proper material
            var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();

            // Attempt to find the shaders and use fallback if necessary
            Shader particleShader = Shader.Find("Particles/Standard Unlit");
            if (particleShader == null)
            {
                Debug.LogWarning("Shader 'Particles/Standard Unlit' not found. Falling back to 'Legacy Shaders/Particles/Additive'.");
                particleShader = Shader.Find("Legacy Shaders/Particles/Additive");
            }

            if (particleShader == null)
            {
                Debug.LogError("Fallback shader 'Legacy Shaders/Particles/Additive' also not found. Aborting blinding flash creation.");
                return;
            }

            Material particleMaterial = new Material(particleShader);

            // Optionally, assign a default texture to the material
            Texture2D defaultTexture = Texture2D.whiteTexture;  // Use white texture for visibility
            particleMaterial.mainTexture = defaultTexture;

            renderer.material = particleMaterial;
            renderer.renderMode = ParticleSystemRenderMode.Billboard;  // Ensure particles always face the camera

            // Set the sorting layer and order to render on top of everything
            renderer.sortingOrder = 32767;  // Set very high sorting order to ensure on top

            particleSystem.Play();

            // Destroy light effect after a short duration
            GameObject.Destroy(lightEffect, 1f);  // Increased duration for visibility

            foreach (var enemy in enemiesInRange)
            {
                // Apply disorient effect to each enemy
                ApplyDisorientEffect(enemy);
            }

            Debug.Log("Blinding Flash Created");
        }



        /*
         public static void CreateBlindingFlash(Actor actor, List<Actor> enemiesInRange)
         {
             Debug.Log("Creating Blinding Flash");

             // Create visual light effect
             GameObject lightEffect = new GameObject("BlindingFlash");
             Light light = lightEffect.AddComponent<Light>();
             light.type = LightType.Point;
             light.range = 10f;
             light.intensity = 8f;
             light.color = Color.white;
             light.transform.position = actor.transform.position;

             // Optionally, you can add a particle system for a more dramatic effect
             var particleSystem = lightEffect.AddComponent<ParticleSystem>();
             var main = particleSystem.main;
             main.startColor = Color.white;
             main.startLifetime = 0.5f;
             main.startSpeed = 2f;
             main.startSize = 1f;

             var emission = particleSystem.emission;
             emission.rateOverTime = 100;

             var shape = particleSystem.shape;
             shape.shapeType = ParticleSystemShapeType.Sphere;
             shape.radius = 2f;

             particleSystem.Play();

             // Destroy light effect after a short duration
             GameObject.Destroy(lightEffect, 0.5f);

             foreach (var enemy in enemiesInRange)
             {
                 // Apply disorient effect to each enemy
                 ApplyDisorientEffect(enemy);
             }

             Debug.Log("Blinding Flash Created");
         }
        */


        public static void ApplyDisorientEffect(Actor enemy)
        {
            Debug.Log($"Applying Disorient Effect to {enemy.name}");

            // Example: Reduce enemy's accuracy and speed temporarily
            enemy.stats[S.accuracy] -= 50;
            enemy.stats[S.speed] -= 50;

            // Add code to revert the effects after a certain duration
        }
    }
}
