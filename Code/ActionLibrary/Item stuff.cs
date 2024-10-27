using System.Collections.Generic;
using System.Linq;

public static class ItemManager
{
    // Store the strongest items by equipment type
    private static Dictionary<EquipmentType, ItemData> strongestItemsOnMap = new Dictionary<EquipmentType, ItemData>();

    // Store items for each actor
    private static Dictionary<Actor, List<ItemData>> actorItems = new Dictionary<Actor, List<ItemData>>();

    // Method to assign strongest items to an actor on the map
    public static void AssignStrongestItemsToActor(WorldTile pTile, string pDropID = null)
    {
        // Update the strongest items list before assigning
        UpdateStrongestItemsOnMap();

        // Ensure the tile is valid
        if (pTile == null) return;

        // List of all actors in the world
        List<Actor> allActors = World.world.units.getSimpleList();

        // Find all actors within a 3-tile radius (same as previous methods)
        List<Actor> actorsInRange = allActors
            .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= 3f)
            .ToList();

        if (actorsInRange.Count == 0) return;

        // Pick a random actor from the list of actors in range
        Actor selectedActor = actorsInRange[UnityEngine.Random.Range(0, actorsInRange.Count)];

        // Ensure the actor exists and has an asset to assign items to
        if (selectedActor == null || selectedActor.asset == null) return;

        // Remove previously assigned items, if any
        if (actorItems.ContainsKey(selectedActor))
        {
            RemoveItemsFromActor(selectedActor);
        }

        // Assign the strongest items to the selected actor
        GiveItemsToActor(selectedActor, strongestItemsOnMap.Values.ToList());
    }

    // Method to update the list of strongest items on the map
    private static void UpdateStrongestItemsOnMap()
    {
        // Clear previous strongest items
        strongestItemsOnMap.Clear();

        // Iterate through all actors on the map to find the strongest equipment
        foreach (Actor actor in World.world.units)
        {
            if (actor.equipment == null) continue;

            // Get all equipment slots for the actor
            List<ActorEquipmentSlot> equipmentSlots = ActorEquipment.getList(actor.equipment);
            if (equipmentSlots == null) continue;

            // Evaluate each equipment slot for the strongest item
            foreach (ActorEquipmentSlot slot in equipmentSlots)
            {
                if (slot.data == null) continue;

                ItemAsset itemAsset = AssetManager.items.get(slot.data.id.ToString()); // Convert ID to string
                if (itemAsset == null) continue;

                EquipmentType equipmentType = itemAsset.equipmentType;
                int itemValue = GetItemValue(slot.data);

                // Check if the item is stronger than the current strongest item of its type
                if (!strongestItemsOnMap.ContainsKey(equipmentType) || itemValue > GetItemValue(strongestItemsOnMap[equipmentType]))
                {
                    strongestItemsOnMap[equipmentType] = slot.data;
                }
            }
        }
    }

    // Method to calculate the value of an item
    private static int GetItemValue(ItemData itemData)
    {
        // Use ItemTools to calculate the item's value
        if (itemData == null) return 0;

        ItemTools.calcItemValues(itemData);
        return ItemTools.s_value;  // The value calculated by the game logic
    }

    // Assign the items to the actor
    private static void GiveItemsToActor(Actor actor, List<ItemData> items)
    {
        if (!actorItems.ContainsKey(actor))
        {
            actorItems.Add(actor, new List<ItemData>());
        }

        // Add the items to the actor's inventory or equipment
        foreach (var item in items)
        {
            ItemAsset itemAsset = AssetManager.items.get(item.id.ToString()); // Convert ID to string
            if (itemAsset == null) continue;

            ActorEquipmentSlot slot = actor.equipment.getSlot(itemAsset.equipmentType);
            if (slot == null || GetItemValue(item) > GetItemValue(slot.data))
            {
                slot.setItem(item);
                actor.setStatsDirty();
                actor.dirty_sprite_item = true;
            }
        }

        // Track assigned items
        actorItems[actor] = items;
    }

    // Remove items from the actor
    private static void RemoveItemsFromActor(Actor actor)
    {
        if (actorItems.ContainsKey(actor))
        {
            foreach (var item in actorItems[actor])
            {
                ItemAsset itemAsset = AssetManager.items.get(item.id.ToString()); // Convert ID to string
                if (itemAsset == null) continue;

                ActorEquipmentSlot slot = actor.equipment.getSlot(itemAsset.equipmentType);
                if (slot != null)
                {
                    slot.emptySlot();
                }
            }
            actorItems.Remove(actor);  // Clear the actor's item list
        }
    }
}

// ItemTools class update for mergeStatsWithItem
public class ItemTools
{
    public static int s_value = 0; // Define the static field to store the calculated item value
    public static ItemQuality s_quality; // Define the item quality
    public static BaseStats s_stats = new BaseStats(); // Base stats for calculations

    // Method to calculate item values
    public static void calcItemValues(ItemData pItemData)
    {
        s_stats.clear(); // Clear the stats
        s_value = 0; // Reset the value
        s_quality = ItemQuality.Normal; // Default item quality

        // Merge the stats of the item
        mergeStatsWithItem(s_stats, pItemData);
    }

    // Method to merge item stats
    public static void mergeStatsWithItem(BaseStats pStats, ItemData pItemData, bool pCalcGlobalValue = true)
    {
        // Get the ItemAsset from the item data
        ItemAsset itemAsset = AssetManager.items.get(pItemData.id.ToString()); // Convert ID to string
        ItemAsset itemMaterialLibrary = getItemMaterialLibrary(itemAsset.equipmentType, pItemData.material.ToString()); // Convert material to string

        // Merge stats from the item asset
        mergeStats(pStats, itemAsset, pCalcGlobalValue);

        // Merge stats from the material library
        if (itemMaterialLibrary != null)
        {
            mergeStats(pStats, itemMaterialLibrary, pCalcGlobalValue);
        }

        // Handle any additional modifiers for the item
        foreach (string modifier in pItemData.modifiers)
        {
            ItemAsset itemAssetModifier = AssetManager.items_modifiers.get(modifier);
            if (itemAssetModifier != null)
            {
                mergeStats(pStats, itemAssetModifier, pCalcGlobalValue);
            }
        }
    }

    // Dummy method for merging stats (you should replace this with the actual logic from the game)
    private static void mergeStats(BaseStats pStats, ItemAsset pAsset, bool pCalcGlobalValue)
    {
        if (pAsset == null) return;
        pStats.mergeStats(pAsset.base_stats); // Assuming base_stats is part of the item asset

        if (pCalcGlobalValue)
        {
            if (pAsset.quality > s_quality) s_quality = pAsset.quality;
            s_value += pAsset.equipment_value + pAsset.mod_rank * 5; // Calculate the value of the item
        }
    }

    // Get the item material library based on the equipment type and material (replace with your logic)
    private static ItemAsset getItemMaterialLibrary(EquipmentType equipmentType, string material) // Material is now string
    {
        // Retrieve the material asset from AssetManager (this is just an example)
        switch (equipmentType)
        {
            case EquipmentType.Helmet:
            case EquipmentType.Armor:
            case EquipmentType.Boots:
                return AssetManager.items_material_armor.get(material);
            case EquipmentType.Weapon:
                return AssetManager.items_material_weapon.get(material);
            case EquipmentType.Ring:
            case EquipmentType.Amulet:
                return AssetManager.items_material_accessory.get(material);
            default:
                return null;
        }
    }
}
