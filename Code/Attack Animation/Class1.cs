using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unit
{
    public enum ActorState
    {
        Idle,
        Walk,
        Attack,
        Death,
        Swim,
        Landing
    }

    public class MobAnimationData
    {
        public string[] WalkSprites, SwimSprites, IdleSprites, DeathSprites;

        // Dynamic list of attack sets, allowing for any number of attack sets
        public List<string[]> AttackSpriteSets;

        private string assetId;
        private int currentAttackSetIndex = 0;  // Track which attack set to use

        private float attackSetTimer = 0.0f;

        public MobAnimationData(string assetId)
        {
            this.assetId = assetId;
            AttackSpriteSets = new List<string[]>();  // Initialize the list of attack sets
        }

        // This method retrieves the current attack set based on the index
        public string[] GetCurrentAttackSet()
        {
            if (AttackSpriteSets.Count == 0)
                return null;

            return AttackSpriteSets[currentAttackSetIndex];
        }


        public void UpdateAttackSetTimer(float deltaTime)
        {
            attackSetTimer += deltaTime;

            if (attackSetTimer >= 5.0f)
            {
                attackSetTimer = 0.0f;
                currentAttackSetIndex = (currentAttackSetIndex + 1) % AttackSpriteSets.Count;
            }
        }


        public string WalkAnimation
        {
            set { WalkSprites = GenerateSpritePaths(assetId, value); }
        }
        public string SwimAnimation
        {
            set { SwimSprites = GenerateSpritePaths(assetId, value); }
        }
        public string IdleAnimation
        {
            set { IdleSprites = GenerateSpritePaths(assetId, value); }
        }
        public void AddAttackSet(string attackSet)
        {
            AttackSpriteSets.Add(GenerateSpritePaths(assetId, attackSet));
        }
        public string DeathAnimation
        {
            set { DeathSprites = GenerateSpritePaths(assetId, value); }
        }

        private string[] GenerateSpritePaths(string assetId, string animationSequence)
        {
            return animationSequence.Split(',')
                .Select(sprite => $"actors/{assetId}/{sprite.Trim()}").ToArray();
        }
    }

    public static class ExampleActorOverrideSprite
    {
        private static float frameDuration = 0.09f;
        private static Dictionary<string, MobAnimationData> mobAnimations = new Dictionary<string, MobAnimationData>();

        public static void Init()
        {
            mobAnimations["DragonSlayer"] = new MobAnimationData("DragonSlayer")
            {
                WalkSprites = GenerateSpritePaths("DragonSlayer", "walk_0,walk_1,walk_2,walk_3"),
                SwimSprites = GenerateSpritePaths("DragonSlayer", "swim_0,swim_1,swim_2"),
                IdleSprites = GenerateSpritePaths("DragonSlayer", "idle_0,idle_1,idle_2,idle_3,idle_4,idle_5"),
                DeathSprites = GenerateSpritePaths("DragonSlayer", "death_0,death_1,death_2,death_3,death_4,death_5,death_6,death_7,death_8,death_9,death_10,death_11,death_12")
            };

            // Adding multiple attack sets dynamically for "DragonSlayer"
            mobAnimations["DragonSlayer"].AddAttackSet("attack_0,attack_1,attack_2,attack_3");
            mobAnimations["DragonSlayer"].AddAttackSet("a1,a2,a3");
            mobAnimations["DragonSlayer"].AddAttackSet("lol1,lol2,lol3");

            mobAnimations["newone"] = new MobAnimationData("newone")
            {
                WalkSprites = GenerateSpritePaths("newone", "walk_0,walk_1,walk_2,walk_3,walk_4,walk_5,walk_6,walk_7"),
                SwimSprites = GenerateSpritePaths("newone", "swim_0,swim_1,swim_2,swim_3"),
                IdleSprites = GenerateSpritePaths("newone", "idle_0"),
                DeathSprites = GenerateSpritePaths("newone", "death_0,death_1,death_2,death_3")
            };

            // Adding multiple attack sets dynamically for "newone"
            mobAnimations["newone"].AddAttackSet("attack_0,attack_1,attack_2,attack_3,attack_4,attack_5,attack_6,attack_7,attack_8,attack_9,attack_10,attack_11,attack_12,attack_13");
            mobAnimations["newone"].AddAttackSet("a1,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11");
            mobAnimations["newone"].AddAttackSet("a1,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11");

            foreach (var mob in mobAnimations.Keys)
            {
                InitMobSpriteOverride(mob);
            }
        }

        private static void InitMobSpriteOverride(string assetId)
        {
            var asset = AssetManager.actor_library.get(assetId);
            asset.has_override_sprite = true;
            asset.deathAnimationAngle = false;
            asset.disablePunchAttackAnimation = true;
            asset.get_override_sprite = actor => GetOverrideSprite(actor, assetId);
        }

        public static Sprite GetOverrideSprite(Actor actor, string assetId)
        {
            if (!mobAnimations.TryGetValue(assetId, out MobAnimationData animationData))
            {
                Debug.LogWarning($"[GetOverrideSprite] Asset ID '{assetId}' not found in mobAnimations. Defaulting to idle frame.");
                return GetSingleSprite("actors/default/idle_0");
            }

            if (actor.frame_data == null)
            {
                actor.frame_data = new AnimationFrameData();
            }

            actor.data.get("last_anim_frame_idx", out int lastFrame);
            actor.data.get("last_anim_state", out string lastAnimState);
            actor.data.get("is_dead_animation_finished", out bool isDeadAnimationFinished);

            // Retrieve the frameTimer from actor.data (actor-specific, not static)
            actor.data.get("frame_timer", out float frameTimer);

            if (lastAnimState == null)
            {
                lastAnimState = "idle";  // Default to idle state if not set
            }

            int currFrame = lastFrame;
            string currAnimState = lastAnimState;
            string framePath = animationData.IdleSprites?[0] ?? "actors/default/idle_0";

            // Handle pause or frozen state: Retain the current frame without resetting (EXCEPT for death animation)
            if ((actor.has_status_frozen || Config.paused || MapBox.instance.isGameplayControlsLocked()) && currAnimState != "death")
            {
                framePath = GetFrameBasedOnState(animationData, currAnimState, currFrame);
                Sprite pausedSprite = GetSingleSprite(framePath);
                return UnitSpriteConstructor.getSpriteUnit(
                    actor.frame_data,
                    pausedSprite,
                    actor,
                    actor.kingdom.kingdomColor,  // Apply kingdom color even when paused
                    actor.race,
                    actor.data.skin_set,
                    actor.data.skin,
                    actor.asset.texture_atlas
                );
            }


            // Update the frame timer for the specific actor
            frameTimer += Time.deltaTime;
            if (frameTimer >= frameDuration)
            {
                frameTimer = 0.0f;
                currFrame++;
            }
            //colour fixes

            // Save the updated frameTimer to actor.data
            actor.data.set("frame_timer", frameTimer);

            // Update the attack set timer
            animationData.UpdateAttackSetTimer(Time.deltaTime);

            // Attack animation logic: Play when the attack hits
            if (Mathf.Abs(actor.attackTimer - actor.s_attackSpeed_seconds) < 0.01f)
            {
                currFrame = 0;
                currAnimState = "attack";
            }

            if (actor.attackTimer > 0 && currAnimState == "attack")
            {
                var currentAttackSprites = animationData.GetCurrentAttackSet();
                if (currFrame >= currentAttackSprites.Length)
                {
                    currFrame = 0;
                    currAnimState = "idle";
                }
                else
                {
                    framePath = currentAttackSprites[currFrame];
                    actor.data.set("last_anim_frame_idx", currFrame);
                    actor.data.set("last_anim_state", currAnimState);
                    Sprite attackSprite = GetSingleSprite(framePath);
                    return UnitSpriteConstructor.getSpriteUnit(
                        actor.frame_data,
                        attackSprite,
                        actor,
                        actor.kingdom.kingdomColor,  // Ensure kingdom color applies during attack
                        actor.race,
                        actor.data.skin_set,
                        actor.data.skin,
                        actor.asset.texture_atlas
                    );
                }
            }

            // Handle other animation states (swim, walk, idle, death)
            if (!actor.isAlive())
            {
                currAnimState = "death";
                if (lastAnimState != "death")
                {
                    currFrame = 0;
                    frameTimer = 0.0f;
                    isDeadAnimationFinished = false;
                }

                if (!isDeadAnimationFinished)
                {
                    if (currFrame >= animationData.DeathSprites?.Length)
                    {
                        currFrame = animationData.DeathSprites?.Length - 1 ?? 0;
                        isDeadAnimationFinished = true;
                        actor.data.set("is_dead_animation_finished", true);
                    }
                    framePath = animationData.DeathSprites?[currFrame];
                }
                else
                {
                    framePath = animationData.DeathSprites?[animationData.DeathSprites.Length - 1];
                }
            }
            else if (actor.isAffectedByLiquid())
            {
                currAnimState = "swim";
                if (animationData.SwimSprites != null && animationData.SwimSprites.Length > 0)
                {
                    currFrame = currFrame % animationData.SwimSprites.Length;
                    framePath = animationData.SwimSprites[currFrame];
                }
            }
            else if (actor.is_moving)
            {
                currAnimState = "walk";
                if (animationData.WalkSprites != null && animationData.WalkSprites.Length > 0)
                {
                    currFrame = currFrame % animationData.WalkSprites.Length;
                    framePath = animationData.WalkSprites[currFrame];
                }
            }
            else
            {
                currAnimState = "idle";
                if (animationData.IdleSprites != null && animationData.IdleSprites.Length > 0)
                {
                    currFrame = currFrame % animationData.IdleSprites.Length;
                    framePath = animationData.IdleSprites[currFrame];
                }
            }

            actor.data.set("last_anim_frame_idx", currFrame);
            actor.data.set("last_anim_state", currAnimState);

            if (framePath == null)
            {
                Debug.LogWarning($"[GetOverrideSprite] Frame path is null for state '{currAnimState}' in '{assetId}'");
                framePath = "actors/default/idle_0";
            }

            actor.frame_data.sheet_path = framePath;
            actor.frame_data.id = $"{currAnimState}_frame_{currFrame}";

            return UnitSpriteConstructor.getSpriteUnit(
                actor.frame_data,
                GetSingleSprite(framePath),
                actor,
                actor.kingdom.kingdomColor,
                actor.race,
                actor.data.skin_set,
                actor.data.skin,
                actor.asset.texture_atlas
            );
        }

        // Helper method to return the correct frame path based on the state
        private static string GetFrameBasedOnState(MobAnimationData animationData, string currAnimState, int currFrame)
        {
            switch (currAnimState)
            {
                case "walk":
                    return animationData.WalkSprites?[currFrame % animationData.WalkSprites.Length];
                case "swim":
                    return animationData.SwimSprites?[currFrame % animationData.SwimSprites.Length];
                case "attack":
                    var currentAttackSprites = animationData.GetCurrentAttackSet();
                    return currentAttackSprites?[currFrame % currentAttackSprites.Length];
                case "death":
                    return animationData.DeathSprites?[currFrame % animationData.DeathSprites.Length];
                case "idle":
                default:
                    return animationData.IdleSprites?[currFrame % animationData.IdleSprites.Length];
            }
        }

        private static Sprite GetSingleSprite(string path)
        {
            var components = path.Split('/');
            var folder = string.Join("/", components.Take(components.Length - 1));
            var spriteList = SpriteTextureLoader.getSpriteList(folder);
            return spriteList.FirstOrDefault(x => x.name == components.Last());
        }

        private static string[] GenerateSpritePaths(string assetId, string animationSequence)
        {
            return animationSequence.Split(',')
                .Select(sprite => $"actors/{assetId}/{sprite.Trim()}").ToArray();
        }
    }
}
