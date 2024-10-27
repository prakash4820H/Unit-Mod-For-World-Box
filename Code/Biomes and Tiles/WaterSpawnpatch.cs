using HarmonyLib;

[HarmonyPatch(typeof(WorldBehaviourActions), "updateUnitSpawn")]
public static class UnitSpawnPatch
{
    [HarmonyPrefix]
    public static bool Prefix()
    {
        // Check if world law allows spawning
        if (!World.world.worldLaws.world_law_animals_spawn.boolVal)
        {
            return false; // Prevents further execution if spawning is not allowed
        }

        // Check if there are any chunks loaded in the world
        if (World.world.mapChunkManager.list.Count == 0)
        {
            return false;
        }

        // Get a random island (not limited to ground-only)
        TileIsland randomIsland = World.world.islandsCalculator.islands.GetRandom(); // Use the general islands list
        if (randomIsland == null)
        {
            return false;
        }

        // Select a random tile from the island
        WorldTile randomTile = randomIsland.getRandomTile();
        if (randomTile == null)
        {
            return false;
        }

        BiomeAsset biome_asset = randomTile.Type.biome_asset;
        if (biome_asset == null || !biome_asset.spawn_units_auto)
        {
            return false;
        }

        // Proceed to spawn the unit
        string randomUnit = biome_asset.pool_units_spawn.GetRandom<string>();
        ActorAsset actorAsset = AssetManager.actor_library.get(randomUnit);
        if (actorAsset == null || actorAsset.currentAmount > actorAsset.maxRandomAmount)
        {
            return false;
        }

        // Check nearby units and spawn if there aren't too many
        World.world.getObjectsInChunks(randomTile, 0, MapObjectType.Actor);
        if (World.world.temp_map_objects.Count > 3)
        {
            return false;
        }

        // Spawn the new unit
        World.world.units.spawnNewUnit(actorAsset.id, randomTile, false, 6f);
        return false; // Prevents further original method execution
    }
}
