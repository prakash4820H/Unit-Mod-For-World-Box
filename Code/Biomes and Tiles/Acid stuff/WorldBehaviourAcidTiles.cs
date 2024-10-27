using System.Collections.Generic;

public static class WorldBehaviourAcidTiles
{
    private static HashSet<WorldTile> _tilesToSpread = new HashSet<WorldTile>();
    private static float _spreadInterval = 0.05f;
    private static float _spreadChance = 0.15f;

    public static void InitializeAcidSpreading()
    {
        WorldBehaviourAsset AcidSpreadBehaviour = new WorldBehaviourAsset
        {
            id = "Acid_spreading",
            interval = _spreadInterval,
            interval_random = 0f,
            enabled_on_minimap = true,
            stop_when_world_on_pause = true,
            action = UpdateAcidTiles,
            action_clear = ClearAcidSpreading
        };
        AcidSpreadBehaviour.manager = new WorldBehaviour(AcidSpreadBehaviour);
        AssetManager.world_behaviours.add(AcidSpreadBehaviour);
    }

    private static void UpdateAcidTiles()
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
            if (CanTransformToAcid(neighbor))
            {
                TileType AcidType = DetermineAcidTileType(neighbor);
                if (AcidType != null)
                {
                    MapAction.terraformTile(neighbor, AcidType, null);
                    toAdd.Add(neighbor);
                }
            }
        }
    }

    private static bool CanTransformToAcid(WorldTile pTile)
    {
        bool isPitTile = !pTile.Type.ocean &&
                         (pTile.Type == AssetManager.tiles.get("pit_deep_ocean") ||
                          pTile.Type == AssetManager.tiles.get("pit_close_ocean") ||
                          pTile.Type == AssetManager.tiles.get("pit_shallow_waters"));

        bool isOceanTile = (pTile.Type == AssetManager.tiles.get("deep_ocean") ||
                            pTile.Type == AssetManager.tiles.get("close_ocean") ||
                            pTile.Type == AssetManager.tiles.get("shallow_waters"));

        bool isLavaTile = (pTile.Type.layerType == TileLayerType.Lava);

        return isPitTile || isOceanTile || isLavaTile;
    }

    private static TileType DetermineAcidTileType(WorldTile pTile)
    {
        if (pTile.Type == (TileType)AssetManager.tiles.get("pit_deep_ocean"))
        {
            return (TileType)AssetManager.tiles.get("deep_acid");
        }
        else if (pTile.Type == (TileType)AssetManager.tiles.get("pit_close_ocean"))
        {
            return (TileType)AssetManager.tiles.get("close_acid");
        }
        else if (pTile.Type == (TileType)AssetManager.tiles.get("pit_shallow_waters"))
        {
            return (TileType)AssetManager.tiles.get("shallow_acid");
        }
        return null;
    }

    public static void ClearAcidSpreading()
    {
        _tilesToSpread.Clear();
    }

    public static void AddInitialAcidTile(WorldTile tile)
    {
        if (tile != null && CanTransformToAcid(tile))
        {
            _tilesToSpread.Add(tile);
        }
    }

    public static void RemoveAcidTile(WorldTile tile)
    {
        _tilesToSpread.Remove(tile);
        // Ensure the tile does not spread again by setting it to a non-spreading type
        if (tile.Type.id == "deep_acid" || tile.Type.id == "close_acid" || tile.Type.id == "shallow_acid")
        {
            tile.setTileType(AssetManager.tiles.get("pit_shallow_waters"));
        }
    }


    public static HashSet<WorldTile> GetTilesToSpread()
    {
        return _tilesToSpread;
    }
}