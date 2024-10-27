using HarmonyLib;
using UnityEngine;

namespace Unit
{
    public class ImmortalTrait : MonoBehaviour
    {
        private Actor actor;
        private ActorAsset originalAsset;
        private ActorAsset immortalAsset;
        private bool isApplied;
        private const float CheckInterval = 0.5f;
        private float nextCheckTime;

        private void Start()
        {
            actor = GetComponent<Actor>();
            if (actor != null)
            {
                originalAsset = actor.asset;
                ApplyTrait(actor);
            }
        }

        private void Update()
        {
            if (Time.time >= nextCheckTime)
            {
                nextCheckTime = Time.time + CheckInterval;
                EnsurePosition();
            }
        }

        private void ApplyTrait(Actor actor)
        {
            if (isApplied) return;

            immortalAsset = ActorAssetUtility.DeepCopyActorAsset(originalAsset);
            // Ensure critical properties aren't modified to prevent conflict
            immortalAsset.base_stats.CopyFrom(originalAsset.base_stats);
            actor.asset = immortalAsset;
            actor.updateStats();
            isApplied = true;
        }

        private void EnsurePosition()
        {
            if (actor?.currentPosition == null) return;

            Vector2 position = actor.currentPosition;
            float maxX = MapBox.width;
            float maxY = MapBox.height;

            if (position.x < 0 || position.x > maxX || position.y < 0 || position.y > maxY)
            {
                Debug.LogWarning($"Actor {actor.name} out of bounds at {position}. Clamping position.");
                position.x = Mathf.Clamp(position.x, 0, maxX);
                position.y = Mathf.Clamp(position.y, 0, maxY);
                actor.currentPosition = position;
                actor.updatePos();
                actor.checkSpriteToRender();
            }
        }
    }

    [HarmonyPatch(typeof(Actor))]
    public static class ImmortalTraitPatches
    {
        public static void Init()
        {
            const string traitId = "immortal_trait";

            if (AssetManager.traits.get(traitId) != null)
            {
                Debug.LogWarning($"Trait {traitId} already exists. Skipping initialization.");
                return;
            }

            var actorTrait = new ActorTrait
            {
                id = traitId,
                path_icon = "ui/Icons/1.png",
                group_id = UnitTraitGroup.Unit,
                type = TraitType.Positive,
                action_special_effect = ApplyImmortality
            };

            AssetManager.traits.add(actorTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(actorTrait.id, "Makes the actor unkillable.");
            PlayerConfig.unlockTrait(traitId);
        }

        private static bool ApplyImmortality(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            if (!(pTarget is Actor actor) || !actor.hasTrait("immortal_trait"))
            {
                return false;
            }

            var immortalTrait = actor.gameObject.GetComponent<ImmortalTrait>();
            if (immortalTrait == null)
            {
                immortalTrait = actor.gameObject.AddComponent<ImmortalTrait>();
            }

            return true;
        }

        [HarmonyPatch(nameof(Actor.updatePos))]
        [HarmonyPrefix]
        private static void UpdatePosPrefix(Actor __instance)
        {
            if (__instance.hasTrait("immortal_trait"))
            {
                Vector2 position = __instance.currentPosition;
                position.x = Mathf.Clamp(position.x, 0, MapBox.width);
                position.y = Mathf.Clamp(position.y, 0, MapBox.height);
                __instance.currentPosition = position;
            }
        }

        [HarmonyPatch(nameof(Actor.getHit))]
        [HarmonyPrefix]
        private static bool GetHitPrefix(Actor __instance, ref float pDamage, AttackType pAttackType, BaseSimObject pAttacker)
        {
            if (__instance.hasTrait("immortal_trait"))
            {
                pDamage = 0;
                return false;
            }

            return true;
        }

        [HarmonyPatch(nameof(Actor.killHimself))]
        [HarmonyPrefix]
        private static bool KillHimselfPrefix(Actor __instance)
        {
            return !(__instance?.hasTrait("immortal_trait") ?? false);
        }
    }
}
