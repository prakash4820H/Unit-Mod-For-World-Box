using UnityEngine;

namespace Unit
{
    public class EnhancedActorTrait : MonoBehaviour
    {
        private Actor actor;
        private ActorAsset originalAsset;
        private ActorAsset modifiedAsset;
        private bool isApplied = false;
        private bool specialEffectApplied = false;
        private float checkInterval = 1f; // Interval to check for trait changes
        private float nextCheckTime = 0f;

        void Start()
        {
            actor = GetComponent<Actor>();
            if (actor != null)
            {
                originalAsset = actor.asset;
                ApplyTrait(actor);
            }
        }

        void Update()
        {
            if (Time.time >= nextCheckTime)
            {
                nextCheckTime = Time.time + checkInterval;
                CheckForTraitChanges();
            }
        }

        public void ApplyTrait(Actor actor)
        {
            if (isApplied) return;

            originalAsset = actor.asset;

            // Create a deep copy of the original asset
            modifiedAsset = new ActorAsset();
            ActorAssetUtility.CopyActorAsset(originalAsset, modifiedAsset);
            actor.asset.base_stats.CopyFrom(originalAsset.base_stats);
            // Apply enhancements
            ApplyEnhancements(modifiedAsset);
            modifiedAsset.canBeKilledByLifeEraser = false;
            modifiedAsset.canBeKilledByDivineLight = true;
            actor.asset = modifiedAsset;
            actor.addTrait("fire_proof");
            actor.updateStats();
            isApplied = true;
        }

        void OnDestroy()
        {
            // Restore the original asset when the trait is removed or the actor is destroyed
            if (actor != null && originalAsset != null)
            {
                actor.asset = originalAsset;
                actor.updateStats();
            }
        }

        private void CheckForTraitChanges()
        {
            if (actor != null && actor.hasTrait("enhanced_actor_trait"))
            {
                // Reapply enhancements when traits are changed
                ApplyTrait(actor);
            }
        }

        private void ApplyEnhancements(ActorAsset asset)
        {
            // Apply only necessary enhancements without modifying base stats
            asset.canBeKilledByStuff = false;
            asset.canBeHurtByPowers = false;
            asset.canBeMovedByPowers = false;
            // Add any other necessary enhancements here
        }

        public bool ApplySpecialEffect()
        {
            if (specialEffectApplied) return false;

            specialEffectApplied = true;
            // Special effect logic here
            Debug.Log("Special effect applied successfully.");
            return true;
        }
    }
}
