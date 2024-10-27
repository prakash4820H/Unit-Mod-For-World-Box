public class DisasterWaterMage1
{
    private const string RequiredTraitId = "immortal_trait"; // Replace with the actual trait ID
    private const int RequiredTraitCount = 5; // Set the required number of actors with the trait

    public static void InitWaterMageDisaster()
    {
        // Create a new water-based disaster asset
        DisasterAsset waterMageDisaster = new DisasterAsset
        {
            id = "water_mage_disaster1",
            rate = 2, // Controls how often it appears
            chance = 0.8f, // Probability of occurrence
            min_world_population = 200,
            min_world_cities = 3,
            world_log = "worldlog_disaster_demon_mage",
            world_log_icon = "iconEvilMage",
            spawn_asset_unit = "piranha", // The unit to spawn
            max_existing_units = 3, // Maximum number of Water Mages at any time
            units_min = 1,
            units_max = 2,
            type = DisasterType.Other,
            premium_only = false
        };

        // Add to the Disaster Library
        AssetManager.disasters.add(waterMageDisaster);

        // Set the action to spawn Water Mages on water tiles
        waterMageDisaster.action = SpawnWaterMageDisaster;
    }

    // Spawning logic for Water Mage
    public static void SpawnWaterMageDisaster(DisasterAsset pAsset)
    {
        // Check if there are enough actors with the required trait
        if (!HasSufficientTraitCount(RequiredTraitId, RequiredTraitCount))
        {
            return; // Exit if there are not enough actors with the specific trait
        }

        // Get a random tile in the world
        WorldTile randomTile = World.world.tilesList.GetRandom();

        // Ensure it spawns only on water tiles (deep ocean, shallow waters, close ocean)
        if (randomTile.Type.IsType("deep_ocean") || randomTile.Type.IsType("close_ocean") || randomTile.Type.IsType("shallow_waters"))
        {
            Actor waterMage = World.world.units.createNewUnit(pAsset.spawn_asset_unit, randomTile);
            WorldLog.logDisaster(pAsset, randomTile); // Log the disaster in the world log
        }
    }

    // Method to check if there are enough actors with the specified trait on the map
    private static bool HasSufficientTraitCount(string traitId, int requiredCount)
    {
        int count = 0;
        foreach (var actor in World.world.units) // Iterate over all units in the game
        {
            if (actor.hasTrait(traitId))
            {
                count++;
                if (count >= requiredCount)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
