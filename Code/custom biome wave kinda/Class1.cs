﻿using System.Collections.Generic;
public class WorldBehaviourGrassWaves
{
    public static void startWaves()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnWave();
        }
    }
    public static void spawnWave()
    {
        if (World.world.zone_camera.zones.Count == 0)
        {
            return;
        }
        TileZone random = World.world.zone_camera.zones.GetRandom<TileZone>();
        if (random.tiles.Count == 0)
        {
            return;
        }
        WorldTile randomTile = random.tiles.GetRandom<WorldTile>();
        if (randomTile.Type.biome_id == ST.biome_grass)
        {
            EffectsLibrary.spawn("fx_wind_trail_t", randomTile, null, null, 0f, -1f, -1f);
        }
    }
    public static void checkTile(WorldTile tTile, int pRadius)
    {
        BaseEffectController baseEffectController = World.world.stackEffects.get("fx_wind_trail_t");
        List<BaseEffect> list = baseEffectController.getList();
        for (int i = 0; i < list.Count; i++)
        {
            BaseEffect baseEffect = list[i];
            if (Toolbox.Dist(baseEffect.transform.position.x, baseEffect.transform.position.y, (float)tTile.pos.x, (float)tTile.pos.y) <= (float)pRadius)
            {
                baseEffectController.killObject(baseEffect);
                return;
            }
        }
    }
}
