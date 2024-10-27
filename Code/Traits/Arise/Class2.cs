using ai;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class ShadowFriendTrait : MonoBehaviour
{
    private Actor actor;
    private Dictionary<Actor, Actor> shadowFriends = new Dictionary<Actor, Actor>();
    private Dictionary<Actor, List<ActorEquipmentSlot>> storedEquipments = new Dictionary<Actor, List<ActorEquipmentSlot>>();
    private float detectionRadius = 10f; // Radius to detect nearby enemies
    private float movementRadius = 5f; // Radius to restrict shadow friends' movement
    private int lastKnownHealth;

    void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
            return;
        }
        lastKnownHealth = actor.data.health; // Assuming actor.data.health exists

        // Ensure the ShadowFriendTrait is attached
        if (actor.GetComponent<ShadowFriendTrait>() == null)
        {
            actor.gameObject.AddComponent<ShadowFriendTrait>();
        }
    }

    void Update()
    {
        DetectAndRevealShadows();
        CheckEnemiesHealth();
        RestrictShadowFriendsMovement();
        FollowMainActor(); // Added this line
    }

    private void CheckEnemiesHealth()
    {
        foreach (Actor enemy in MapBox.instance.units.getSimpleList())
        {
            if (IsEnemy(actor, enemy) && enemy.data.health <= 0 && !shadowFriends.ContainsKey(enemy) && enemy.asset.id != "dragon")
            {
                SpawnShadowFriend(enemy);
            }
        }
    }



    private void DetectAndRevealShadows()
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, detectionRadius);
        foreach (var shadowPair in new Dictionary<Actor, Actor>(shadowFriends))
        {
            Actor shadow = shadowPair.Value;
            if (shadow == null || shadow.gameObject == null)
            {
                shadowFriends.Remove(shadowPair.Key);
                if (storedEquipments.ContainsKey(shadowPair.Value))
                {
                    storedEquipments.Remove(shadowPair.Value);
                }
                continue;
            }

            bool hasNearbyEnemies = nearbyEnemies.Count > 0;
            SetShadowVisibility(shadow, hasNearbyEnemies);
        }
    }

    private void RestrictShadowFriendsMovement()
    {
        foreach (var shadowPair in new Dictionary<Actor, Actor>(shadowFriends))
        {
            Actor shadow = shadowPair.Value;
            if (shadow == null || shadow.gameObject == null)
            {
                shadowFriends.Remove(shadowPair.Key);
                continue;
            }

            // Skip movement restriction for shadows with bows, staffs, or more than 300 health
            if (HasBowOrStaff(shadow) || shadow.data.health > 300)
            {
                continue;
            }

            float distanceFromMain = Vector2.Distance(actor.currentPosition, shadow.currentPosition);
            if (distanceFromMain > movementRadius)
            {
                Vector2 direction = (shadow.currentPosition - actor.currentPosition).normalized;
                Vector2 newPosition = actor.currentPosition + direction * movementRadius;

                // Ensure the position is properly synced
                shadow.currentPosition = newPosition;
                WorldTile newTile = MapBox.instance.GetTileSimple((int)newPosition.x, (int)newPosition.y);
                shadow.setCurrentTile(newTile);
                shadow.updatePos();
                shadow.addForce(0f, 0f, 0.6f);
                shadow.clearBeh();
                shadow.updateStats();
                shadow.setStatsDirty();
            }
        }
    }

    private bool CanEquipItems(Actor shadow)
    {
        // Example condition to check if the actor can equip items
        // You might need to adapt this based on the actual properties of your Actor and ActorAsset classes
        return shadow.asset != null && shadow.asset.use_items;
    }


    private bool HasBowOrStaff(Actor shadow)
    {
        if (shadow == null || shadow.equipment == null || shadow.equipment.slots == null)
        {
            return false;
        }

        foreach (var slot in shadow.equipment.slots)
        {
            if (slot.data != null && (slot.data.id.ToLower().Contains("bow") || slot.data.id.ToLower().Contains("staff")))
            {
                return true;
            }
        }
        return false;
    }



    private void FollowMainActor()
    {
        foreach (var shadowPair in new Dictionary<Actor, Actor>(shadowFriends))
        {
            Actor shadow = shadowPair.Value;
            if (shadow == null || shadow.gameObject == null)
            {
                shadowFriends.Remove(shadowPair.Key);
                continue;
            }

            // Calculate a random nearby tile around the main actor within the movement radius
            WorldTile targetTile = GetRandomNearbyTileWithinRadius(actor.currentTile, movementRadius);

            if (targetTile != null)
            {
                shadow.goTo(targetTile, pPathOnWater: false, pWalkOnBlocks: false);
            }
        }
    }

    private WorldTile GetRandomNearbyTileWithinRadius(WorldTile originTile, float radius)
    {
        int radiusInTiles = Mathf.CeilToInt(radius);
        int randomX = UnityEngine.Random.Range(-radiusInTiles, radiusInTiles + 1);
        int randomY = UnityEngine.Random.Range(-radiusInTiles, radiusInTiles + 1);

        int targetX = originTile.x + randomX;
        int targetY = originTile.y + randomY;

        WorldTile targetTile = MapBox.instance.GetTile(targetX, targetY);
        return targetTile;
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

    private void SpawnShadowFriend(Actor enemy)
    {
        Actor shadowFriend = MapBox.instance.units.createNewUnit(enemy.asset.id, actor.currentTile);
        if (shadowFriend == null)
        {
            Debug.LogError("Failed to create shadow friend.");
            return;
        }

        // Create a deep copy of the ActorAsset for this specific actor
        ActorAsset originalAsset = enemy.asset;
        ActorAsset copiedAsset = ActorAssetUtility.DeepCopyActorAsset(originalAsset);
        shadowFriend.asset = copiedAsset;

        // Copy base stats from the enemy to the shadow friend
        shadowFriend.asset.base_stats.CopyFrom(originalAsset.base_stats);

        // Disable shadow for the shadow friend
        ActorAssetUtility.ModifyActorAsset(shadowFriend.asset, "shadow", false);
        shadowFriend.asset.canBeCitizen = false;
        shadowFriend.asset.immune_to_slowness = true;
        shadowFriend.asset.immune_to_tumor = true;
        shadowFriend.asset.immune_to_injuries = true;

        // Making the shadow friend black
        SpriteRenderer renderer = shadowFriend.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = new Color(0, 0, 0, 1); // Correct type assignment
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on shadow friend.");
        }
        //  shadowFriend.data.traits = enemy.data.traits;
        shadowFriend.data.name = enemy.data.name;
        shadowFriend.kingdom = actor.kingdom;
        shadowFriend.getMaxHealth();
        shadowFriend.restoreHealth(shadowFriend.data.health);
        shadowFriend.updateStats();
        shadowFriend.setStatsDirty(); // this updates the stats and stuff (- ___ -) wtf
        SetShadowVisibility(shadowFriend, false);
        shadowFriends[enemy] = shadowFriend;
    }

    private void SetShadowVisibility(Actor shadow, bool isVisible)
    {
        if (shadow == null || shadow.gameObject == null)
        {
            return;
        }

        if (isVisible)
        {
            RestoreEquipment(shadow);
        }
        else
        {
            StoreAndRemoveEquipment(shadow);
        }

        SpriteRenderer renderer = shadow.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = isVisible ? 1f : 0f;
            renderer.color = color;
        }
        shadow.setShowShadow(isVisible);
        if (CanEquipItems(shadow))
        {
            EnsureNoBowsOrStaffs(shadow);
        }
    }

    private void EnsureNoBowsOrStaffs(Actor shadow)
    {
        foreach (var slot in shadow.equipment.slots)
        {
            if (slot.data != null && (IsBowOrStaff(slot.data) || HasHighRange(slot.data)))
            {
                shadow.killHimself(); // Kill the shadow if it has a bow or staff or high range item
                break;
            }
        }
    }

    private void RestoreEquipment(Actor shadow)
    {
        if (shadow == null || shadow.equipment == null)
        {
            return;
        }

        if (storedEquipments.ContainsKey(shadow))
        {
            foreach (var slot in storedEquipments[shadow])
            {
                if (slot != null && slot.data != null)
                {
                    var equipmentSlot = shadow.equipment.getSlot(slot.type);
                    if (equipmentSlot != null)
                    {
                        equipmentSlot.setItem(slot.data);
                    }
                }
            }
        }
    }


    private void CopyStatsAndTraits(Actor enemy, Actor friend)
    {
        friend.asset.base_stats.CopyFrom(enemy.asset.base_stats);

        foreach (var trait in enemy.data.traits)
        {
            if (trait != "madness" && trait != "burning_feet" && trait != "plague" && trait != "infected") // Exclude specific traits
            {
                friend.addTrait(trait);
            }
        }
        ActorTool.copyUnitToOtherUnit(enemy, friend);
        friend.data.name = enemy.data.name;
        friend.data.gender = enemy.data.gender;
        friend.data.head = enemy.data.head;
        friend.data.skin = enemy.data.skin;
        friend.data.skin_set = enemy.data.skin_set;
        friend.removeTrait("burning_feet");
        friend.updateStats();
    }

    private void CopyEquipment(Actor enemy, Actor friend)
    {
        friend.equipment = new ActorEquipment();
        foreach (var slot in enemy.equipment.slots)
        {
            if (slot.data != null && !IsBowOrStaff(slot.data) && !HasHighRange(slot.data))
            {
                // Correctly add item to the friend's equipment
                friend.equipment.getSlot(slot.type).setItem(slot.data);
            }
        }
    }
    private bool IsBowOrStaff(ItemData item)
    {
        string itemName = item.name.ToLower();
        return itemName.Contains("bow") || itemName.Contains("staff");
    }

    private bool HasHighRange(ItemData item)
    {
        ItemAsset itemAsset = AssetManager.items.get(item.id);
        return itemAsset.base_stats[S.range] > 5f;
    }

    private void StoreAndRemoveEquipment(Actor shadow)
    {
        if (shadow == null || shadow.equipment == null || shadow.equipment.slots == null)
        {
            return;
        }

        // Store equipment only if it hasn't been stored before
        if (!storedEquipments.ContainsKey(shadow))
        {
            storedEquipments[shadow] = new List<ActorEquipmentSlot>();
            foreach (var slot in shadow.equipment.slots)
            {
                if (slot != null && slot.data != null && !IsBowOrStaff(slot.data) && !HasHighRange(slot.data))
                {
                    storedEquipments[shadow].Add(new ActorEquipmentSlot { type = slot.type, data = slot.data });
                }
            }
        }

        // Create a separate list to hold the slots that need to be emptied
        List<ActorEquipmentSlot> slotsToEmpty = new List<ActorEquipmentSlot>();

        // Identify slots to empty
        foreach (var slot in shadow.equipment.slots)
        {
            if (slot != null && slot.data != null && !IsBowOrStaff(slot.data) && !HasHighRange(slot.data))
            {
                slotsToEmpty.Add(slot);
            }
        }

        // Empty the identified slots
        foreach (var slot in slotsToEmpty)
        {
            slot.emptySlot();
            shadow.dirty_sprite_item = true;
            shadow.updateStats();
            // updatePos();
            // clearBeh();
        }
    }
}