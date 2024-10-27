using UnityEngine;


namespace Unit
{
    class EffectsLibrary1
    {
        public static void init()
        {

            EffectAsset DL = new EffectAsset();
            DL.id = "fx_teleport_dark";
            DL.use_basic_prefab = true;
            DL.sprite_path = "effects/fx_teleport_dark_t";
            DL.sorting_layer_id = "EffectsTop";
            DL.draw_light_area = true;
            DL.limit = 0;
            AssetManager.effects_library.add(DL);

            EffectAsset FE = new EffectAsset();
            FE.id = "fx_FE";
            FE.use_basic_prefab = true;
            FE.show_on_mini_map = true;
            FE.sprite_path = "effects/fx_black_fire_t";
            FE.sorting_layer_id = "EffectsTop";
            FE.draw_light_area = true;
            AssetManager.effects_library.add(FE);

            EffectAsset AR = new EffectAsset();
            AR.id = "fx_red_trail";
            AR.use_basic_prefab = true;
            AR.sprite_path = "effects/fx_red_trail";
            AR.sorting_layer_id = "EffectsTop";
            AR.draw_light_area = true;
            AR.limit = 0;
            AssetManager.effects_library.add(AR);

            EffectAsset WE = new EffectAsset();
            WE.id = "fx_teleport_WE";
            WE.use_basic_prefab = true;
            WE.sprite_path = "effects/fx_teleport_WE_t";
            WE.sorting_layer_id = "EffectsTop";
            WE.draw_light_area = true;
            WE.limit = 0;
            AssetManager.effects_library.add(WE);

            EffectAsset MC = new EffectAsset();
            MC.id = "fx_wind_trail_t";
            // MC.prefab_id = "effects/prefabs/PrefabWave";
            MC.use_basic_prefab = true;
            MC.sprite_path = "effects/fx_wind_trail_t";
            //    MC.spawn_action = new EffectAction(spawnSimpleTile);
            MC.sorting_layer_id = "EffectsTop";
            MC.draw_light_area = true;
            MC.draw_light_size = 0.5f;
            MC.limit = 0;
            AssetManager.effects_library.add(MC);

            EffectAsset BC = new EffectAsset();
            BC.id = "fx_dark_trail_t";
            BC.use_basic_prefab = true;
            BC.sprite_path = "effects/fx_dark_trail_t";
            BC.sorting_layer_id = "EffectsTop";
            BC.draw_light_area = true;
            BC.limit = 0;
            AssetManager.effects_library.add(BC);


            EffectAsset BBurn = new EffectAsset();
            BBurn.id = "fx_black_fire_t";
            BBurn.use_basic_prefab = true;
            BBurn.sprite_path = "effects/fx_black_fire_t";
            BBurn.sorting_layer_id = "EffectsTop";
            BBurn.draw_light_area = true;
            BBurn.limit = 0;
            AssetManager.effects_library.add(BBurn);
        }
        public static BaseEffect spawnSimpleTile(BaseEffect pEffect, WorldTile pTile, string pParam1 = null, string pParam2 = null, float pFloatParam1 = 0f)
        {
            // Spawning the effect on the tile
            pEffect.spawnOnTile(pTile);

            // Adjust the scale of the effect (e.g., making it smaller)
            pEffect.transform.localScale = new Vector3(0.5f / 7, 0.5f / 7, 0.5f / 7); // Adjust values to scale down the effect

            return pEffect;
        }
    }
}
