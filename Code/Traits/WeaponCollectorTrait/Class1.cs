using HarmonyLib;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class WeaponCollectorTrait : MonoBehaviour
{
    private Actor actor;
    private Queue<ItemData> storedWeapons = new Queue<ItemData>(); // Queue to store collected weapons
    private float detectionRadius = 10f; // Radius to detect nearby enemies
    private float checkInterval = 2f; // Time between checks for nearby enemies
    private float lastCheckTime = 0f;
    private Dictionary<Actor, ItemData> storedEquipment = new Dictionary<Actor, ItemData>(); // Store enemy weapons

    private void Start()
    {
        actor = GetComponent<Actor>();
    }

    private void Update()
    {
        if (actor == null || !actor.isAlive()) return;

        if (Time.time - lastCheckTime >= checkInterval)
        {
            HandleNearbyEnemies();
            lastCheckTime = Time.time;
        }
    }

    private void HandleNearbyEnemies()
    {
        List<Actor> nearbyEnemies = FindNearbyEnemies();
        if (nearbyEnemies.Count > 0)
        {
            // Equip a stored weapon if there are enemies nearby
            EquipStoredWeapon();
        }
        else
        {
            // Clear inventory if no enemies are nearby
            ClearInventory();
        }
    }

    private List<Actor> FindNearbyEnemies()
    {
        List<Actor> nearbyEnemies = new List<Actor>();
        List<Actor> allActors = MapBox.instance.units.getSimpleList();

        foreach (Actor otherActor in allActors)
        {
            if (IsEnemy(otherActor) && Vector2.Distance(actor.currentPosition, otherActor.currentPosition) <= detectionRadius)
            {
                nearbyEnemies.Add(otherActor);
            }
        }
        return nearbyEnemies;
    }

    private bool IsEnemy(Actor otherActor)
    {
        return actor.kingdom.isEnemy(otherActor.kingdom);
    }

    private void EquipStoredWeapon()
    {
        if (storedWeapons.Count == 0)
        {
            Debug.Log("No weapons stored.");
            return;
        }

        // Equip the next weapon in the queue
        ItemData nextWeapon = storedWeapons.Dequeue();
        ActorEquipmentSlot weaponSlot = actor.equipment.getSlot(EquipmentType.Weapon);

        if (weaponSlot != null && nextWeapon != null)
        {
            weaponSlot.setItem(nextWeapon);
            actor.setStatsDirty();
            actor.dirty_sprite_item = true;
            Debug.Log($"Equipped weapon: {nextWeapon.name}");
        }
        else
        {
            Debug.Log("Failed to equip weapon. Slot or weapon is null.");
        }

        // Rotate the weapon back into the queue for future use
        storedWeapons.Enqueue(nextWeapon);
    }

    private void ClearInventory()
    {
        // Empty the actor's current weapon slot
        ActorEquipmentSlot weaponSlot = actor.equipment.getSlot(EquipmentType.Weapon);
        if (weaponSlot != null)
        {
            weaponSlot.emptySlot();
            actor.setStatsDirty();
            Debug.Log("Cleared actor's weapon slot.");
        }
    }

    public void OnKill(Actor enemy)
    {
        // Collect the enemy's weapon
        if (enemy.equipment != null)
        {
            ActorEquipmentSlot enemyWeaponSlot = enemy.equipment.getSlot(EquipmentType.Weapon);
            if (enemyWeaponSlot != null && enemyWeaponSlot.data != null)
            {
                storedWeapons.Enqueue(enemyWeaponSlot.data);
                enemyWeaponSlot.emptySlot();
                enemy.setStatsDirty();
                Debug.Log($"Stored weapon from enemy: {enemyWeaponSlot.data.name}");
            }
            else
            {
                Debug.Log("Enemy has no weapon to store.");
            }
        }

        // Immediately equip the next available weapon after a kill
        ClearInventory();
        EquipStoredWeapon();
    }

    private void RestoreEquipment()
    {
        if (storedWeapons.Count > 0)
        {
            EquipStoredWeapon();
        }
    }
}

[HarmonyPatch(typeof(Actor))]
public class WeaponCollectorTraitInit
{
    public static void Init()
    {
        ActorTrait trait = new ActorTrait
        {
            id = "weapon_collector_trait",
            path_icon = "ui/Icons/weapon_collector_trait_icon",
            group_id = UnitTraitGroup.Unit,
            action_special_effect = ApplyWeaponCollector
        };

        AssetManager.traits.add(trait);
        LocalizationUtility.addTraitToLocalizedLibrary(trait.id, "Collects and switches weapons from killed enemies.");
        PlayerConfig.unlockTrait("weapon_collector_trait");
    }

    public static bool ApplyWeaponCollector(BaseSimObject target = null, WorldTile tile = null)
    {
        Actor actor = target as Actor;
        if (actor == null || !actor.hasTrait("weapon_collector_trait"))
        {
            return false;
        }
        WeaponCollectorTrait weaponCollector = actor.gameObject.GetComponent<WeaponCollectorTrait>();
        if (weaponCollector == null)
        {
            weaponCollector = actor.gameObject.AddComponent<WeaponCollectorTrait>();
        }
        return true;
    }
}
