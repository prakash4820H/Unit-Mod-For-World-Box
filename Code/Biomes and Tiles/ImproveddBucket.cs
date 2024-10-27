using HarmonyLib;

[HarmonyPatch(typeof(PowerLibrary))]
[HarmonyPatch("drawBucket")]
public static class DrawBucketPatch
{
    [HarmonyPrefix]
    public static bool PrefixDrawBucket(WorldTile pTile)
    {
        if (pTile.Type.id == "deep_acid")
        {
            // Remove from acid spreading management
            WorldBehaviourAcidTiles.RemoveAcidTile(pTile);
            pTile.setTileType(AssetManager.tiles.get("pit_deep_ocean"));
            return false;
        }
        else if (pTile.Type.id == "close_acid")
        {
            WorldBehaviourAcidTiles.RemoveAcidTile(pTile);
            pTile.setTileType(AssetManager.tiles.get("pit_close_ocean"));
            return false;
        }
        else if (pTile.Type.id == "shallow_acid")
        {
            WorldBehaviourAcidTiles.RemoveAcidTile(pTile);
            pTile.setTileType(AssetManager.tiles.get("pit_shallow_waters"));
            return false;
        }
        else if (pTile.Type.id == "deep_blood")
        {
            // Remove from blood spreading management
            WorldBehaviourBloodTiles.RemoveBloodTile(pTile);
            pTile.setTileType(AssetManager.tiles.get("pit_deep_ocean"));
            return false;
        }
        else if (pTile.Type.id == "close_blood")
        {
            WorldBehaviourBloodTiles.RemoveBloodTile(pTile);
            pTile.setTileType(AssetManager.tiles.get("pit_close_ocean"));
            return false;
        }
        else if (pTile.Type.id == "shallow_blood")
        {
            WorldBehaviourBloodTiles.RemoveBloodTile(pTile);
            pTile.setTileType(AssetManager.tiles.get("pit_shallow_waters"));
            return false;
        }

        // Handle tiles that can be removed with a bucket
        if (pTile.Type.lava || pTile.Type.canBeRemovedWithBucket)
        {
            MapAction.decreaseTile(pTile);
        }

        return true;
    }
}
