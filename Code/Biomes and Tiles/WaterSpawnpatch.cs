using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

[HarmonyPatch(typeof(WorldBehaviourActions), "updateUnitSpawn")]
public static class UnitSpawnPatch
{
    private static float waterSpawnInterval = 0.5f; // Interval for water spawns
    private static float waterSpawnIntervalRandom = 1.0f; // Random interval variation

    [HarmonyPrefix]
    public static bool Prefix()
    {
        if (!World.world.worldLaws.world_law_animals_spawn.boolVal) return true;
        if (World.world.mapChunkManager.list.Count == 0) return true;

        List<WorldTile> waterTiles = new List<WorldTile>();
        foreach (var tile in World.world.tilesList)
        {
            if (tile.Type.liquid && tile.Type.ocean) // Focus on ocean tiles only
            {
                waterTiles.Add(tile);
            }
        }
        if (waterTiles.Count == 0) return true; // Run original method if no water tiles

        for (int attempt = 0; attempt < 10; attempt++)
        {
            WorldTile randomTile = waterTiles[UnityEngine.Random.Range(0, waterTiles.Count)];
            BiomeAsset biomeAsset = randomTile.Type.biome_asset;

            if (biomeAsset == null || !biomeAsset.spawn_units_auto) continue;

            string randomUnit = biomeAsset.pool_units_spawn.GetRandom<string>();
            ActorAsset actorAsset = AssetManager.actor_library.get(randomUnit);

            if (actorAsset == null || actorAsset.currentAmount > actorAsset.maxRandomAmount) continue;

            bool unitNearby = false;
            foreach (WorldTile neighbor in randomTile.neighboursAll)
            {
                if (neighbor == null) continue;

                foreach (Actor unit in neighbor._units)
                {
                    if (unit.asset.id == actorAsset.id)
                    {
                        unitNearby = true;
                        break;
                    }
                }
                if (unitNearby) break;
            }

            if (unitNearby) continue;

            World.world.units.spawnNewUnit(actorAsset.id, randomTile, false, 6f);
        }

        // Allow original spawns for non-water tiles by running the original method
        return true;
    }
}
