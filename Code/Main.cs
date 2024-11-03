
using HarmonyLib;
using NeoModLoader.api;
using NeoModLoader.services;
using System;
using System.Reflection;
using UnityEngine;


namespace Unit;

public class ModClass : MonoBehaviour, IMod
{

    private ModDeclare _declare;
    private GameObject _gameObject;

    public ModDeclare GetDeclaration()
    {
        return _declare;
    }
    public GameObject GetGameObject()
    {
        return _gameObject;
    }
    public string GetUrl()
    {
        return "URL of your mod's website or item page on workshop";
    }

    public void OnLoad(ModDeclare pModDecl, GameObject pGameObject)
    {

        _declare = pModDecl;
        _gameObject = pGameObject;
        // Initialize your mod.
        // Methods are called in the order: OnLoad -> Awake -> OnEnable -> Start -> Update

        LogService.LogInfo($"[{pModDecl.Name}]: Hello World!");
    }

    void Awake()
    {
        var harmony = new Harmony("com.prakash.unitmod");
        var executingAssembly = Assembly.GetExecutingAssembly();
        harmony.PatchAll(executingAssembly);
        InitializeComponents();

        GameObject gameObject = new GameObject("CP");
        gameObject.AddComponent<CP>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {


    }

    void InitializeComponents()
    {
        try
        {
            // Initialize Traits
            {
                // TimeTravelerTrait.Init();
                DisasterWaterMage.InitWaterMageDisaster();
                WeaponCollectorTraitInit.Init();
                TeleportationTraitInit.Init();
                InfinityTraitInit.Init();
                ExternalParasiteTraitInit.Init();
                ExternalParasiteStatusEffect.Init();
                NearbyEnemyTraitInit.Init();
                MahoragaTraitInit.Init();
                DefensiveShieldTraitInit.Init();
                FrozenBarrierTraitInit.Init();
                BarrierTraitInit.Init();
                ShadowFriendTraitInit.Init();
                SpawnFriendsTraitInit.Init();
                FreezeAndKillTraitInit.Init();
                EnhancedActorTraitInit.Init();
                ThiefActorTraitInit.Init();
                ImmortalTraitPatches.Init();
                MainUnitDeathTraitInit.Init();
                LightManipulationTraitInit.Init();
                LightningShieldTraitInit.Init();
                TornadoShieldTraitInit.Init();
                MedusaTraitInit.Init();
                AriseShadowTraitInit.Init();
                WhiteScreenTraitInit.Init();
                CircularSpriteTraitInit.Init();
                GojoSatoruTraits.Init();
                UnitTraits.init();
                SuppressorAura.Init();
                LeaderTraitInit.Init();

            }
            //Biomes and Tiles
            {
               UnitBiomes.Init();
               UnitBiomes.post_init_tiles();
                //     Patch_RemoveSmallBiomePatches.InitializeBiomes();
            }
            //Buildings
            {
                UnitBuildings.init();
            }
            UnitNames.Init();
            UnitTab.Init();
            WorldBehaviourAcidTiles.InitializeAcidSpreading();
            WorldBehaviourBloodTiles.InitializeBloodSpreading();
            UnitButtons.init();
            UnitItems.Init();
            EffectsLibrary1.init();
            UnitStatusLibrary.Init();
            UnitUnitys.init();
            UnitTraitGroup.init();
            ExampleActorOverrideSprite.Init();
            ModInitializerColorCycle.Init();
            TraitReplicateEnemyPositiveTraits.Init();
            UnitBiomes.post_init1();
            DisasterWaterMage1.InitWaterMageDisaster();

        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception in InitializeComponents: {ex.Message}");
            Debug.LogError($"Stack Trace: {ex.StackTrace}");
        }
    }
}
