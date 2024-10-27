using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(DropsLibrary))]
[HarmonyPatch("action_acid")]
public static class ActionAcidPatch
{
    [HarmonyPrefix]
    public static bool PrefixActionAcid(WorldTile pTile = null, string pDropID = null)
    {
        if (pTile != null)
        {
            // Custom acid behaviors executed before the original function
            MapAction.checkAcidTerraform(pTile);
            if (Toolbox.randomChance(0.2f))
            {
                World.world.particlesSmoke.spawn(pTile.posV3);
            }

            // Damage buildings if affected by acid
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
                if (actor != null && !actor.hasTrait("acid_proof") && !actor.hasTrait("acid_blood") && Toolbox.randomChance(0.6f))
                {
                    actor.getHit(20f);
                }
            }

            // ** Custom Transformation Logic ** - Ensure correct tile change and add to spreading behavior

            if (pTile.Type == AssetManager.tiles.get("pit_deep_ocean"))
            {
                WorldBehaviourAcidTiles.AddInitialAcidTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("deep_acid"));
                return false; // Prevent further processing
            }
            else if (pTile.Type == AssetManager.tiles.get("pit_close_ocean"))
            {
                WorldBehaviourAcidTiles.AddInitialAcidTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("close_acid"));
                return false;
            }
            else if (pTile.Type == AssetManager.tiles.get("pit_shallow_waters"))
            {
                WorldBehaviourAcidTiles.AddInitialAcidTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("shallow_acid"));
                return false;
            }
            else if (pTile.Type == AssetManager.tiles.get("deep_ocean"))
            {
                WorldBehaviourAcidTiles.AddInitialAcidTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("deep_acid"));
                return false;
            }
            else if (pTile.Type == AssetManager.tiles.get("shallow_waters"))
            {
                WorldBehaviourAcidTiles.AddInitialAcidTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("shallow_acid"));
                return false;
            }
            else if (pTile.Type == AssetManager.tiles.get("close_ocean"))
            {
                WorldBehaviourAcidTiles.AddInitialAcidTile(pTile);
                pTile.setTileType(AssetManager.tiles.get("close_acid"));
                return false;
            }
            // Continue with original logic only if custom conditions do not match
            return true;
        }

        return true; // Proceed with original function if pTile is null
    }
}
