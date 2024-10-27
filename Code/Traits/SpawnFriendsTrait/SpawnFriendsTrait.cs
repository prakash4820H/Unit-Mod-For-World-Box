using System.Collections.Generic;
using Unit;
using UnityEngine;

public class SpawnFriendsTrait : MonoBehaviour
{
    private Actor actor;
    private float detectionRadius = 10f; // Adjust as needed
    private Dictionary<Actor, List<Actor>> spawnedFriends = new Dictionary<Actor, List<Actor>>();
    private HashSet<string> allowedAssetIds = new HashSet<string> { "unit_human", "unit_orc", "unit_dwarf", "unit_elf", "demon", "whiteMage", "evilMage", "skeleton", "bandit", "necromancer", "plagueDoctor" };


    void Start()
    {
        actor = GetComponent<Actor>();
        if (actor == null)
        {
            Debug.LogError("Actor component is missing.");
            return;
        }

        // Check if the actor's asset ID is in the allowed list
        if (!allowedAssetIds.Contains(actor.asset.id))
        {
            Destroy(this); // Remove this component if the actor's asset ID is not allowed
            return;
        }
    }

    void Update()
    {
        DetectAndSpawnFriends();
    }

    private void DetectAndSpawnFriends()
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, detectionRadius);
        foreach (Actor enemy in nearbyEnemies)
        {
            if (!spawnedFriends.ContainsKey(enemy) && allowedAssetIds.Contains(enemy.asset.id))
            {
                SpawnFriendsForEnemy(enemy);
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
        return actor.kingdom.isEnemy(potentialEnemy.kingdom); // Adjust based on actual method/property for enemy determination
    }

    private void SpawnFriendsForEnemy(Actor enemy)
    {
        List<Actor> friends = new List<Actor>();
        for (int i = 0; i < 2; i++)
        {
            Actor friend = MapBox.instance.units.createNewUnit(actor.asset.id, actor.currentTile);
            CopyStatsAndTraits(enemy, friend);
            CopyEquipment(enemy, friend);
            AddFollowerBehavior(friend);
            friend.data.gender = enemy.data.gender;
            friend.data.head = enemy.data.head;
            friend.data.skin = enemy.data.skin;
            friend.data.skin_set = enemy.data.skin_set;
            friends.Add(friend);
            friend.kingdom = actor.kingdom;
            friend.getMaxHealth();
        }
        // ArrangeFriendsInCircle(friends, actor);
        spawnedFriends[enemy] = friends;
    }
    /*
    private void ArrangeFriendsInCircle(List<Actor> friends, Actor mainActor)
    {
        int numberOfFriends = friends.Count;
        float radius = 3f; // Radius of the circle around the main unit, adjust as needed for spacing

        for (int i = 0; i < numberOfFriends; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfFriends;
            Vector2 newPosition = new Vector2(
                mainActor.currentPosition.x + Mathf.Cos(angle) * radius,
                mainActor.currentPosition.y + Mathf.Sin(angle) * radius
            );

            friends[i].currentPosition = newPosition;
            friends[i].updatePos();
        }
    }
    */

    private void CopyStatsAndTraits(Actor enemy, Actor friend)
    {
        friend.asset.base_stats.CopyFrom(actor.asset.base_stats);
        foreach (var trait in enemy.data.traits)
        {
            if (trait != "madness") // Exclude the madness trait
            {
                friend.addTrait(trait);
            }
        }
        /*
        // Ensure the friend's race matches the enemy's race
        if (friend.asset.race != enemy.asset.race)
        {
            friend.asset.race = enemy.asset.race;
        }

        // Copy skin and skin_set
        friend.data.skin = enemy.data.skin;
        friend.data.skin_set = enemy.data.skin_set;
        */
    }

    private void CopyEquipment(Actor enemy, Actor friend)
    {
        friend.equipment = new ActorEquipment();
        foreach (var slot in enemy.equipment.slots)
        {
            if (slot.data != null)
            {
                // Correctly add item to the friend's equipment
                friend.equipment.getSlot(slot.type).setItem(slot.data);
            }
        }
    }
    private void AddFollowerBehavior(Actor friend)
    {
        // Cancel all existing behaviors
        friend.cancelAllBeh();

        // Add follower behavior
        FriendFollowerBehaviour follower = friend.gameObject.AddComponent<FriendFollowerBehaviour>();
        follower.SetTarget(actor); // Set the main unit as the target to follow
    }
}

public static class SpawnFriendsTraitInit
{
    public static void Init()
    {
        ActorTrait trait = new ActorTrait
        {
            id = "spawn_friends_trait",
            path_icon = "ui/Icons/icon_spawn_friends",
            group_id = UnitTraitGroup.Unit,
            action_special_effect = ApplyTrait,
            opposite = "madness"
        };
        AssetManager.traits.add(trait);
        LocalizationUtility.addTraitToLocalizedLibrary(trait.id, "Spawns two friends for each nearby enemy.");
        PlayerConfig.unlockTrait("spawn_friends_trait");
    }

    public static bool ApplyTrait(BaseSimObject pSelf, WorldTile pTile = null)
    {
        Actor actor = pSelf as Actor;
        if (actor == null || !actor.hasTrait("spawn_friends_trait"))
        {
            return false;
        }
        SpawnFriendsTrait component = actor.gameObject.GetComponent<SpawnFriendsTrait>();
        if (component == null)
        {
            component = actor.gameObject.AddComponent<SpawnFriendsTrait>();
        }
        return true;
    }
}