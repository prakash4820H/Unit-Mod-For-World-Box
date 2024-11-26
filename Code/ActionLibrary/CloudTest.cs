using System.Collections.Generic;
using UnityEngine;
using NCMS;

namespace Unit
{
    class CustomCloudLibrary : AssetLibrary<CloudAsset>
    {
        public CloudLibrary baseLib;
        public override void init()
        {
            List<string> list4 = List.Of<string>("effects/MudCloud");

            CloudAsset customCloud = AssetManager.clouds.get("cloud_rage");
            customCloud.path_sprites = list4;
        }

        public override void post_init()
        {
            base.post_init();
            foreach (CloudAsset cloudAsset in this.list)
            {
                cloudAsset.cached_sprites = new List<Sprite>();
                foreach (string pPath in cloudAsset.path_sprites)
                {
                    Sprite sprite = SpriteTextureLoader.getSprite(pPath);
                    if (!(sprite == null))
                    {
                        cloudAsset.cached_sprites.Add(sprite);
                    }
                }
            }
        }
    }
}