using UnityEngine;
using UnityEngine.Tilemaps;

namespace Unit
{
    class UnitBiomes
    {
        // Cached tiles for better performance
        private static TopTileType _cachedTileLow;
        private static TopTileType _cachedTileHigh;

        // Initialize the biome and its elements
        public static void Init()
        {

            BuildingAsset zelkovaTree = new BuildingAsset();
            zelkovaTree.id = "zelkovaTree";
            zelkovaTree.fundament = new BuildingFundament(3, 3, 4, 0);
            zelkovaTree.buildingType = BuildingType.Tree;
            zelkovaTree.type = SB.type_tree;
            zelkovaTree.destroyOnLiquid = true;
            zelkovaTree.randomFlip = true;
            zelkovaTree.ignoredByCities = true;
            zelkovaTree.ignoreDemolish = true;
            zelkovaTree.burnable = true;
            zelkovaTree.affectedByAcid = true;
            zelkovaTree.affectedByLava = true;
            zelkovaTree.canBeHarvested = true;
            zelkovaTree.produce_biome_food = true;
            zelkovaTree.vegetation = true;
            zelkovaTree.canBeDamagedByTornado = true;
            zelkovaTree.has_special_animation_state = true;
            zelkovaTree.race = SK.nature;
            zelkovaTree.canBePlacedOnBlocks = false;
            zelkovaTree.kingdom = SK.nature;
            zelkovaTree.cityBuilding = false;
            zelkovaTree.has_special_animation_state = false;
            zelkovaTree.checkForCloseBuilding = false;
            zelkovaTree.canBePlacedOnlyOn = List.Of<string>(new string[]
             {
                "zelkova_low",
                "zelkova_high"
             });
            zelkovaTree.material = "tree";
            zelkovaTree.affected_by_drought = true;
            zelkovaTree.fmod_spawn = "event:/SFX/NATURE/BaseFloraSpawn";
            zelkovaTree.remove_ruins = false;
            zelkovaTree.spread = true;
            zelkovaTree.spread_ids = List.Of<string>(new string[]
             {
            SB.tree
             });
            zelkovaTree.canBeLivingPlant = true;
            zelkovaTree.limit_per_zone = 8;
            zelkovaTree.setShadow(0.6f, 0.03f, 0.12f);
            AssetManager.buildings.add(zelkovaTree);
            AssetManager.buildings.loadSprites(zelkovaTree);

            BuildingAsset zelkovaPlant = new BuildingAsset();
            zelkovaPlant.id = "zelkovaPlant";
            zelkovaPlant.fundament = new BuildingFundament(4, 3, 4, 0);
            zelkovaPlant.buildingType = BuildingType.Plant;
            zelkovaPlant.type = SB.type_flower;
            zelkovaPlant.destroyOnLiquid = true;
            zelkovaPlant.randomFlip = true;
            zelkovaPlant.ignoredByCities = true;
            zelkovaPlant.ignoreDemolish = true;
            zelkovaPlant.burnable = true;
            zelkovaPlant.affectedByAcid = true;
            zelkovaPlant.affectedByLava = true;
            zelkovaPlant.canBeHarvested = true;
            zelkovaPlant.vegetation = true;
            zelkovaPlant.cityBuilding = false;
            zelkovaPlant.canBeDamagedByTornado = true;
            zelkovaPlant.race = SK.nature;
            zelkovaPlant.kingdom = SK.nature;
            zelkovaPlant.canBePlacedOnBlocks = false;
            zelkovaPlant.has_special_animation_state = true;
            zelkovaPlant.checkForCloseBuilding = false;
            zelkovaPlant.canBePlacedOnlyOn = List.Of<string>(new string[]
             {
                "zelkova_low",
                "zelkova_high"
             });
            zelkovaPlant.material = "tree";
            zelkovaPlant.affected_by_drought = true;
            zelkovaPlant.fmod_spawn = "event:/SFX/NATURE/BaseFloraSpawn";
            zelkovaPlant.remove_ruins = false;
            zelkovaPlant.spread = true;
            zelkovaPlant.spread_ids = List.Of<string>(new string[]
             {
            "zelkovaPlant"
             });
            zelkovaPlant.canBeLivingPlant = true;
            zelkovaPlant.limit_per_zone = 10;
            zelkovaPlant.setShadow(0.6f, 0.03f, 0.12f);
            AssetManager.buildings.add(zelkovaPlant);
            AssetManager.buildings.loadSprites(zelkovaPlant);

            TopTileType zelkova_low = new TopTileType();
            zelkova_low.id = "zelkova_low";
            zelkova_low.color = Toolbox.makeColor("#4B6C44", -1f);
            zelkova_low.setBiome("biome_zelkova");
            zelkova_low.setDrawLayer(TileZIndexes.enchanted_low, null);
            zelkova_low.force_unit_skin_set = "enchanted";
            zelkova_low.fireChance = 0.01f;
            zelkova_low.can_be_biome = true;
            zelkova_low.canBuildOn = true;
            zelkova_low.can_be_farm = true;
            zelkova_low.can_be_frozen = true;
            zelkova_low.remove_on_heat = true;
            zelkova_low.ground = true;
            zelkova_low.can_be_unfrozen = true;
            zelkova_low.is_biome = true;
            zelkova_low.food_resource = SR.herbs;
            zelkova_low.food_resource = SR.wheat;
            zelkova_low.canBeRemovedWithSpade = true;
            zelkova_low.canErrodeToSand = true;
            zelkova_low.farm_field = true;
            zelkova_low.food_resource = "herbs";
            zelkova_low.food_resource = SR.wheat;
            zelkova_low.food_resource = SR.pie;
            MapBox.instance.tilemap.layers[zelkova_low.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            zelkova_low.food_resource = SR.coconut;
            zelkova_low.walkMod = 1f;
            AssetManager.topTiles.add(zelkova_low);
            AssetManager.topTiles.loadSpritesForTile(zelkova_low);

            TopTileType zelkova_high = new TopTileType();
            zelkova_high.id = "zelkova_high";
            zelkova_high.color = Toolbox.makeColor("#466540", -1f);
            zelkova_high.setBiome("biome_zelkova");
            zelkova_high.setDrawLayer(TileZIndexes.enchanted_low, null);
            zelkova_high.force_unit_skin_set = "enchanted";
            zelkova_high.fireChance = 0.01f;
            zelkova_high.canBuildOn = true;
            zelkova_high.food_resource = SR.herbs;
            zelkova_high.can_be_farm = true;
            zelkova_high._current_tiles = null;
            zelkova_high.can_be_biome = true;
            zelkova_high.can_be_frozen = true;
            zelkova_high.canBeRemovedWithSpade = true;
            zelkova_high.can_be_unfrozen = true;
            zelkova_high.food_resource = SR.herbs;
            zelkova_high.food_resource = SR.wheat;
            zelkova_high.ground = true;
            zelkova_high.food_resource = SR.pie;
            zelkova_high.food_resource = SR.coconut;
            zelkova_high.farm_field = true;
            zelkova_high.canErrodeToSand = true;
            zelkova_high.walkMod = 1f;
            MapBox.instance.tilemap.layers[zelkova_high.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            zelkova_high.is_biome = true;
            zelkova_high.remove_on_heat = true;
            AssetManager.topTiles.add(zelkova_high);
            AssetManager.topTiles.loadSpritesForTile(zelkova_high);

            BiomeAsset biome_zelkova = new BiomeAsset();
            biome_zelkova.id = "biome_zelkova";
            biome_zelkova.spread_biome = true;
            biome_zelkova.tile_low = "zelkova_low";
            biome_zelkova.tile_high = "zelkova_high";
            biome_zelkova.spawn_units_auto = true;
            biome_zelkova.grow_vegetation_auto = true;
            biome_zelkova.addTree("zelkovaTree");
            biome_zelkova.addPlant("mushroom", 2);
            biome_zelkova.addPlant("green_herb", 4);
            biome_zelkova.addPlant("flower", 4);
            biome_zelkova.addPlant("fruit_bush", 1);
            biome_zelkova.grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral);
            biome_zelkova.grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees);
            biome_zelkova.grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants);
            biome_zelkova.addUnit(SA.buffalo, 2);
            biome_zelkova.addUnit(SA.fly, 4);
            biome_zelkova.addUnit(SA.beetle, 4);
            biome_zelkova.addMineral(SB.mineral_adamantine, 4);
            biome_zelkova.addMineral(SB.mineral_mythril, 3);
            BiomeLibrary.pool_biomes.Add(biome_zelkova);
            AssetManager.biome_library.addBiomeToPool(biome_zelkova);
            AssetManager.biome_library.add(biome_zelkova);

            TileType deep_blood = AssetManager.tiles.clone("deep_blood", "shallow_waters");
            deep_blood.decreaseToID = ST.pit_deep_ocean;
            deep_blood.increaseToID = ST.pit_close_ocean;
            deep_blood.ocean = true;
            deep_blood.layerType = TileLayerType.Null;
            deep_blood.canBeRemovedWithBucket = true;
            deep_blood.canBuildOn = true;
            deep_blood.heightMin = 70;
            deep_blood.color = Toolbox.makeColor("#BD170B", -1f);
            deep_blood.edge_color = Toolbox.makeColor("#FF5733", -1f);
            deep_blood.liquid = true;
            MapBox.instance.tilemap.layers[deep_blood.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            deep_blood.used_in_generator = false;
            AssetManager.tiles.add(deep_blood);
            AssetManager.tiles.loadSpritesForTile(deep_blood);


            TileType close_blood = AssetManager.tiles.clone("close_blood", "close_ocean");
            close_blood.decreaseToID = ST.pit_close_ocean;
            close_blood.increaseToID = ST.pit_shallow_waters;
            close_blood.used_in_generator = false;
            close_blood.layerType = TileLayerType.Null;
            close_blood.liquid = true;
            close_blood.ocean = true;
            close_blood.heightMin = 70;
            close_blood.canBuildOn = true;
            close_blood.edge_color = Toolbox.makeColor("#FF5733", -1f);
            close_blood.color = Toolbox.makeColor("#E83427", -1f);
            close_blood.canBeRemovedWithBucket = true;
            close_blood.ocean = false;
            MapBox.instance.tilemap.layers[close_blood.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            close_blood.used_in_generator = false;
            AssetManager.tiles.add(close_blood);
            AssetManager.tiles.loadSpritesForTile(close_blood);

            TileType shallow_blood = AssetManager.tiles.clone("shallow_blood", "shallow_waters");
            shallow_blood.decreaseToID = ST.pit_shallow_waters;
            shallow_blood.increaseToID = ST.sand;
            shallow_blood.canBeRemovedWithBucket = true;
            shallow_blood.heightMin = 70;
            shallow_blood.layerType = TileLayerType.Null;
            shallow_blood.ocean = true;
            shallow_blood.canBuildOn = true;
            shallow_blood.edge_color = Toolbox.makeColor("#FF5733", -1f);
            shallow_blood.color = Toolbox.makeColor("#E35259", -1f);
            shallow_blood.liquid = true;
            MapBox.instance.tilemap.layers[shallow_blood.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            shallow_blood.used_in_generator = false;
            AssetManager.tiles.add(shallow_blood);
            AssetManager.tiles.loadSpritesForTile(shallow_blood);


            TileType deep_acid = AssetManager.tiles.clone("deep_acid", "shallow_waters");
            deep_acid.decreaseToID = ST.pit_deep_ocean;
            deep_acid.increaseToID = ST.pit_close_ocean;
            deep_acid.canBeRemovedWithBucket = true;
            deep_acid.ocean = false;
            deep_acid.liquid = true;
            deep_acid.damage = 4;
            deep_acid.damageUnits = true;
            deep_acid.layerType = TileLayerType.Null;
            MapBox.instance.tilemap.layers[deep_acid.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            deep_acid.used_in_generator = false;
            AssetManager.tiles.add(deep_acid);
            AssetManager.tiles.loadSpritesForTile(deep_acid);


            TileType close_acid = AssetManager.tiles.clone("close_acid", "close_ocean");
            close_acid.decreaseToID = ST.pit_close_ocean;
            close_acid.increaseToID = ST.pit_shallow_waters;
            close_acid.used_in_generator = false;
            close_acid.layerType = TileLayerType.Null;
            close_acid.ocean = false;
            close_acid.damageUnits = true;
            close_acid.liquid = true;
            close_acid.damage = 4;
            close_acid.canBeRemovedWithBucket = true;
            close_acid.used_in_generator = false;
            MapBox.instance.tilemap.layers[close_acid.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            AssetManager.tiles.add(close_acid);
            AssetManager.tiles.loadSpritesForTile(close_acid);

            TileType shallow_acid = AssetManager.tiles.clone("shallow_acid", "shallow_waters");
            shallow_acid.decreaseToID = ST.pit_shallow_waters;
            shallow_acid.increaseToID = ST.sand;
            shallow_acid.layerType = TileLayerType.Null;
            shallow_acid.damageUnits = true;
            shallow_acid.liquid = true;
            shallow_acid.canBeRemovedWithBucket = true;
            shallow_acid.ocean = false;
            shallow_acid.damage = 4;
            shallow_acid.used_in_generator = false;
            MapBox.instance.tilemap.layers[shallow_acid.render_z].tilemap.GetComponent<TilemapRenderer>().mode = TilemapRenderer.Mode.Individual;
            AssetManager.tiles.add(shallow_acid);
            AssetManager.tiles.loadSpritesForTile(shallow_acid);


            BiomeAsset biometoallotocean = new BiomeAsset();
            biometoallotocean.id = "biometoallotocean";
            biometoallotocean.special_biome = true;
            biometoallotocean.tile_high = ST.close_ocean;
            biometoallotocean.tile_low = ST.shallow_waters;
            biometoallotocean.spread_biome = true;
            biometoallotocean.grow_strength = 10;
            biometoallotocean.spawn_units_auto = true;
            biometoallotocean.addUnit(SA.piranha, 2);
            AssetManager.biome_library.add(biometoallotocean);


            // Ensure TileTypes are initialized before assigning biomes
            TileType close_ocean = AssetManager.tiles.get(ST.close_ocean);
            close_ocean.biome_id = "biometoallotocean";
            close_ocean.ground = false;
            close_ocean.liquid = true;

            // Ensure TileTypes are initialized before assigning biomes
            TileType deep_ocean = AssetManager.tiles.get(ST.close_ocean);
            deep_ocean.biome_id = "biometoallotocean";
            deep_ocean.ground = false;
            deep_ocean.liquid = true;

            TileType shallow_waters = AssetManager.tiles.get(ST.shallow_waters);
            shallow_waters.biome_id = "biometoallotocean";
            shallow_waters.ground = false;
            shallow_waters.liquid = true;

            if (close_ocean != null && shallow_waters != null)
            {
                close_ocean.setBiome("biometoallotocean");
                shallow_waters.setBiome("biometoallotocean");
            }
            else
            {
                Debug.LogError("Error: TileType deep_ocean or close_ocean is null.");
            }
        }

        public static void post_init_tiles()
        {
            AssetManager.tiles.post_init();

            foreach (TileType tile in AssetManager.tiles.list)
            {
                if (!string.IsNullOrEmpty(tile.biome_id))
                {
                    tile.biome_asset = AssetManager.biome_library.get(tile.biome_id);
                }
            }
        }

        public static void post_init1()
        {
            AssetManager.biome_library.post_init();
            foreach (BiomeAsset item in AssetManager.biome_library.list)
            {
                AssetManager.biome_library.addBiomeToPool(item);
            }

            _cachedTileLow = AssetManager.topTiles.get("zelkova_low");
            _cachedTileHigh = AssetManager.topTiles.get("zelkova_high");

            foreach (TopTileType item2 in AssetManager.topTiles.list)
            {
                if (!string.IsNullOrEmpty(item2.biome_id))
                {
                    item2.biome_asset = AssetManager.biome_library.get(item2.biome_id);
                }
            }
        }

        public static TopTileType getTile(WorldTile pTile)
        {
            return pTile.main_type.rank_type switch
            {
                TileRank.High => getTileHigh(),
                TileRank.Low => getTileLow(),
                _ => null,
            };
        }

        public static TopTileType getTileLow()
        {
            if (_cachedTileLow == null)
            {
                _cachedTileLow = AssetManager.topTiles.get("zelkova_low");
            }
            return _cachedTileLow;
        }

        public static TopTileType getTileHigh()
        {
            if (_cachedTileHigh == null)
            {
                _cachedTileHigh = AssetManager.topTiles.get("zelkova_high");
            }
            return _cachedTileHigh;
        }
    }
}
