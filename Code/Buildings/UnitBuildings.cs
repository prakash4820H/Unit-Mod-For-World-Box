namespace Unit
{
    class UnitBuildings
    {

        public static void init()
        {

            BuildingAsset flower = AssetManager.buildings.get("flower");
            flower.canBePlacedOnlyOn = List.Of<string>(new string[]
            {
                ST.lemon_low,
            ST.lemon_high,
            ST.grass_low,
            ST.grass_high,
            ST.mushroom_high,
            ST.mushroom_low,
            ST.enchanted_low,
            ST.enchanted_high,
            "zelkova_low",
            "zelkova_high"
        });

            BuildingAsset mushroom = AssetManager.buildings.get("mushroom");
            mushroom.canBePlacedOnlyOn = List.Of<string>(ST.lemon_low, ST.lemon_high, ST.grass_low, ST.grass_high, ST.mushroom_high, ST.mushroom_low);
            mushroom.spread_ids = List.Of<string>(SB.mushroom, "biome_zelkova");

            BuildingAsset fruit_bush = AssetManager.buildings.get("fruit_bush");
            fruit_bush.has_ruin_state = false;
            fruit_bush.canBeLivingPlant = true;
            fruit_bush.buildingType = BuildingType.Fruits;
            fruit_bush.has_special_animation_state = true;
            fruit_bush.type = SB.type_fruits;
            fruit_bush.burnable = true;
            fruit_bush.vegetation = true;
            fruit_bush.canBeDamagedByTornado = true;
            fruit_bush.ignoredByCities = true;
            fruit_bush.vegetationRandomChance = 0.02f;
            fruit_bush.limit_per_zone = 1;
            fruit_bush.canBePlacedOnlyOn = List.Of<string>(ST.lemon_low, ST.lemon_high, ST.jungle_low, ST.jungle_high, ST.grass_low, ST.grass_high, ST.mushroom_low, ST.mushroom_high, ST.enchanted_low, ST.enchanted_high, ST.savanna_low, ST.savanna_high, "biome_zelkova");
            fruit_bush.material = "tree";
            fruit_bush.setShadow(0.19f, 0.03f, 0.09f);

            BuildingAsset hi = new BuildingAsset();
            hi.id = "hi";
            hi.grow_creep = true;
            hi.material = "jelly";
            hi.transformTilesToTopTiles = "zelkova_low";
            hi.race = "unit_orc";
            hi.kingdom = "unit_orc";
            hi.canBePlacedOnBlocks = true;
            hi.canBePlacedOnLiquid = true;
            hi.cityBuilding = false;
            hi.type = SB.type_fruits;
            hi.fundament = new BuildingFundament(1, 1, 1, 0);
            hi.grow_creep_direction_random_position = true;
            hi.has_ruin_state = false;
            hi.setShadow(0.19f, 0.03f, 0.09f);
            AssetManager.buildings.add(hi);
        }


    }
}
