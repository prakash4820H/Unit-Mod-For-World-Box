using System.Collections.Generic;

public static class WorldBehaviourBloodTiles
{
    private static HashSet<WorldTile> _tilesToSpread = new HashSet<WorldTile>();
    private static float _spreadInterval = 0.2f;
    private static float _spreadChance = 0.15f;

    public static void InitializeBloodSpreading()
    {
        WorldBehaviourAsset BloodSpreadBehaviour = new WorldBehaviourAsset
        {
            id = "Blood_spreading",
            interval = _spreadInterval,
            interval_random = 0f,
            enabled_on_minimap = true,
            stop_when_world_on_pause = true,
            action = UpdateBloodTiles,
            action_clear = ClearBloodSpreading
        };
        BloodSpreadBehaviour.manager = new WorldBehaviour(BloodSpreadBehaviour);
        AssetManager.world_behaviours.add(BloodSpreadBehaviour);
    }

    private static void UpdateBloodTiles()
    {
        List<WorldTile> toAdd = new List<WorldTile>();

        foreach (WorldTile tile in _tilesToSpread)
        {
            if (Toolbox.randomChance(_spreadChance) && tile.Type.id != "removed")
            {
                CheckAndSpreadToAdjacent(tile, toAdd);
            }
        }

        foreach (WorldTile newTile in toAdd)
        {
            _tilesToSpread.Add(newTile);
        }
    }


    private static void CheckAndSpreadToAdjacent(WorldTile originTile, List<WorldTile> toAdd)
    {
        foreach (WorldTile neighbor in originTile.neighbours)
        {
            if (CanTransformToBlood(neighbor))
            {
                TileType BloodType = DetermineBloodTileType(neighbor);
                if (BloodType != null)
                {
                    MapAction.terraformTile(neighbor, BloodType, null);
                    toAdd.Add(neighbor);
                }
            }
        }
    }

    private static bool CanTransformToBlood(WorldTile pTile)
    {
        bool isPitTile = !pTile.Type.ocean &&
                         (pTile.Type == AssetManager.tiles.get("pit_deep_ocean") ||
                          pTile.Type == AssetManager.tiles.get("pit_close_ocean") ||
                          pTile.Type == AssetManager.tiles.get("pit_shallow_waters"));

        bool isOceanTile = (pTile.Type == AssetManager.tiles.get("deep_ocean") ||
                            pTile.Type == AssetManager.tiles.get("close_ocean") ||
                            pTile.Type == AssetManager.tiles.get("shallow_waters"));

        return isPitTile || isOceanTile;
    }

    private static TileType DetermineBloodTileType(WorldTile pTile)
    {
        if (pTile.Type == AssetManager.tiles.get("pit_deep_ocean"))
        {
            return AssetManager.tiles.get("deep_blood");
        }
        else if (pTile.Type == AssetManager.tiles.get("pit_close_ocean"))
        {
            return AssetManager.tiles.get("close_blood");
        }
        else if (pTile.Type == AssetManager.tiles.get("pit_shallow_waters"))
        {
            return AssetManager.tiles.get("shallow_blood");
        }
        else if (pTile.Type == AssetManager.tiles.get("deep_ocean"))
        {
            return AssetManager.tiles.get("deep_blood");
        }
        else if (pTile.Type == AssetManager.tiles.get("close_ocean"))
        {
            return AssetManager.tiles.get("close_blood");
        }
        else if (pTile.Type == AssetManager.tiles.get("shallow_waters"))
        {
            return AssetManager.tiles.get("shallow_blood");
        }
        return null;
    }

    public static void RemoveBloodTile(WorldTile tile)
    {
        _tilesToSpread.Remove(tile);
        // Ensure the tile does not spread again by setting it to a non-spreading type
        if (tile.Type.id == "deep_blood" || tile.Type.id == "close_blood" || tile.Type.id == "shallow_blood")
        {
            tile.setTileType(AssetManager.tiles.get("pit_shallow_waters"));
        }
    }

    private static void ClearBloodSpreading()
    {
        // Properly clear the blood spreading tiles when transitioning maps
        _tilesToSpread.Clear();
    }
    public static HashSet<WorldTile> GetTilesToSpread()
    {
        return _tilesToSpread;
    }

    public static void AddInitialBloodTile(WorldTile tile)
    {
        if (tile != null && CanTransformToBlood(tile))
        {
            _tilesToSpread.Add(tile);
        }
    }
}
