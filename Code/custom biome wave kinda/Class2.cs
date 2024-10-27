using UnityEngine;

namespace Unit
{
    class UnitBehaviour : MonoBehaviour
    {
        public static void init()
        {
            WorldBehaviourAsset ArchMagePower = new WorldBehaviourAsset();
            ArchMagePower.id = "lol";
            ArchMagePower.enabled_on_minimap = false;
            ArchMagePower.interval = 0.3f;
            ArchMagePower.interval_random = 0.3f;
            ArchMagePower.action = new WorldBehaviourAction(WorldBehaviourGrassWaves.startWaves);
            ArchMagePower.manager = new WorldBehaviour(ArchMagePower);
            AssetManager.world_behaviours.add(ArchMagePower);
        }
    }
}
