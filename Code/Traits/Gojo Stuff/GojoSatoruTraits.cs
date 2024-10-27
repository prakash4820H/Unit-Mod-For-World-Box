using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class GojoSatoruTraits : MonoBehaviour
    {
        private Actor gojoSatoru;
        private float infinityRange = 5.0f;
        private float expandedInfinityRange = 10.0f;
        private Actor currentTarget;
        private bool infinityExpanded = false;
        private bool domainExpansionActive = false;
        private float domainExpansionDuration = 5.0f;

        private float lastAbilityTime = -20.0f;
        private bool blueAbilityUsed = false;
        private bool isDead = false;
        private float blueAbilityCooldown = 30.0f;
        private float redAbilityCooldown = 30.0f;
        private float lastBlueAbilityTime = -30.0f;
        private float lastRedAbilityTime = -30.0f;
        private int blueAbilityTriggerCount = 5;
        private int redAbilityTriggerCount = 5;
        private int hollowPurpleTriggerCount = 10;
        private int domainExpansionTriggerCount = 60;
        private int blackFlashTriggerCount = 3;
        private string blueIconPath = "ui/Icons/blue";
        private string redIconPath = "ui/Icons/red";
        private GameObject abilityIconObject;



        void Start()
        {
            Init();
            Initialize();
        }

        public static void Init()
        {
            string traitId = "gojo_satoru";

            // Check if the trait already exists
            if (AssetManager.traits.get(traitId) != null)
            {
                Debug.LogWarning($"Trait {traitId} already exists. Skipping initialization.");
                return;
            }

            ActorTrait gojoTrait = new ActorTrait
            {
                id = traitId,
                action_special_effect = new WorldAction(ApplyInfinity),
                path_icon = "ui/Icons/icongojo",
                group_id = UnitTraitGroup.Unit,
                type = TraitType.Positive,
            };

            gojoTrait.base_stats[S.health] = 1000f;
            gojoTrait.base_stats[S.damage] = 100f;
            gojoTrait.base_stats[S.attack_speed] = 0.1f;
            gojoTrait.base_stats[S.armor] = 100f;
            gojoTrait.base_stats[S.dodge] = 90f;
            gojoTrait.base_stats[S.knockback_reduction] = 1000f;
            gojoTrait.base_stats[S.max_age] = 2000f;
            AssetManager.traits.add(gojoTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(gojoTrait.id, "The Power.");
            PlayerConfig.unlockTrait(traitId);
        }

        private static bool ApplyInfinity(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("gojo_satoru"))
                return false;

            GojoSatoruTraits component = actor.gameObject.GetComponent<GojoSatoruTraits>();
            if (component == null)
            {
                component = actor.gameObject.AddComponent<GojoSatoruTraits>();
                component.gojoSatoru = actor;
            }
            return true;
        }

        // Initialize UI Element for Ability Icon
        private void Initialize()
        {
            gojoSatoru.stats[S.health] = 1000f;
            gojoSatoru.stats[S.damage] = 100f;
            gojoSatoru.stats[S.attack_speed] = 0.1f;
            gojoSatoru.stats[S.armor] = 100f;
            gojoSatoru.stats[S.dodge] = 90f;
            gojoSatoru.stats[S.knockback_reduction] = 1000f;
            gojoSatoru.updateStats();

            // Create a UI element for displaying the ability icon
            abilityIconObject = new GameObject("AbilityIcon");
            SpriteRenderer spriteRenderer = abilityIconObject.AddComponent<SpriteRenderer>();
            abilityIconObject.transform.SetParent(gojoSatoru.transform);
            abilityIconObject.transform.localPosition = new Vector3(0, 2, 0); // Adjust the position to be above the actor's head
            abilityIconObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Adjust the scale to reduce size
            spriteRenderer.sortingOrder = 10; // Ensure the icon is rendered above the character
            spriteRenderer.sortingLayerName = "effectsTop"; // Set the sorting layer
        }


        private void SetAbilityIcon(string iconPath)
        {
            Sprite icon = Resources.Load<Sprite>(iconPath);
            if (icon != null && abilityIconObject != null)
            {
                SpriteRenderer spriteRenderer = abilityIconObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = icon;
                spriteRenderer.sortingLayerName = "effectsTop"; // Set the sorting layer
                abilityIconObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Adjust the scale to reduce size
            }
        }


        private void ClearAbilityIcon()
        {
            if (abilityIconObject != null)
            {
                SpriteRenderer spriteRenderer = abilityIconObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = null;
            }
        }

        void Update()
        {
            if (gojoSatoru == null)
                return;

            if (!gojoSatoru.isAlive() && !isDead)
            {
                OnGojoDeath();
                isDead = true;
                return;
            }

            UpdateTarget();
            List<Actor> nearbyEnemies = GetNearbyEnemies(gojoSatoru, infinityExpanded ? expandedInfinityRange : infinityRange);
            HandleInfinityExpansion(nearbyEnemies);

            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != gojoSatoru && IsEnemy(enemy) && !enemy.has_status_frozen)
                {
                    if (currentTarget == null || currentTarget != enemy)
                    {
                        KeepAway(enemy);
                    }
                    else
                    {
                        AllowApproach(enemy);
                    }
                    enemy.updateStats();
                }
            }

            if (Time.time - lastAbilityTime >= 20.0f)
            {
                if (nearbyEnemies.Count >= blueAbilityTriggerCount && !IsAnyAbilityActive() && !blueAbilityUsed && Time.time - lastBlueAbilityTime >= blueAbilityCooldown)
                {
                    ActivateBlueAbility(nearbyEnemies);
                    lastAbilityTime = Time.time;
                    lastBlueAbilityTime = Time.time;
                    blueAbilityUsed = true;
                }
                else if (nearbyEnemies.Count >= redAbilityTriggerCount && !IsAnyAbilityActive() && blueAbilityUsed && Time.time - lastRedAbilityTime >= redAbilityCooldown)
                {
                    ActivateRedAbility(nearbyEnemies);
                    lastAbilityTime = Time.time;
                    lastRedAbilityTime = Time.time;
                    blueAbilityUsed = false;
                }

                if (nearbyEnemies.Count >= hollowPurpleTriggerCount && !IsAnyAbilityActive())
                {
                    ActivateHollowPurple(nearbyEnemies);
                    lastAbilityTime = Time.time;
                }

                if (nearbyEnemies.Count >= domainExpansionTriggerCount && !IsAnyAbilityActive())
                {
                    ActivateDomainExpansion(nearbyEnemies);
                    lastAbilityTime = Time.time;
                }

                if (nearbyEnemies.Count >= blackFlashTriggerCount && !IsAnyAbilityActive())
                {
                    ActivateBlackFlash(nearbyEnemies);
                    lastAbilityTime = Time.time;
                }
            }

            gojoSatoru.updateStats();
        }

        private void UpdateTarget()
        {
            if (currentTarget == null || !currentTarget.isAlive())
            {
                currentTarget = GetNewTarget();
            }
        }

        private Actor GetNewTarget()
        {
            List<Actor> nearbyEnemies = GetNearbyEnemies(gojoSatoru, infinityRange);
            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != gojoSatoru && IsEnemy(enemy) && enemy.isAlive() && IsWeaker(enemy))
                {
                    return enemy;
                }
            }
            return null;
        }

        private bool IsWeaker(Actor enemy)
        {
            return gojoSatoru.stats[S.attack_speed] > enemy.stats[S.attack_speed] &&
                   gojoSatoru.stats[S.health] > enemy.stats[S.health] &&
                   gojoSatoru.stats[S.armor] > enemy.stats[S.armor] &&
                   gojoSatoru.stats[S.dodge] > enemy.stats[S.dodge] &&
                   gojoSatoru.stats[S.damage] > enemy.stats[S.damage];
        }

        private List<Actor> GetNearbyEnemies(Actor actor, float range)
        {
            List<Actor> nearbyEnemies = new List<Actor>();
            List<Actor> allActors = MapBox.instance.units.getSimpleList();

            foreach (Actor otherActor in allActors)
            {
                if (Vector2.Distance(actor.currentPosition, otherActor.currentPosition) <= range && IsEnemy(otherActor))
                {
                    nearbyEnemies.Add(otherActor);
                }
            }

            return nearbyEnemies;
        }

        private bool IsEnemy(Actor enemy)
        {
            return gojoSatoru.kingdom.isEnemy(enemy.kingdom);
        }

        private void UpdateEnemyPosition(Actor enemy)
        {
            if (enemy == null || enemy.currentPosition == null)
                return;

            WorldTile lastTile = MapBox.instance.GetTileSimple((int)enemy.currentPosition.x, (int)enemy.currentPosition.y);
            enemy.findCurrentTile();

            if (enemy.currentTile != lastTile)
            {
                Debug.Log("Position desync detected. Correcting...");
                enemy.updatePos();
            }
        }

        // Modify the KeepAway method to include position update
        private void KeepAway(Actor enemy)
        {
            if (enemy == null || enemy.currentPosition == null)
                return;

            Vector2 direction = (enemy.currentPosition - gojoSatoru.currentPosition).normalized;
            enemy.currentPosition += direction * Time.deltaTime * 5.0f;
            enemy.dirty_position = true;
            UpdateEnemyPosition(enemy); // Ensure the enemy position is updated on the map
        }

        // Modify the AllowApproach method to include position update
        private void AllowApproach(Actor enemy)
        {
            if (enemy == null || enemy.currentPosition == null)
                return;

            enemy.dirty_position = true;
            UpdateEnemyPosition(enemy); // Ensure the enemy position is updated on the map
        }

        private void HandleInfinityExpansion(List<Actor> nearbyEnemies)
        {
            if (nearbyEnemies.Count >= 30 && !infinityExpanded)
            {
                infinityExpanded = true;
                infinityRange = expandedInfinityRange;
                foreach (Actor enemy in nearbyEnemies)
                {
                    KeepAway(enemy);
                }
            }
            else if (nearbyEnemies.Count < 15 && infinityExpanded)
            {
                infinityExpanded = false;
                infinityRange /= 2;
            }
        }
        private void ActivateBlueAbility(List<Actor> nearbyEnemies)
        {
            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != gojoSatoru && IsEnemy(enemy) && !enemy.has_status_frozen)
                {
                    Vector2 direction = (gojoSatoru.currentPosition - enemy.currentPosition).normalized;
                    enemy.currentPosition += direction * 5.0f;
                    enemy.getHit(30.0f, true, AttackType.Other, gojoSatoru, false, false);
                    enemy.updateStats();
                }
            }
            SetAbilityIcon(blueIconPath);
            StartCoroutine(ClearAbilityIconAfterDuration(5.0f)); // Adjust the duration as needed
        }

        private void ActivateRedAbility(List<Actor> nearbyEnemies)
        {
            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != gojoSatoru && IsEnemy(enemy) && !enemy.has_status_frozen)
                {
                    Vector2 direction = (enemy.currentPosition - gojoSatoru.currentPosition).normalized;
                    enemy.currentPosition += direction * 5.0f;
                    enemy.getHit(50.0f, true, AttackType.Other, gojoSatoru, false, false);
                    enemy.updateStats();
                }
            }
            SetAbilityIcon(redIconPath);
            StartCoroutine(ClearAbilityIconAfterDuration(5.0f)); // Adjust the duration as needed
        }

        private IEnumerator ClearAbilityIconAfterDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            ClearAbilityIcon();
        }


        private Sprite LoadIcon(string path)
        {
            return Resources.Load<Sprite>(path);
        }


        private void ActivateHollowPurple(List<Actor> nearbyEnemies)
        {
            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != gojoSatoru && IsEnemy(enemy) && !enemy.has_status_frozen)
                {
                    enemy.killHimself();
                    gojoSatoru.data.kills++;
                    gojoSatoru.addExperience(10);
                }
            }
        }

        private void ActivateDomainExpansion(List<Actor> nearbyEnemies)
        {
            if (domainExpansionActive)
                return;

            domainExpansionActive = true;
            StartCoroutine(DomainExpansionCoroutine(nearbyEnemies));
        }

        private IEnumerator DomainExpansionCoroutine(List<Actor> nearbyEnemies)
        {
            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != null && enemy != gojoSatoru && IsEnemy(enemy))
                {
                    enemy.has_status_frozen = true;
                    enemy.stats[S.knockback_reduction] = 1000;
                    enemy.updateStats();
                }
            }

            yield return new WaitForSeconds(domainExpansionDuration);

            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != null && enemy != gojoSatoru && IsEnemy(enemy))
                {
                    enemy.killHimself();
                }
            }

            domainExpansionActive = false;
        }


        private void ActivateBlackFlash(List<Actor> nearbyEnemies)
        {
            foreach (Actor enemy in nearbyEnemies)
            {
                if (enemy != gojoSatoru && IsEnemy(enemy) && IsWithinInfinity(enemy))
                {
                    ApplyForce(gojoSatoru.currentTile, 5, 1.5f, true, true, 30, gojoSatoru, gojoSatoru);
                    enemy.updateStats();
                }
            }
        }

        private bool IsWithinInfinity(Actor enemy)
        {
            return Vector2.Distance(gojoSatoru.currentPosition, enemy.currentPosition) <= infinityRange;
        }

        private void ApplyForce(WorldTile targetTile, float forceMagnitude, float duration, bool isKnockback, bool ignoreArmor, float damage, Actor attacker, Actor self)
        {
            List<Actor> forceTempActorList = new List<Actor>();
            Toolbox.fillListWithUnitsFromChunk(targetTile.chunk, forceTempActorList);
            for (int i = 0; i < targetTile.chunk.neighboursAll.Count; i++)
            {
                Toolbox.fillListWithUnitsFromChunk(targetTile.chunk.neighboursAll[i], forceTempActorList);
            }
            if (attacker != null && attacker.isActor())
            {
                forceTempActorList.Remove(attacker);
            }
            float num = 1f;
            for (int j = 0; j < forceTempActorList.Count; j++)
            {
                Actor actor = forceTempActorList[j];
                if (!actor.asset.very_high_flyer && IsEnemy(actor))
                {
                    float num2 = Toolbox.DistVec2(actor.currentTile.pos, targetTile.pos);
                    if (num2 <= forceMagnitude)
                    {
                        if (actor.asset.canBeHurtByPowers && IsEnemy(actor))
                        {
                            AttackType pType = AttackType.Other;
                            actor.getHit(damage, true, pType, attacker, true, false);
                        }
                        float num3 = forceMagnitude - forceMagnitude * actor.stats[S.knockback_reduction];
                        if (num3 < 0f)
                        {
                            num3 = 0f;
                        }
                        if (num3 > 0f)
                        {
                            float angle = Toolbox.getAngle((float)actor.currentTile.x, (float)actor.currentTile.y, (float)targetTile.x, (float)targetTile.y);
                            float num4 = Mathf.Cos(angle) * num3 * num;
                            float num5 = Mathf.Sin(angle) * num3 * num;
                            if (isKnockback)
                            {
                                num4 *= -1f;
                                num5 *= -1f;
                            }
                            actor.addForce(num4, num5, num);
                        }
                    }
                }
            }
        }

        private void OnGojoDeath()
        {
            List<Actor> allActors = MapBox.instance.units.getSimpleList();
            Vector2 mapCenter = new Vector2(MapBox.width / 2, MapBox.height / 2);
            float forceMagnitude = Mathf.Max(MapBox.width, MapBox.height) * 2.0f;

            foreach (Actor enemy in allActors)
            {
                if (IsEnemy(enemy))
                {
                    Vector2 direction = (enemy.currentPosition - mapCenter).normalized;
                    enemy.addForce(direction.x * forceMagnitude, direction.y * forceMagnitude, 0);
                    StartCoroutine(KillEnemyAfterDelay(enemy, 1.0f));
                }
            }
        }

        private IEnumerator KillEnemyAfterDelay(Actor enemy, float delay)
        {
            yield return new WaitForSeconds(delay);
            enemy.killHimself();
        }

        private bool IsAnyAbilityActive()
        {
            return domainExpansionActive || gojoSatoru.hasStatus("red_ability_effect") ||
                   gojoSatoru.hasStatus("blue_ability_effect") ||
                   gojoSatoru.hasStatus("hollow_purple_ability_effect");
        }
    }
}
