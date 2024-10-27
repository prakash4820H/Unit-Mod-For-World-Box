using System.Collections.Generic;

public static class ActionBlood
{
    public static void ActionBloodTile(WorldTile pTile = null, string pDropID = null)
    {
        if (pTile != null)
        {
            // Custom blood behaviors executed before the original function
            checkBloodTerraform(pTile);
            if (Toolbox.randomChance(0.2f))
            {
                World.world.particlesSmoke.spawn(pTile.posV3);
            }

            // Damage buildings if affected by blood (mimicking acid behavior)
            if (pTile.building != null && pTile.building.asset.affectedByAcid && pTile.building.isAlive())
            {
                pTile.building.getHit(20f);
            }

            // Interact with actors using corrected list access
            World.world.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
            List<BaseSimObject> actors = World.world.temp_map_objects;
            foreach (BaseSimObject obj in actors)
            {
                Actor actor = obj as Actor;
                if (actor != null && !actor.hasTrait("acid_proof") && Toolbox.randomChance(0.6f))
                {
                    actor.getHit(20f);
                }
            }

            // Transformation logic for blood tiles
            if (pTile.Type == AssetManager.tiles.get("pit_deep_ocean"))
            {
                WorldBehaviourBloodTiles.AddInitialBloodTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("deep_blood"));
            }
            else if (pTile.Type == AssetManager.tiles.get("pit_close_ocean"))
            {
                WorldBehaviourBloodTiles.AddInitialBloodTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("close_blood"));
            }
            else if (pTile.Type == AssetManager.tiles.get("pit_shallow_waters"))
            {
                WorldBehaviourBloodTiles.AddInitialBloodTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("shallow_blood"));
            }
            else if (pTile.Type == AssetManager.tiles.get("deep_ocean"))
            {
                WorldBehaviourBloodTiles.AddInitialBloodTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("deep_blood"));
            }
            else if (pTile.Type == AssetManager.tiles.get("close_ocean"))
            {
                WorldBehaviourBloodTiles.AddInitialBloodTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("close_blood"));
            }
            else if (pTile.Type == AssetManager.tiles.get("shallow_waters"))
            {
                WorldBehaviourBloodTiles.AddInitialBloodTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("shallow_blood"));
            }
        }
    }

    public static void checkBloodTerraform(WorldTile pTile)
    {
        if (pTile.isTemporaryFrozen())
        {
            pTile.unfreeze(99);
        }
        if (pTile.top_type != null && pTile.top_type.wasteland)
        {
            return;
        }
        if (pTile.top_type != null)
        {
            MapAction.decreaseTile(pTile);
        }
        else if (pTile.Type.ground)
        {
            if (pTile.isTileRank(TileRank.Low))
            {
                MapAction.terraformTop(pTile, TopTileLibrary.wasteland_low);
            }
        }
    }
}
