using UnityEngine;
using System.Collections.Generic;

namespace Unit
{
    class UnitBehaviour1 : MonoBehaviour
    {
        public static void init()
        {
            // Initialize fully to avoid unassigned variable error
            WorldBehaviourAsset BloodToCandyTransformation = new WorldBehaviourAsset
            {
                id = "blood_to_candy_transformation",
                enabled_on_minimap = false,
                interval = 2.0f,
                interval_random = 1.0f,
                action = BloodToCandyAction,
            };
            BloodToCandyTransformation.manager = new WorldBehaviour(BloodToCandyTransformation);
            AssetManager.world_behaviours.add(BloodToCandyTransformation);
        }

        public static void BloodToCandyAction()
        {
            foreach (WorldTile tile in MapBox.instance.tilesList)
            {
                // Ensure tile is "soil_low" type
                if (tile.Type == AssetManager.tiles.get("soil_low"))
                {
                    foreach (WorldTile neighbor in tile.neighboursAll)
                    {
                        // Check if neighbor is a blood tile type
                        if (neighbor.Type == AssetManager.tiles.get("shadow_blood") ||
                            neighbor.Type == AssetManager.tiles.get("close_blood") ||
                            neighbor.Type == AssetManager.tiles.get("deep_blood"))
                        {
                            // Transform using setTileType and apply candy_low top tile
                            tile.setTileType(AssetManager.tiles.get("soil_low")); // Ensures base type compatibility
                            tile.setTileTypes("soil_low", AssetManager.topTiles.get("wasteland_low"));
                            break;
                        }
                    }
                }
            }
        }
    }
}
