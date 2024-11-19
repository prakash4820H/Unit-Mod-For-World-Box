using ai.behaviours;
using HarmonyLib;
using NeoModLoader.api;
using NeoModLoader.services;
using System;
using System.Reflection;
using UnityEngine;

namespace Unit
{
    public class ModClass : MonoBehaviour, IMod
    {
        private ModDeclare _declare;
        private GameObject _gameObject;

        // Property to get mod declaration and game object
        public ModDeclare GetDeclaration() => _declare;
        public GameObject GetGameObject() => _gameObject;
        public string GetUrl() => "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

        // Main loading method for the mod
        public void OnLoad(ModDeclare pModDecl, GameObject pGameObject)
        {
            _declare = pModDecl;
            _gameObject = pGameObject;

            LogService.LogInfo($"[{pModDecl.Name}]: Mod loading initiated.");
        }

        void Awake()
        {
            ApplyHarmonyPatches();
            InitializeGameComponents();
            InitializeModdedGameObject();
        }

        void Update()
        {
            // Reserved for periodic updates if needed in the future
        }

        // Function to apply Harmony patches
        private void ApplyHarmonyPatches()
        {
            try
            {
                var harmony = new Harmony("com.prakash.unitmod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                LogService.LogInfo("[ModClass] Harmony patches applied successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModClass] Harmony patching failed: {ex.Message}");
            }
        }

        // Initialize game components and traits
        private void InitializeGameComponents()
        {
            try
            {
                InitializeTraits();
                InitializeBiomesAndTiles();
                InitializeBuildings();
                InitializeOtherComponents();

                LogService.LogInfo("[ModClass] Game components initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModClass] Exception in InitializeGameComponents: {ex.Message}");
                Debug.LogError($"Stack Trace: {ex.StackTrace}");
            }
        }

        // Function to initialize mod-specific traits
        private void InitializeTraits()
        {
            try
            {
                UnitBehaviour1.init();
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

                LogService.LogInfo("[ModClass] Traits initialized.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModClass] Error in InitializeTraits: {ex.Message}");
            }
        }

        // Function to initialize biomes and tile behaviors
        private void InitializeBiomesAndTiles()
        {
            try
            {
                UnitBiomes.Init();
                UnitBiomes.post_init_tiles();
                UnitBiomes.post_init1();
                WorldBehaviourAcidTiles.InitializeAcidSpreading();
                WorldBehaviourBloodTiles.InitializeBloodSpreading();

                LogService.LogInfo("[ModClass] Biomes and tile behaviors initialized.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModClass] Error in InitializeBiomesAndTiles: {ex.Message}");
            }
        }

        // Function to initialize buildings
        private void InitializeBuildings()
        {
            try
            {
                UnitBuildings.init();
                LogService.LogInfo("[ModClass] Buildings initialized.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModClass] Error in InitializeBuildings: {ex.Message}");
            }
        }

        // Function to initialize remaining components
        private void InitializeOtherComponents()
        {
            try
            {
                AllUnitsWindow.init();
                CreditsWindow.init();
                UnitNames.Init();
                UnitTab.Init();
                EstablishKingdomTraitInit.Init();
                UnitButtons.init();
                UnitItems.Init();
                EffectsLibrary1.init();
                UnitStatusLibrary.Init();
                UnitUnitys.init();
                UnitTraitGroup.init();
                ExampleActorOverrideSprite.Init();
                ModInitializerColorCycle.Init();
                TraitReplicateEnemyPositiveTraits.Init();
                DisasterWaterMage1.InitWaterMageDisaster();
                LogService.LogInfo("[ModClass] Other components initialized.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModClass] Error in InitializeOtherComponents: {ex.Message}");
            }
        }


        // Ensure mod object is persistent and initialize additional MonoBehaviour
        private void InitializeModdedGameObject()
        {
            try
            {
                GameObject modObject = new GameObject("CP");
                modObject.AddComponent<CP>();
                DontDestroyOnLoad(modObject);
                LogService.LogInfo("[ModClass] Modded GameObject initialized and set to persist.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModClass] Error in InitializeModdedGameObject: {ex.Message}");
            }
        }
    }
}
