using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;
using HarmonyLib;
using System.Reflection;
using Newtonsoft.Json;
using Beebyte.Obfuscator;
using System.Text;
using System.Threading.Tasks;
using static Config;
using UnityEngine.UI;
using UnityEngine.Events;
using ai;
namespace Unit
{

    class LibraryBaseEffects : Library<EffectAsset>
    {
        public EffectsLibrary baseLib;

        public override void init()
        {
            base.init();
            this.baseLib = AssetManager.effects_library;
            base.lib = this.baseLib;

            this.add(new EffectAsset
            {
                id = "fx_explosion_borgir_bomb",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsTop",
                sprite_path = "effects/fx_explosion_borgir_bomb",
                show_on_mini_map = true,
                draw_light_area = true,
                sound_launch = "event:/SFX/EXPLOSIONS/ExplosionCrabBomb"
            });


            this.add(new EffectAsset
            {
                id = "fx_wind_trail_t",
                use_basic_prefab = true,
                sorting_layer_id = "EffectsTop",
                sprite_path = "effects/fx_wind_trail_t",
                show_on_mini_map = true,
                draw_light_area = true,
                sound_launch = "event:/SFX/EXPLOSIONS/ExplosionCrabBomb"
            });
        }
    }
}
