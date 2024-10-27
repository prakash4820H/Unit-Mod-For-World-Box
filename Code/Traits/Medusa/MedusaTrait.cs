using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class MedusaTrait : MonoBehaviour
    {
        private Actor actor;
        private float petrificationRange = 5f;

        private void Start()
        {
            actor = GetComponent<Actor>();
            if (actor != null)
            {
                ApplyMedusaTrait(actor);
            }
        }

        private void Update()
        {
            if (actor != null)
            {
                ApplyPetrificationEffect();
            }
        }

        private void ApplyMedusaTrait(Actor actor)
        {
            ActorTrait medusaTrait = new ActorTrait();
            medusaTrait.id = "medusa_trait";
            medusaTrait.path_icon = "ui/Icons/icon_medusa_trait";
            medusaTrait.group_id = UnitTraitGroup.Unit;
            medusaTrait.action_special_effect = ApplySpecialEffect;
            AssetManager.traits.add(medusaTrait);
            PlayerConfig.unlockTrait("medusa_trait");
        }

        private void ApplyPetrificationEffect()
        {
            List<Actor> nearbyUnits = GetNearbyUnits(actor, petrificationRange);
            foreach (Actor unit in nearbyUnits)
            {
                if (unit != actor) // Ensure the main actor is not affected
                {
                    PetrifyUnit(unit);
                }
            }
        }

        private List<Actor> GetNearbyUnits(Actor actor, float range)
        {
            List<Actor> nearbyUnits = new List<Actor>();
            List<Actor> allUnits = MapBox.instance.units.getSimpleList();
            foreach (Actor unit in allUnits)
            {
                if (Vector2.Distance(actor.currentPosition, unit.currentPosition) <= range)
                {
                    nearbyUnits.Add(unit);
                }
            }
            return nearbyUnits;
        }

        private void PetrifyUnit(Actor unit)
        {
            unit.stats.set(S.speed, 0);
            unit.stats.set(S.attack_speed, 0);
            unit.has_status_frozen = true;
            SpriteRenderer spriteRenderer = unit.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.gray; // Change color to gray to represent stone
            }

            unit.updateStats();
        }

        public static bool ApplySpecialEffect(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("medusa_trait"))
            {
                return false;
            }

            MedusaTrait medusaTrait = actor.gameObject.GetComponent<MedusaTrait>();
            if (medusaTrait == null)
            {
                medusaTrait = actor.gameObject.AddComponent<MedusaTrait>();
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Actor))]
    public class MedusaTraitInit
    {
        public static void Init()
        {
            ActorTrait medusaTrait = new ActorTrait();
            medusaTrait.id = "medusa_trait";
            medusaTrait.path_icon = "ui/Icons/iconMedusa";
            medusaTrait.group_id = UnitTraitGroup.Unit;
            medusaTrait.type = TraitType.Negative;
            medusaTrait.action_special_effect = MedusaTrait.ApplySpecialEffect;
            AssetManager.traits.add(medusaTrait);
            PlayerConfig.unlockTrait("medusa_trait");
        }
    }
}
