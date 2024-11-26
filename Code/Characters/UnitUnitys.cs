using ReflectionUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Unit
{
    class UnitUnitys
    {
        public static void init()
        {
            create_DragonSlayer();
            create_MaleVampire();
            create_FeMaleVampire();
            create_SoulStealer();
            create_MageHunter();
            create_ArchMage();
            create_evilfish();
            create_GrandMage();
            create_MageLord();
            create_Lich();
            create_Robot();
            create_BlueFish();
            create_Salmon();
            create_DemonLord();
            create_GojoSatoru();
            create_Sus();
            create_spider();
            create_newone();
            /*
            ActorAsset Snake = AssetManager.actor_library.get("snake");
            Snake.egg = true;
            Snake.eggStatsID = "egg_turtle";
            Snake.growIntoID = "dragon";
            Snake.years_to_grow_to_adult = 25;
            */

            /*
            ActorAsset DragonSlayer = AssetManager.actor_library.get("DragonSlayer");
            DragonSlayer.canBeKilledByLifeEraser = false;
            */

            ActorAsset Snake = AssetManager.actor_library.get("wolf");
            Snake.deathAnimationAngle = false;


            ActorAsset Snake1 = AssetManager.actor_library.get("greg");
            Snake1.use_items = true;

            // mobs which is not unit or not having heads can change texture
            ActorAsset h = AssetManager.actor_library.get("snake");
            h.texture_path = "ArchMage";

            ActorAsset h1 = AssetManager.actor_library.get("piranha");
            h1.maxRandomAmount = 2;
        }

        public static void create_DragonSlayer()
        {
            var DragonSlayer = AssetManager.actor_library.clone("DragonSlayer", "_mob");
            DragonSlayer.id = "DragonSlayer";
            DragonSlayer.nameLocale = "DragonSlayer";
            DragonSlayer.nameTemplate = "dragonslayer_name";
            DragonSlayer.race = "DragonSlayerRace";
            DragonSlayer.kingdom = "DragonSlayerRace";


            DragonSlayer.base_stats[S.max_age] = 250;
            DragonSlayer.base_stats[S.knockback_reduction] = 10f;
            DragonSlayer.base_stats[S.critical_chance] = 50;
            DragonSlayer.base_stats[S.attack_speed] = 70f;
            DragonSlayer.base_stats[S.knockback] = 0f;
            DragonSlayer.base_stats[S.accuracy] = 70f;
            DragonSlayer.base_stats[S.health] = 1500;
            DragonSlayer.base_stats[S.speed] = 60;
            DragonSlayer.base_stats[S.damage] = 74;
            DragonSlayer.base_stats[S.targets] = 5;
            DragonSlayer.base_stats[S.dodge] = 0.5f;
            DragonSlayer.base_stats[S.armor] = 50;
            DragonSlayer.defaultAttack = "Catapult";
            DragonSlayer.base_stats[S.scale] = 0.12f;
            //     DragonSlayer.specialDeadAnimation = true;
            DragonSlayer.immune_to_tumor = true;
            DragonSlayer.immune_to_injuries = true;
            //        DragonSlayer.has_override_sprite = true;
            //      DragonSlayer.get_override_sprite = ExampleActorOverrideSprite.get_human_override_sprite;
            DragonSlayer.color = Toolbox.makeColor("#000000", -1f);
            DragonSlayer.immune_to_slowness = true;
            DragonSlayer.canBeKilledByDivineLight = false;
            DragonSlayer.canBeKilledByLifeEraser = true;
            DragonSlayer.ignoredByInfinityCoin = false;
            DragonSlayer.disableJumpAnimation = true;
            DragonSlayer.canBeMovedByPowers = true;
            // DragonSlayer.canTurnIntoZombie = false;
            DragonSlayer.canAttackBuildings = true;
            DragonSlayer.hideFavoriteIcon = false;
            //   DragonSlayer.can_edit_traits = true;
            //  DragonSlayer.very_high_flyer = false;
            //    DragonSlayer.damagedByOcean = false;
            //   DragonSlayer.swampCreature = false;
            DragonSlayer.damagedByRain = false;
            DragonSlayer.oceanCreature = false;
            DragonSlayer.landCreature = true;
            DragonSlayer.dieOnGround = false;
            DragonSlayer.take_items = false;
            DragonSlayer.use_items = true;
            DragonSlayer.diet_meat = false;
            DragonSlayer.dieInLava = false;
            //  DragonSlayer.needFood = false;
            DragonSlayer.has_soul = true;
            DragonSlayer.flying = false;
            DragonSlayer.canBeCitizen = true;
            DragonSlayer.sound_hit = "event:/SFX/HIT/HitMetal";
            //     DragonSlayer.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            /*
            DragonSlayer.defaultWeapons = List.Of<string>(new string[]
           {  "DragonSlayerSword" });
            DragonSlayer.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            */
            //      DragonSlayer.animation_idle = "idle_0";
            //     DragonSlayer.fmod_attack = "swim_0,swim_1";
            //     DragonSlayer.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            DragonSlayer.texture_path = "DragonSlayer";
            DragonSlayer.icon = "iconDragonSlayer";
            //    DragonSlayer.job = "attacker";
            // Initialize all mobs with custom sprite orders
            AssetManager.actor_library.add(DragonSlayer);
            AssetManager.actor_library.CallMethod("loadShadow", DragonSlayer);
            AssetManager.actor_library.CallMethod("addTrait", "dragonslayer");
            AssetManager.actor_library.CallMethod("addTrait", "immune");
            AssetManager.actor_library.CallMethod("addTrait", "Revive");
            AssetManager.actor_library.CallMethod("addTrait", "fire_proof");
            AssetManager.actor_library.CallMethod("addTrait", "DragonSlayerAura");
            LocalizationUtility.AddLocalization(DragonSlayer.nameLocale, DragonSlayer.nameLocale);

            KingdomAsset DragonSlayerRaceKingdom = new KingdomAsset();
            DragonSlayerRaceKingdom.id = "DragonSlayerRace";
            DragonSlayerRaceKingdom.mobs = true;
            DragonSlayerRaceKingdom.addTag("DragonSlayerRace");
            //    /*DragonSlayerRaceKingdom.addTag("good");
            //	DragonSlayerRaceKingdom.addTag("nature_creature");
            DragonSlayerRaceKingdom.addFriendlyTag(SK.undead);
            DragonSlayerRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(DragonSlayerRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", DragonSlayerRaceKingdom);

        }


        public static void create_SoulStealer()
        {
            var SoulStealer = AssetManager.actor_library.clone("SoulStealer", "_mob");
            SoulStealer.nameLocale = "SoulStealer";
            SoulStealer.nameTemplate = "dragonslayer_name";
            SoulStealer.race = "SoulStealerRace";
            SoulStealer.kingdom = "SoulStealerRace";
            SoulStealer.base_stats[S.max_age] = 250;
            SoulStealer.base_stats[S.knockback_reduction] = 10f;
            SoulStealer.base_stats[S.critical_chance] = 50;
            SoulStealer.base_stats[S.attack_speed] = 70f;
            SoulStealer.base_stats[S.knockback] = 0f;
            SoulStealer.base_stats[S.accuracy] = 70f;
            SoulStealer.base_stats[S.health] = 1500;
            SoulStealer.base_stats[S.speed] = 60;
            SoulStealer.base_stats[S.damage] = 74;
            SoulStealer.base_stats[S.targets] = 5;
            SoulStealer.base_stats[S.dodge] = 0.5f;
            SoulStealer.base_stats[S.armor] = 50;
            SoulStealer.base_stats[S.scale] = 0.12f;
            SoulStealer.immune_to_tumor = true;
            SoulStealer.immune_to_injuries = true;
            SoulStealer.color = Toolbox.makeColor("#000000", -1f);
            SoulStealer.immune_to_slowness = true;
            SoulStealer.canBeKilledByDivineLight = false;
            SoulStealer.canBeKilledByLifeEraser = true;
            SoulStealer.ignoredByInfinityCoin = false;
            SoulStealer.disableJumpAnimation = true;
            SoulStealer.canBeMovedByPowers = true;
            SoulStealer.canTurnIntoZombie = false;
            SoulStealer.canAttackBuildings = true;
            SoulStealer.hideFavoriteIcon = false;
            SoulStealer.can_edit_traits = true;
            SoulStealer.very_high_flyer = false;
            SoulStealer.damagedByOcean = false;
            SoulStealer.swampCreature = false;
            SoulStealer.damagedByRain = false;
            SoulStealer.oceanCreature = false;
            SoulStealer.landCreature = true;
            SoulStealer.dieOnGround = false;
            SoulStealer.take_items = false;
            SoulStealer.use_items = true;
            SoulStealer.diet_meat = false;
            SoulStealer.dieInLava = false;
            SoulStealer.needFood = false;
            SoulStealer.has_soul = true;
            SoulStealer.flying = false;
            SoulStealer.canBeCitizen = true;

            /*
            SoulStealer.defaultWeapons = List.Of<string>(new string[]
           {  "MageSlayerSword" });
            SoulStealer.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            */
            SoulStealer.sound_hit = "event:/SFX/HIT/HitMetal";
            SoulStealer.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
      //      SoulStealer.animation_walk = "walk_0,walk_1,walk_2,walk_3";
      //      SoulStealer.animation_swim = "walk_0,walk_1,walk_2,walk_3";
            SoulStealer.texture_path = "SoulStealer";
            SoulStealer.icon = "iconSoulStealer";
            SoulStealer.job = "attacker";
            AssetManager.actor_library.add(SoulStealer);
            AssetManager.actor_library.CallMethod("loadShadow", SoulStealer);
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "poison_immune");
            AssetManager.actor_library.CallMethod("addTrait", "acid_proof");
            AssetManager.actor_library.CallMethod("addTrait", "freeze_proof");
            AssetManager.actor_library.CallMethod("addTrait", "fire_proof");
            LocalizationUtility.AddLocalization(SoulStealer.nameLocale, SoulStealer.nameLocale);


            KingdomAsset SoulStealerRaceKingdom = new KingdomAsset();
            SoulStealerRaceKingdom.id = "SoulStealerRace";
            SoulStealerRaceKingdom.mobs = true;
            SoulStealerRaceKingdom.addTag("SoulStealerRace");
            /*SoulStealerRaceKingdom.addTag("good");
			SoulStealerRaceKingdom.addTag("nature_creature");
			SoulStealerRaceKingdom.addFriendlyTag("SoulStealerRace");
			SoulStealerRaceKingdom.addFriendlyTag("MaleVampireRace");
			SoulStealerRaceKingdom.addFriendlyTag("neutral");/*/
            SoulStealerRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(SoulStealerRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", SoulStealerRaceKingdom);

        }

        public static void create_newone()
        {
            var newone = AssetManager.actor_library.clone("newone", "_mob");
            newone.id = "newone";
            newone.nameLocale = "DragonSlayer";
            newone.nameTemplate = "dragonslayer_name";
            newone.race = "newoneRace";
            newone.disableJumpAnimation = true;
            newone.kingdom = "spiderRace";
            newone.base_stats[S.health] = 500;
            newone.base_stats[S.damage] = 50;
            newone.base_stats[S.attack_speed] = 99999;
            newone.base_stats[S.scale] = 0.03f;
            newone.color = Toolbox.makeColor("#000000", -1f);
            AssetManager.actor_library.addColorSet(S_SkinColor.polar);
            AssetManager.actor_library.addColorSet(S_SkinColor.polar_2);
            AssetManager.actor_library.addColorSet(S_SkinColor.corrupted);
            AssetManager.actor_library.addColorSet(S_SkinColor.generic_brown);
            AssetManager.actor_library.addColorSet(S_SkinColor.generic_grey);
            AssetManager.actor_library.addColorSet(S_SkinColor.generic_dark);
            AssetManager.actor_library.addColorSet(S_SkinColor.desert);
            AssetManager.actor_library.addColorSet(S_SkinColor.infernal);
            AssetManager.actor_library.addColorSet(S_SkinColor.lemon);
            newone.texture_path = "newone";
            newone.icon = "iconDinosaw";
            newone.color = Toolbox.makeColor("#393939", -1f);
            newone.disablePunchAttackAnimation = true;
            newone.deathAnimationAngle = false;
            AssetManager.actor_library.add(newone);
            AssetManager.actor_library.CallMethod("loadShadow", newone);
            LocalizationUtility.AddLocalization(newone.nameLocale, newone.nameLocale);
        }

        public static void create_spider()
        {
            var spider = AssetManager.actor_library.clone("spider", "_mob");
            spider.id = "spider";
            spider.nameLocale = "DragonSlayer";
            spider.nameTemplate = "dragonslayer_name";
            spider.race = "spiderRace";
            spider.kingdom = "spiderRace";
            spider.base_stats[S.max_age] = 150;
            spider.base_stats[S.knockback_reduction] = 10f;
            spider.base_stats[S.attack_speed] = 70f;
            spider.base_stats[S.accuracy] = 0f;
            spider.base_stats[S.health] = 1200;
            spider.base_stats[S.speed] = 35;
            spider.base_stats[S.damage] = 55;
            spider.base_stats[S.targets] = 5;
            spider.base_stats[S.dodge] = 0.5f;
            spider.base_stats[S.armor] = 45;
            // Hitbox-like visual adjustments
            spider.hovering_min = 8f;
            spider.hovering_max = 8f;
            spider.flying = true;
            spider.very_high_flyer = true;
            spider.base_stats[S.scale] = 0.12f;
            spider.immune_to_tumor = true;
            spider.immune_to_injuries = true;
            spider.color = Toolbox.makeColor("#000000", -1f);
            spider.immune_to_slowness = true;
            spider.canBeKilledByDivineLight = false;
            spider.canBeKilledByLifeEraser = true;
            spider.ignoredByInfinityCoin = false;
            spider.disableJumpAnimation = true;
            spider.canBeMovedByPowers = true;
            spider.canTurnIntoZombie = false;
            spider.canAttackBuildings = true;
            spider.hideFavoriteIcon = false;
            spider.can_edit_traits = true;
            spider.damagedByOcean = false;
            spider.swampCreature = false;
            spider.damagedByRain = false;
            spider.oceanCreature = false;
            spider.landCreature = true;
            spider.dieOnGround = false;
            spider.take_items = false;
            spider.use_items = true;
            spider.diet_meat = false;
            spider.dieInLava = false;
            spider.needFood = false;
            spider.has_soul = true;
            spider.canBeCitizen = true;
            spider.sound_hit = "event:/SFX/HIT/HitMetal";
            spider.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            spider.deathAnimationAngle = false;
            spider.action_death = (WorldAction)Delegate.Combine(spider.action_death, new WorldAction(MagnetShroom_1));
            spider.animation_idle = "walk_0";
            spider.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            spider.animation_swim = "swim_0,swim_1,swim_2,swim_3";
            spider.texture_path = "spider";
            spider.icon = "iconspider";
            spider.job = "attacker";
            AssetManager.actor_library.add(spider);
            AssetManager.actor_library.CallMethod("loadShadow", spider);
            AssetManager.actor_library.CallMethod("addTrait", "immune");
            AssetManager.actor_library.CallMethod("addTrait", "Revive");
            AssetManager.actor_library.CallMethod("addTrait", "fire_proof");
            LocalizationUtility.AddLocalization(spider.nameLocale, spider.nameLocale);

            KingdomAsset spiderRaceKingdom = new KingdomAsset();
            spiderRaceKingdom.id = "spiderRace";
            spiderRaceKingdom.mobs = true;
            spiderRaceKingdom.addTag("spiderRace");
            /*spiderRaceKingdom.addTag("good");
			spiderRaceKingdom.addTag("nature_creature");*/
            spiderRaceKingdom.addFriendlyTag("spiderRace");
            spiderRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(spiderRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", spiderRaceKingdom);

        }

        public static void create_MaleVampire()
        {
            var MaleVampire = AssetManager.actor_library.clone("MaleVampire", "_mob");
            MaleVampire.nameLocale = "MaleVampire";
            MaleVampire.nameTemplate = "dragonslayer_name";
            MaleVampire.race = "MaleVampireRace";
            MaleVampire.kingdom = "MaleVampireRace";
            MaleVampire.base_stats[S.max_age] = 1000;
            MaleVampire.base_stats[S.knockback_reduction] = 10f;
            MaleVampire.base_stats[S.critical_chance] = 50;
            MaleVampire.base_stats[S.attack_speed] = 70f;
            MaleVampire.base_stats[S.knockback] = 0f;
            MaleVampire.base_stats[S.accuracy] = 70f;
            MaleVampire.base_stats[S.health] = 1200;
            MaleVampire.base_stats[S.speed] = 69;
            MaleVampire.base_stats[S.damage] = 40;
            MaleVampire.base_stats[S.targets] = 5;
            MaleVampire.base_stats[S.dodge] = 100f;
            MaleVampire.base_stats[S.armor] = 30;
            MaleVampire.base_stats[S.scale] = 0.12f;
            MaleVampire.immune_to_tumor = true;
            MaleVampire.color = Toolbox.makeColor("#000000", -1f);
            MaleVampire.immune_to_injuries = true;
            MaleVampire.immune_to_slowness = true;
            MaleVampire.canBeKilledByDivineLight = false;
            MaleVampire.canBeKilledByLifeEraser = true;
            MaleVampire.ignoredByInfinityCoin = false;
            MaleVampire.disableJumpAnimation = true;
            MaleVampire.canBeMovedByPowers = true;
            MaleVampire.canTurnIntoZombie = false;
            MaleVampire.canAttackBuildings = true;
            MaleVampire.hideFavoriteIcon = false;
            MaleVampire.can_edit_traits = true;
            MaleVampire.very_high_flyer = false;
            MaleVampire.damagedByOcean = false;
            MaleVampire.swampCreature = false;
            MaleVampire.damagedByRain = false;
            MaleVampire.oceanCreature = false;
            MaleVampire.landCreature = true;
            MaleVampire.dieOnGround = false;
            MaleVampire.take_items = false;
            MaleVampire.use_items = true;
            MaleVampire.diet_meat = false;
            MaleVampire.dieInLava = false;
            MaleVampire.needFood = false;
            MaleVampire.has_soul = true;
            MaleVampire.flying = false;
            MaleVampire.canBeCitizen = true;
            MaleVampire.sound_hit = "event:/SFX/HIT/HitMetal";
            MaleVampire.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            MaleVampire.animation_walk = "walk_0,walk_1,walk_2,walk_3,walk_4";
            MaleVampire.animation_swim = "swim_0,swim_1,swim_2";
            MaleVampire.texture_path = "MaleVampire";
            MaleVampire.icon = "iconMaleVampire";
            MaleVampire.job = "attacker";
            MaleVampire.defaultWeapons = List.Of<string>(new string[]
           { "RedStoneBow" });
            MaleVampire.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            AssetManager.actor_library.add(MaleVampire);
            AssetManager.actor_library.CallMethod("loadShadow", MaleVampire);
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "poison_immune");
            AssetManager.actor_library.CallMethod("addTrait", "acid_proof");
            AssetManager.actor_library.CallMethod("addTrait", "freeze_proof");
            LocalizationUtility.AddLocalization(MaleVampire.nameLocale, MaleVampire.nameLocale);



            KingdomAsset MaleVampireRaceKingdom = new KingdomAsset();
            MaleVampireRaceKingdom.id = "MaleVampireRace";
            MaleVampireRaceKingdom.mobs = true;
            MaleVampireRaceKingdom.addTag("MaleVampireRace");
            /*MaleVampireRaceKingdom.addTag("good");
			MaleVampireRaceKingdom.addTag("nature_creature");
			MaleVampireRaceKingdom.addFriendlyTag("MaleVampireRace");
			MaleVampireRaceKingdom.addFriendlyTag("FeMaleVampireRace");
			MaleVampireRaceKingdom.addFriendlyTag("neutral");*/
            MaleVampireRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(MaleVampireRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", MaleVampireRaceKingdom);


        }

        public static void create_FeMaleVampire()
        {
            var FeMaleVampire = AssetManager.actor_library.clone("FeMaleVampire", "_mob");
            FeMaleVampire.nameLocale = "FeMaleVampire";
            FeMaleVampire.nameTemplate = "dragonslayer_name";
            FeMaleVampire.race = "FeMaleVampireRace";
            FeMaleVampire.kingdom = "FeMaleVampireRace";
            FeMaleVampire.base_stats[S.max_age] = 1000;
            FeMaleVampire.base_stats[S.knockback_reduction] = 10f;
            FeMaleVampire.base_stats[S.critical_chance] = 50;
            FeMaleVampire.base_stats[S.attack_speed] = 70f;
            FeMaleVampire.base_stats[S.knockback] = 0f;
            FeMaleVampire.base_stats[S.accuracy] = 70f;
            FeMaleVampire.base_stats[S.health] = 1100;
            FeMaleVampire.base_stats[S.speed] = 69;
            FeMaleVampire.base_stats[S.damage] = 40;
            FeMaleVampire.base_stats[S.targets] = 5;
            FeMaleVampire.base_stats[S.dodge] = 100f;
            FeMaleVampire.base_stats[S.armor] = 20;
            FeMaleVampire.base_stats[S.scale] = 0.12f;
            FeMaleVampire.color = Toolbox.makeColor("#000000", -1f);
            FeMaleVampire.immune_to_tumor = true;
            FeMaleVampire.immune_to_injuries = true;
            FeMaleVampire.immune_to_slowness = true;
            FeMaleVampire.defaultWeapons = List.Of<string>(new string[]
           { "WhiteSword" });
            FeMaleVampire.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            FeMaleVampire.canBeKilledByDivineLight = false;
            FeMaleVampire.canBeKilledByLifeEraser = true;
            FeMaleVampire.ignoredByInfinityCoin = false;
            FeMaleVampire.disableJumpAnimation = true;
            FeMaleVampire.canBeMovedByPowers = true;
            FeMaleVampire.canTurnIntoZombie = false;
            FeMaleVampire.canAttackBuildings = true;
            FeMaleVampire.hideFavoriteIcon = false;
            FeMaleVampire.can_edit_traits = true;
            FeMaleVampire.very_high_flyer = false;
            FeMaleVampire.damagedByOcean = false;
            FeMaleVampire.swampCreature = false;
            FeMaleVampire.damagedByRain = false;
            FeMaleVampire.oceanCreature = false;
            FeMaleVampire.landCreature = true;
            FeMaleVampire.dieOnGround = false;
            FeMaleVampire.take_items = false;
            FeMaleVampire.use_items = true;
            FeMaleVampire.diet_meat = false;
            FeMaleVampire.dieInLava = false;
            FeMaleVampire.needFood = false;
            FeMaleVampire.has_soul = true;
            FeMaleVampire.flying = false;
            FeMaleVampire.canBeCitizen = true;
            FeMaleVampire.sound_hit = "event:/SFX/HIT/HitMetal";
            FeMaleVampire.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            FeMaleVampire.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            FeMaleVampire.animation_swim = "swim_0,swim_1,swim_2";
            FeMaleVampire.texture_path = "FeMaleVampire";
            FeMaleVampire.icon = "iconFeMaleVampire";
            FeMaleVampire.job = "attacker";
            AssetManager.actor_library.add(FeMaleVampire);
            AssetManager.actor_library.CallMethod("loadShadow", FeMaleVampire);
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "immune");
            AssetManager.actor_library.CallMethod("addTrait", "poison_immune");
            AssetManager.actor_library.CallMethod("addTrait", "freeze_proof");
            AssetManager.actor_library.CallMethod("addTrait", "Special");
            LocalizationUtility.AddLocalization(FeMaleVampire.nameLocale, FeMaleVampire.nameLocale);


            KingdomAsset FeMaleVampireRaceKingdom = new KingdomAsset();
            FeMaleVampireRaceKingdom.id = "FeMaleVampireRace";
            FeMaleVampireRaceKingdom.mobs = true;
            FeMaleVampireRaceKingdom.addTag("FeMaleVampireRace");
            /*FeMaleVampireRaceKingdom.addTag("good");
			FeMaleVampireRaceKingdom.addTag("nature_creature");
			FeMaleVampireRaceKingdom.addFriendlyTag("FeMaleVampireRace");
			FeMaleVampireRaceKingdom.addFriendlyTag("MaleVampireRace");
			FeMaleVampireRaceKingdom.addFriendlyTag("neutral");/*/
            FeMaleVampireRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(FeMaleVampireRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", FeMaleVampireRaceKingdom);

        }


        public static void create_ArchMage()
        {
            var ArchMage = AssetManager.actor_library.clone("ArchMage", "_mob");
            ArchMage.nameLocale = "ArchMage";
            ArchMage.nameTemplate = "dragonslayer_name";
            ArchMage.race = "ArchMageRace";
            ArchMage.kingdom = "ArchMageRace";
            ArchMage.base_stats[S.max_age] = 300;
            ArchMage.base_stats[S.knockback_reduction] = 10f;
            ArchMage.base_stats[S.critical_chance] = 50;
            ArchMage.base_stats[S.attack_speed] = 70f;
            ArchMage.base_stats[S.knockback] = 0f;
            ArchMage.base_stats[S.accuracy] = 70f;
            ArchMage.base_stats[S.health] = 2000;
            ArchMage.base_stats[S.speed] = 45;
            ArchMage.base_stats[S.damage] = 60;
            ArchMage.base_stats[S.targets] = 5;
            ArchMage.base_stats[S.dodge] = 100f;
            ArchMage.base_stats[S.armor] = 30;
            ArchMage.base_stats[S.scale] = 0.12f;
            ArchMage.immune_to_tumor = true;
            ArchMage.immune_to_injuries = true;
            ArchMage.immune_to_slowness = true;
            ArchMage.canBeKilledByDivineLight = false;
            ArchMage.canBeKilledByLifeEraser = true;
            ArchMage.ignoredByInfinityCoin = false;
            ArchMage.disableJumpAnimation = true;
            ArchMage.canBeMovedByPowers = true;
            ArchMage.canTurnIntoZombie = false;
            ArchMage.canAttackBuildings = true;
            ArchMage.hideFavoriteIcon = false;
            ArchMage.can_edit_traits = true;
            ArchMage.very_high_flyer = false;
            ArchMage.damagedByOcean = false;
            ArchMage.swampCreature = false;
            ArchMage.damagedByRain = false;
            ArchMage.oceanCreature = false;
            ArchMage.landCreature = true;
            ArchMage.dieOnGround = false;
            ArchMage.take_items = false;
            ArchMage.use_items = true;
            ArchMage.diet_meat = false;
            ArchMage.dieInLava = false;
            ArchMage.needFood = false;
            ArchMage.has_soul = true;
            ArchMage.color = Toolbox.makeColor("#000000", -1f);
            ArchMage.flying = false;
            ArchMage.canBeCitizen = true;
            ArchMage.action_death = UnitActionLibrary.Meteore;
            ArchMage.defaultWeapons = List.Of<string>(new string[]
           { "EStaff"});
            ArchMage.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            ArchMage.sound_hit = "event:/SFX/HIT/HitMetal";
            ArchMage.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            ArchMage.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            ArchMage.animation_swim = "walk_0,walk_1,walk_2,walk_3";
            ArchMage.texture_path = "ArchMage";
            ArchMage.icon = "iconArchMage";
            ArchMage.job = "attacker";
            AssetManager.actor_library.add(ArchMage);
            AssetManager.actor_library.CallMethod("loadShadow", ArchMage);
            AssetManager.actor_library.CallMethod("addTrait", "AutoImmune");
            AssetManager.actor_library.CallMethod("addTrait", "ArchMage");
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "immune");
            LocalizationUtility.AddLocalization(ArchMage.nameLocale, ArchMage.nameLocale);


            KingdomAsset ArchMageRaceKingdom = new KingdomAsset();
            ArchMageRaceKingdom.id = "ArchMageRace";
            ArchMageRaceKingdom.mobs = true;
            ArchMageRaceKingdom.addTag("ArchMageRace");
            /*ArchMageRaceKingdom.addTag("good");
			ArchMageRaceKingdom.addTag("nature_creature");
			ArchMageRaceKingdom.addFriendlyTag("ArchMageRace");
			ArchMageRaceKingdom.addFriendlyTag("MaleVampireRace");
			ArchMageRaceKingdom.addFriendlyTag("neutral");/*/
            ArchMageRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(ArchMageRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", ArchMageRaceKingdom);

        }

        public static void create_MageHunter()
        {
            var MageHunter = AssetManager.actor_library.clone("MageHunter", "_mob");
            MageHunter.nameLocale = "MageHunter";
            MageHunter.nameTemplate = "dragonslayer_name";
            MageHunter.race = "MageHunterRace";
            MageHunter.kingdom = "MageHunterRace";
            MageHunter.base_stats[S.max_age] = 101;
            MageHunter.base_stats[S.knockback_reduction] = 10f;
            MageHunter.base_stats[S.critical_chance] = 50;
            MageHunter.base_stats[S.attack_speed] = 70f;
            MageHunter.base_stats[S.knockback] = 0f;
            MageHunter.base_stats[S.accuracy] = 70f;
            MageHunter.base_stats[S.health] = 1200;
            MageHunter.base_stats[S.speed] = 25;
            MageHunter.base_stats[S.damage] = 63;
            MageHunter.base_stats[S.targets] = 5;
            MageHunter.base_stats[S.dodge] = 100f;
            MageHunter.base_stats[S.armor] = 30;
            MageHunter.base_stats[S.scale] = 0.12f;
            MageHunter.immune_to_tumor = true;
            MageHunter.immune_to_injuries = true;
            MageHunter.color = Toolbox.makeColor("#000000", -1f);
            MageHunter.immune_to_slowness = true;
            MageHunter.canBeKilledByDivineLight = false;
            MageHunter.canBeKilledByLifeEraser = true;
            MageHunter.ignoredByInfinityCoin = false;
            MageHunter.disableJumpAnimation = true;
            MageHunter.canBeMovedByPowers = true;
            MageHunter.canTurnIntoZombie = false;
            MageHunter.canAttackBuildings = true;
            MageHunter.hideFavoriteIcon = false;
            MageHunter.can_edit_traits = true;
            MageHunter.very_high_flyer = false;
            MageHunter.damagedByOcean = false;
            MageHunter.swampCreature = false;
            MageHunter.damagedByRain = false;
            MageHunter.oceanCreature = false;
            MageHunter.landCreature = true;
            MageHunter.dieOnGround = false;
            MageHunter.take_items = false;
            MageHunter.use_items = true;
            MageHunter.diet_meat = false;
            MageHunter.dieInLava = false;
            MageHunter.needFood = false;
            MageHunter.has_soul = true;
            MageHunter.flying = false;
            MageHunter.canBeCitizen = true;
            MageHunter.defaultWeapons = List.Of<string>(new string[]
           {  "RedStoneBow" });
            MageHunter.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            MageHunter.sound_hit = "event:/SFX/HIT/HitMetal";
            MageHunter.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            MageHunter.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            MageHunter.animation_swim = "swim_0,swim_1,swim_2,swim_3";
            MageHunter.texture_path = "MageHunter";
            MageHunter.icon = "iconMageHunter";
            MageHunter.job = "attacker";
            AssetManager.actor_library.add(MageHunter);
            AssetManager.actor_library.CallMethod("loadShadow", MageHunter);
            AssetManager.actor_library.CallMethod("addTrait", "mageslayer");
            AssetManager.actor_library.CallMethod("addTrait", "poison_immune");
            AssetManager.actor_library.CallMethod("addTrait", "freeze_proof");
            AssetManager.actor_library.CallMethod("addTrait", "fire_proof");
            LocalizationUtility.AddLocalization(MageHunter.nameLocale, MageHunter.nameLocale);


            KingdomAsset MageHunterRaceKingdom = new KingdomAsset();
            MageHunterRaceKingdom.id = "MageHunterRace";
            MageHunterRaceKingdom.mobs = true;
            MageHunterRaceKingdom.addTag("MageHunterRace");
            /*MageHunterRaceKingdom.addTag("good");
			MageHunterRaceKingdom.addTag("nature_creature");
			MageHunterRaceKingdom.addFriendlyTag("MageHunterRace");
			MageHunterRaceKingdom.addFriendlyTag("MaleVampireRace");
			MageHunterRaceKingdom.addFriendlyTag("neutral");/*/
            MageHunterRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(MageHunterRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", MageHunterRaceKingdom);

        }

        public static void create_evilfish()
        {
            var evilfish = AssetManager.actor_library.clone("evilfish", "_mob");
            evilfish.id = "evilfish";
            evilfish.nameLocale = "evilfish";
            evilfish.nameTemplate = "dragonslayer_name";
            evilfish.race = "evilfishRace";
            evilfish.kingdom = "evilfishRace";
            evilfish.base_stats[S.max_age] = 20;
            evilfish.base_stats[S.knockback_reduction] = 10f;
            evilfish.base_stats[S.critical_chance] = 50;
            evilfish.base_stats[S.attack_speed] = 70f;
            evilfish.base_stats[S.knockback] = 0f;
            evilfish.base_stats[S.accuracy] = 70f;
            evilfish.base_stats[S.health] = 150;
            evilfish.base_stats[S.speed] = 95;
            evilfish.base_stats[S.damage] = 100;
            evilfish.base_stats[S.targets] = 5;
            evilfish.base_stats[S.armor] = 10;
            evilfish.base_stats[S.scale] = 0.12f;
            evilfish.color = Toolbox.makeColor("#000000", -1f);
            evilfish.immune_to_tumor = true;
            evilfish.immune_to_injuries = true;
            evilfish.immune_to_slowness = true;
            evilfish.canBeKilledByDivineLight = false;
            evilfish.canBeKilledByLifeEraser = true;
            evilfish.ignoredByInfinityCoin = false;
            evilfish.disableJumpAnimation = true;
            evilfish.canBeMovedByPowers = true;
            evilfish.canTurnIntoZombie = false;
            evilfish.canAttackBuildings = true;
            evilfish.hideFavoriteIcon = false;
            evilfish.can_edit_traits = true;
            evilfish.very_high_flyer = false;
            evilfish.damagedByOcean = false;
            evilfish.swampCreature = false;
            evilfish.damagedByRain = false;
            evilfish.oceanCreature = true;
            evilfish.landCreature = true;
            evilfish.dieOnGround = false;
            evilfish.take_items = false;
            evilfish.use_items = true;
            evilfish.diet_meat = false;
            evilfish.dieInLava = false;
            evilfish.needFood = false;
            evilfish.has_soul = true;
            evilfish.flying = false;
            evilfish.canBeCitizen = true;
            evilfish.sound_hit = "event:/SFX/HIT/HitMetal";
            evilfish.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            evilfish.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            evilfish.animation_swim = "swim_0,swim_1,swim_2";
            evilfish.texture_path = "evilfish";
            evilfish.icon = "iconevilfish";
            evilfish.job = "attacker";
            AssetManager.actor_library.add(evilfish);
            AssetManager.actor_library.CallMethod("loadShadow", evilfish);
            LocalizationUtility.AddLocalization(evilfish.nameLocale, evilfish.nameLocale);


            KingdomAsset evilfishRaceKingdom = new KingdomAsset();
            evilfishRaceKingdom.id = "evilfishRace";
            evilfishRaceKingdom.mobs = true;
            evilfishRaceKingdom.addTag("evilfishRace");
            /*evilfishRaceKingdom.addTag("good");
			evilfishRaceKingdom.addTag("nature_creature");
			evilfishRaceKingdom.addFriendlyTag("evilfishRace");
			evilfishRaceKingdom.addFriendlyTag("MaleVampireRace");
			evilfishRaceKingdom.addFriendlyTag("neutral");/*/
            evilfishRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(evilfishRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", evilfishRaceKingdom);

        }

        public static void create_GrandMage()
        {
            var GrandMage = AssetManager.actor_library.clone("GrandMage", "_mob");
            GrandMage.nameLocale = "GrandMage";
            GrandMage.nameTemplate = "GrandMage_name";
            GrandMage.race = "GrandMageRace";
            GrandMage.kingdom = "GrandMageRace";
            GrandMage.base_stats[S.max_age] = 1000;
            GrandMage.base_stats[S.knockback_reduction] = 10f;
            GrandMage.base_stats[S.critical_chance] = 50;
            GrandMage.base_stats[S.attack_speed] = 70f;
            GrandMage.base_stats[S.knockback] = 0f;
            GrandMage.base_stats[S.accuracy] = 70f;
            GrandMage.base_stats[S.health] = 1800;
            GrandMage.base_stats[S.speed] = 95;
            GrandMage.base_stats[S.damage] = 50;
            GrandMage.base_stats[S.targets] = 5;
            GrandMage.base_stats[S.dodge] = 100f;
            GrandMage.base_stats[S.armor] = 30;
            GrandMage.base_stats[S.scale] = 0.12f;
            GrandMage.immune_to_tumor = true;
            GrandMage.immune_to_injuries = true;
            GrandMage.effect_teleport = "fx_teleport_WE";
            GrandMage.color = Toolbox.makeColor("#000000", -1f);
            GrandMage.immune_to_slowness = true;
            GrandMage.canBeKilledByDivineLight = false;
            GrandMage.canBeKilledByLifeEraser = true;
            GrandMage.ignoredByInfinityCoin = false;
            GrandMage.disableJumpAnimation = true;
            GrandMage.canBeMovedByPowers = true;
            GrandMage.canTurnIntoZombie = false;
            GrandMage.canAttackBuildings = true;
            GrandMage.hideFavoriteIcon = false;
            GrandMage.can_edit_traits = true;
            GrandMage.very_high_flyer = false;
            GrandMage.damagedByOcean = false;
            GrandMage.swampCreature = false;
            GrandMage.damagedByRain = false;
            GrandMage.oceanCreature = false;
            GrandMage.landCreature = true;
            GrandMage.dieOnGround = false;
            GrandMage.take_items = false;
            GrandMage.use_items = true;
            GrandMage.diet_meat = false;
            GrandMage.dieInLava = false;
            GrandMage.needFood = false;
            GrandMage.has_soul = true;
            GrandMage.flying = false;
            GrandMage.canBeCitizen = true;
            GrandMage.defaultWeapons = List.Of<string>(new string[]
           {  "GrandStaff" });
            GrandMage.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            GrandMage.sound_hit = "event:/SFX/HIT/HitMetal";
            GrandMage.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            GrandMage.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            GrandMage.texture_path = "GrandMage";
            GrandMage.icon = "iconGrandMage";
            GrandMage.job = "attacker";
            AssetManager.actor_library.add(GrandMage);
            AssetManager.actor_library.CallMethod("loadShadow", GrandMage);
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "AutoImmune");
            AssetManager.actor_library.CallMethod("addTrait", "ArchMage");
            LocalizationUtility.AddLocalization(GrandMage.nameLocale, GrandMage.nameLocale);


            KingdomAsset GrandMageRaceKingdom = new KingdomAsset();
            GrandMageRaceKingdom.id = "GrandMageRace";
            GrandMageRaceKingdom.mobs = true;
            GrandMageRaceKingdom.addTag("GrandMageRace");
            /*GrandMageRaceKingdom.addTag("good");
			GrandMageRaceKingdom.addTag("nature_creature");
			GrandMageRaceKingdom.addFriendlyTag("GrandMageRace");
			GrandMageRaceKingdom.addFriendlyTag("MaleVampireRace");
			GrandMageRaceKingdom.addFriendlyTag("neutral");/*/
            GrandMageRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(GrandMageRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", GrandMageRaceKingdom);

        }

        public static void create_MageLord()
        {
            var MageLord = AssetManager.actor_library.clone("MageLord", "_mob");
            MageLord.nameLocale = "MageLord";
            MageLord.nameTemplate = "dragonslayer_name";
            MageLord.race = "MageLordRace";
            MageLord.kingdom = "MageLordRace";
            MageLord.base_stats[S.max_age] = 1000;
            MageLord.base_stats[S.knockback_reduction] = 10f;
            MageLord.base_stats[S.critical_chance] = 50;
            MageLord.base_stats[S.attack_speed] = 70f;
            MageLord.base_stats[S.knockback] = 0f;
            MageLord.base_stats[S.accuracy] = 70f;
            MageLord.base_stats[S.health] = 1100;
            MageLord.base_stats[S.speed] = 95;
            MageLord.base_stats[S.damage] = 100;
            MageLord.base_stats[S.targets] = 5;
            MageLord.base_stats[S.dodge] = 100f;
            MageLord.base_stats[S.armor] = 20;
            MageLord.base_stats[S.scale] = 0.12f;
            MageLord.immune_to_tumor = true;
            MageLord.immune_to_injuries = true;
            MageLord.color = Toolbox.makeColor("#000000", -1f);
            MageLord.immune_to_slowness = true;
            MageLord.canBeKilledByDivineLight = false;
            MageLord.canBeKilledByLifeEraser = true;
            MageLord.ignoredByInfinityCoin = false;
            MageLord.disableJumpAnimation = true;
            MageLord.canBeMovedByPowers = true;
            MageLord.canTurnIntoZombie = false;
            MageLord.canAttackBuildings = true;
            MageLord.hideFavoriteIcon = false;
            MageLord.can_edit_traits = true;
            MageLord.very_high_flyer = false;
            MageLord.damagedByOcean = false;
            MageLord.swampCreature = false;
            MageLord.damagedByRain = false;
            MageLord.oceanCreature = false;
            MageLord.landCreature = true;
            MageLord.dieOnGround = false;
            MageLord.take_items = false;
            MageLord.use_items = true;
            MageLord.dieInLava = false;
            MageLord.needFood = false;
            MageLord.has_soul = true;
            MageLord.flying = false;
            MageLord.canBeCitizen = true;
            MageLord.defaultWeapons = List.Of<string>(new string[]
           {  "LordStaff" });
            MageLord.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            MageLord.sound_hit = "event:/SFX/HIT/HitMetal";
            MageLord.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            MageLord.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            MageLord.texture_path = "MageLord";
            MageLord.icon = "iconMageLord";
            MageLord.job = "attacker";
            AssetManager.actor_library.add(MageLord);
            AssetManager.actor_library.CallMethod("loadShadow", MageLord);
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "immune");
            AssetManager.actor_library.CallMethod("addTrait", "poison_immune");
            AssetManager.actor_library.CallMethod("addTrait", "fire_proof");
            LocalizationUtility.AddLocalization(MageLord.nameLocale, MageLord.nameLocale);


            KingdomAsset MageLordRaceKingdom = new KingdomAsset();
            MageLordRaceKingdom.id = "MageLordRace";
            MageLordRaceKingdom.mobs = true;
            MageLordRaceKingdom.addTag("MageLordRace");
            /*MageLordRaceKingdom.addTag("good");
			MageLordRaceKingdom.addTag("nature_creature");
			MageLordRaceKingdom.addFriendlyTag("MageLordRace");
			MageLordRaceKingdom.addFriendlyTag("MaleVampireRace");
			MageLordRaceKingdom.addFriendlyTag("neutral");/*/
            MageLordRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(MageLordRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", MageLordRaceKingdom);

        }

        public static void create_Lich()
        {
            var Lich = AssetManager.actor_library.clone("Lich", "_mob");
            Lich.nameLocale = "Lich";
            Lich.nameTemplate = "dragonslayer_name";
            Lich.race = "LichRace";
            Lich.kingdom = "LichRace";
            Lich.base_stats[S.max_age] = 250;
            Lich.base_stats[S.knockback_reduction] = 10f;
            Lich.base_stats[S.critical_chance] = 50;
            Lich.base_stats[S.attack_speed] = 70f;
            Lich.base_stats[S.knockback] = 0f;
            Lich.base_stats[S.accuracy] = 70f;
            Lich.base_stats[S.health] = 1500;
            Lich.base_stats[S.speed] = 40;
            Lich.base_stats[S.damage] = 38;
            Lich.base_stats[S.targets] = 5;
            Lich.base_stats[S.dodge] = 100f;
            Lich.base_stats[S.armor] = 50;
            Lich.base_stats[S.scale] = 0.12f;
            Lich.immune_to_tumor = true;
            Lich.immune_to_injuries = true;
            Lich.color = Toolbox.makeColor("#000000", -1f);
            Lich.immune_to_slowness = true;
            Lich.canBeKilledByDivineLight = false;
            Lich.canBeKilledByLifeEraser = true;
            Lich.ignoredByInfinityCoin = false;
            Lich.disableJumpAnimation = true;
            Lich.canBeMovedByPowers = true;
            Lich.canTurnIntoZombie = false;
            Lich.canAttackBuildings = true;
            Lich.hideFavoriteIcon = false;
            Lich.can_edit_traits = true;
            Lich.very_high_flyer = false;
            Lich.damagedByOcean = false;
            Lich.swampCreature = false;
            Lich.damagedByRain = false;
            Lich.oceanCreature = false;
            Lich.landCreature = true;
            Lich.dieOnGround = false;
            Lich.take_items = false;
            Lich.use_items = true;
            Lich.diet_meat = false;
            Lich.dieInLava = false;
            Lich.needFood = false;
            Lich.has_soul = true;
            Lich.flying = false;
            Lich.canBeCitizen = true;
            Lich.defaultWeapons = List.Of<string>(new string[]
           {  "LichStaff" });
            Lich.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            Lich.sound_hit = "event:/SFX/HIT/HitMetal";
            Lich.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            Lich.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            Lich.animation_swim = "walk_0,walk_1,walk_2,walk_3";
            Lich.texture_path = "Lich";
            Lich.icon = "iconLich";
            Lich.job = "attacker";
            AssetManager.actor_library.add(Lich);
            AssetManager.actor_library.CallMethod("loadShadow", Lich);
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            LocalizationUtility.AddLocalization(Lich.nameLocale, Lich.nameLocale);


            KingdomAsset LichRaceKingdom = new KingdomAsset();
            LichRaceKingdom.id = "LichRace";
            LichRaceKingdom.mobs = true;
            LichRaceKingdom.addTag("LichRace");
            /*LichRaceKingdom.addTag("good");
			LichRaceKingdom.addTag("nature_creature");
			LichRaceKingdom.addFriendlyTag("LichRace");
			LichRaceKingdom.addFriendlyTag("MaleVampireRace");
			LichRaceKingdom.addFriendlyTag("neutral");/*/
            LichRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(LichRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", LichRaceKingdom);

        }

        public static void create_Robot()
        {
            var Robot = AssetManager.actor_library.clone("Robot", "_mob");
            Robot.nameLocale = "Robot";
            Robot.nameTemplate = "dragonslayer_name";
            Robot.race = "RobotRace";
            Robot.kingdom = "RobotRace";
            Robot.base_stats[S.max_age] = 20;
            Robot.base_stats[S.knockback_reduction] = 10f;
            Robot.base_stats[S.critical_chance] = 50;
            Robot.base_stats[S.attack_speed] = 70f;
            Robot.base_stats[S.knockback] = 0f;
            Robot.base_stats[S.accuracy] = 70f;
            Robot.base_stats[S.health] = 150;
            Robot.base_stats[S.speed] = 95;
            Robot.base_stats[S.damage] = 100;
            Robot.base_stats[S.targets] = 5;
            Robot.base_stats[S.armor] = 10;
            Robot.base_stats[S.scale] = 0.12f;
            Robot.color = Toolbox.makeColor("#000000", -1f);
            Robot.immune_to_tumor = true;
            Robot.immune_to_injuries = true;
            Robot.immune_to_slowness = true;
            Robot.canBeKilledByDivineLight = false;
            Robot.canBeKilledByLifeEraser = true;
            Robot.ignoredByInfinityCoin = false;
            Robot.disableJumpAnimation = true;
            Robot.canBeMovedByPowers = true;
            Robot.canTurnIntoZombie = false;
            Robot.canAttackBuildings = true;
            Robot.hideFavoriteIcon = false;
            Robot.can_edit_traits = true;
            Robot.very_high_flyer = false;
            Robot.damagedByOcean = false;
            Robot.swampCreature = false;
            Robot.damagedByRain = false;
            Robot.oceanCreature = true;
            Robot.landCreature = true;
            Robot.dieOnGround = false;
            Robot.take_items = false;
            Robot.use_items = true;
            Robot.diet_meat = false;
            Robot.dieInLava = false;
            Robot.needFood = false;
            Robot.has_soul = true;
            Robot.flying = false;
            Robot.action_death = (WorldAction)Delegate.Combine(Robot.action_death, new WorldAction(ActionLibrary.deathBomb));
            Robot.canBeCitizen = true;
            Robot.sound_hit = "event:/SFX/HIT/HitMetal";
            Robot.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            Robot.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            Robot.texture_path = "Robot";
            Robot.icon = "iconRobot";
            Robot.job = "attacker";
            AssetManager.actor_library.add(Robot);
            AssetManager.actor_library.CallMethod("loadShadow", Robot);
            LocalizationUtility.AddLocalization(Robot.nameLocale, Robot.nameLocale);


            KingdomAsset RobotRaceKingdom = new KingdomAsset();
            RobotRaceKingdom.id = "RobotRace";
            RobotRaceKingdom.mobs = true;
            RobotRaceKingdom.addTag("RobotRace");
            /*RobotRaceKingdom.addTag("good");
			RobotRaceKingdom.addTag("nature_creature");
			RobotRaceKingdom.addFriendlyTag("RobotRace");
			RobotRaceKingdom.addFriendlyTag("MaleVampireRace");
			RobotRaceKingdom.addFriendlyTag("neutral");/*/
            RobotRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(RobotRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", RobotRaceKingdom);

        }

        public static void create_Salmon()
        {
            var Salmon = AssetManager.actor_library.clone("Salmon", "_mob");
            Salmon.nameLocale = "Salmon";
            Salmon.nameTemplate = "dragonslayer_name";
            Salmon.race = "SalmonRace";
            Salmon.kingdom = "SalmonRace";
            Salmon.base_stats[S.max_age] = 20;
            Salmon.base_stats[S.knockback_reduction] = 10f;
            Salmon.base_stats[S.critical_chance] = 50;
            Salmon.base_stats[S.attack_speed] = 70f;
            Salmon.base_stats[S.knockback] = 0f;
            Salmon.base_stats[S.accuracy] = 70f;
            Salmon.base_stats[S.health] = 150;
            Salmon.base_stats[S.speed] = 95;
            Salmon.base_stats[S.damage] = 100;
            Salmon.base_stats[S.targets] = 5;
            Salmon.base_stats[S.armor] = 10;
            Salmon.base_stats[S.scale] = 0.12f;
            Salmon.color = Toolbox.makeColor("#000000", -1f);
            Salmon.immune_to_tumor = true;
            Salmon.immune_to_injuries = true;
            Salmon.immune_to_slowness = true;
            Salmon.canBeKilledByDivineLight = false;
            Salmon.canBeKilledByLifeEraser = true;
            Salmon.ignoredByInfinityCoin = false;
            Salmon.disableJumpAnimation = true;
            Salmon.canBeMovedByPowers = true;
            Salmon.canTurnIntoZombie = false;
            Salmon.canAttackBuildings = true;
            Salmon.hideFavoriteIcon = false;
            Salmon.can_edit_traits = true;
            Salmon.very_high_flyer = false;
            Salmon.damagedByOcean = false;
            Salmon.swampCreature = false;
            Salmon.damagedByRain = false;
            Salmon.oceanCreature = true;
            Salmon.landCreature = true;
            Salmon.dieOnGround = false;
            Salmon.take_items = false;
            Salmon.use_items = true;
            Salmon.diet_meat = false;
            Salmon.dieInLava = false;
            Salmon.needFood = false;
            Salmon.has_soul = true;
            Salmon.flying = false;
            Salmon.canBeCitizen = true;
            Salmon.sound_hit = "event:/SFX/HIT/HitMetal";
            Salmon.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            Salmon.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            Salmon.animation_swim = "swim_0,swim_1,swim_2";
            Salmon.texture_path = "Salmon";
            Salmon.icon = "iconSalmon";
            Salmon.job = "attacker";
            AssetManager.actor_library.add(Salmon);
            AssetManager.actor_library.CallMethod("loadShadow", Salmon);
            LocalizationUtility.AddLocalization(Salmon.nameLocale, Salmon.nameLocale);


            KingdomAsset SalmonRaceKingdom = new KingdomAsset();
            SalmonRaceKingdom.id = "SalmonRace";
            SalmonRaceKingdom.mobs = true;
            SalmonRaceKingdom.addTag("SalmonRace");
            /*SalmonRaceKingdom.addTag("good");
			SalmonRaceKingdom.addTag("nature_creature");
			SalmonRaceKingdom.addFriendlyTag("SalmonRace");
			SalmonRaceKingdom.addFriendlyTag("MaleVampireRace");
			SalmonRaceKingdom.addFriendlyTag("neutral");/*/
            SalmonRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(SalmonRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", SalmonRaceKingdom);

        }

        public static void create_BlueFish()
        {
            var BlueFish = AssetManager.actor_library.clone("BlueFish", "_mob");
            BlueFish.nameLocale = "BlueFish";
            BlueFish.nameTemplate = "dragonslayer_name";
            BlueFish.race = "BlueFishRace";
            BlueFish.kingdom = "BlueFishRace";
            BlueFish.base_stats[S.max_age] = 20;
            BlueFish.base_stats[S.knockback_reduction] = 10f;
            BlueFish.base_stats[S.critical_chance] = 50;
            BlueFish.base_stats[S.attack_speed] = 70f;
            BlueFish.base_stats[S.knockback] = 0f;
            BlueFish.base_stats[S.accuracy] = 70f;
            BlueFish.base_stats[S.health] = 150;
            BlueFish.base_stats[S.speed] = 95;
            BlueFish.base_stats[S.damage] = 100;
            BlueFish.base_stats[S.targets] = 5;
            BlueFish.base_stats[S.armor] = 10;
            BlueFish.base_stats[S.scale] = 0.12f;
            BlueFish.color = Toolbox.makeColor("#000000", -1f);
            BlueFish.immune_to_tumor = true;
            BlueFish.immune_to_injuries = true;
            BlueFish.immune_to_slowness = true;
            BlueFish.canBeKilledByDivineLight = false;
            BlueFish.canBeKilledByLifeEraser = true;
            BlueFish.ignoredByInfinityCoin = false;
            BlueFish.disableJumpAnimation = true;
            BlueFish.canBeMovedByPowers = true;
            BlueFish.canTurnIntoZombie = false;
            BlueFish.canAttackBuildings = true;
            BlueFish.hideFavoriteIcon = false;
            BlueFish.can_edit_traits = true;
            BlueFish.very_high_flyer = false;
            BlueFish.damagedByOcean = false;
            BlueFish.swampCreature = false;
            BlueFish.damagedByRain = false;
            BlueFish.oceanCreature = true;
            BlueFish.landCreature = true;
            BlueFish.dieOnGround = false;
            BlueFish.take_items = false;
            BlueFish.use_items = true;
            BlueFish.diet_meat = false;
            BlueFish.dieInLava = false;
            BlueFish.needFood = false;
            BlueFish.has_soul = true;
            BlueFish.flying = false;
            BlueFish.canBeCitizen = true;
            BlueFish.sound_hit = "event:/SFX/HIT/HitMetal";
            BlueFish.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            BlueFish.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            BlueFish.animation_swim = "swim_0,swim_1,swim_2";
            BlueFish.texture_path = "BlueFish";
            BlueFish.icon = "iconBlueFish";
            BlueFish.job = "attacker";
            AssetManager.actor_library.add(BlueFish);
            AssetManager.actor_library.CallMethod("loadShadow", BlueFish);
            LocalizationUtility.AddLocalization(BlueFish.nameLocale, BlueFish.nameLocale);


            KingdomAsset BlueFishRaceKingdom = new KingdomAsset();
            BlueFishRaceKingdom.id = "BlueFishRace";
            BlueFishRaceKingdom.mobs = true;
            BlueFishRaceKingdom.addTag("BlueFishRace");
            /*BlueFishRaceKingdom.addTag("good");
			BlueFishRaceKingdom.addTag("nature_creature");
			BlueFishRaceKingdom.addFriendlyTag("BlueFishRace");
			BlueFishRaceKingdom.addFriendlyTag("MaleVampireRace");
			BlueFishRaceKingdom.addFriendlyTag("neutral");/*/
            BlueFishRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(BlueFishRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", BlueFishRaceKingdom);

        }

        public static void create_DemonLord()
        {
            var DemonLord = AssetManager.actor_library.clone("DemonLord", "_mob");
            DemonLord.nameLocale = "DemonLord";
            DemonLord.nameTemplate = "dragonslayer_name";
            DemonLord.race = "DemonLordRace";
            DemonLord.kingdom = "DemonLordRace";
            DemonLord.base_stats[S.max_age] = 20;
            DemonLord.base_stats[S.knockback_reduction] = 10f;
            DemonLord.base_stats[S.critical_chance] = 50;
            DemonLord.base_stats[S.attack_speed] = 70f;
            DemonLord.base_stats[S.knockback] = 0f;
            DemonLord.base_stats[S.accuracy] = 70f;
            DemonLord.base_stats[S.health] = 150;
            DemonLord.base_stats[S.speed] = 30;
            DemonLord.base_stats[S.damage] = 100;
            DemonLord.base_stats[S.targets] = 5;
            DemonLord.base_stats[S.armor] = 10;
            DemonLord.base_stats[S.scale] = 0.12f;
            DemonLord.color = Toolbox.makeColor("#000000", -1f);
            DemonLord.immune_to_tumor = true;
            DemonLord.immune_to_injuries = true;
            DemonLord.immune_to_slowness = true;
            DemonLord.canBeKilledByDivineLight = false;
            DemonLord.canBeKilledByLifeEraser = true;
            DemonLord.ignoredByInfinityCoin = false;
            DemonLord.disableJumpAnimation = true;
            DemonLord.canBeMovedByPowers = true;
            DemonLord.canTurnIntoZombie = false;
            DemonLord.canAttackBuildings = true;
            DemonLord.hideFavoriteIcon = false;
            DemonLord.can_edit_traits = true;
            DemonLord.very_high_flyer = false;
            DemonLord.damagedByOcean = false;
            DemonLord.swampCreature = false;
            DemonLord.damagedByRain = false;
            DemonLord.oceanCreature = true;
            DemonLord.landCreature = true;
            DemonLord.dieOnGround = false;
            DemonLord.take_items = false;
            DemonLord.use_items = true;
            DemonLord.diet_meat = false;
            DemonLord.dieInLava = false;
            DemonLord.needFood = false;
            DemonLord.has_soul = true;
            DemonLord.flying = false;
            DemonLord.canBeCitizen = true;
            DemonLord.defaultWeapons = List.Of<string>(new string[]
               { "DemonStaff"});
            DemonLord.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            DemonLord.sound_hit = "event:/SFX/HIT/HitMetal";
            DemonLord.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            DemonLord.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            DemonLord.texture_path = "DemonLord";
            DemonLord.icon = "iconDemonLord";
            DemonLord.job = "attacker";
            AssetManager.actor_library.add(DemonLord);
            AssetManager.actor_library.CallMethod("loadShadow", DemonLord);
            LocalizationUtility.AddLocalization(DemonLord.nameLocale, DemonLord.nameLocale);


            KingdomAsset DemonLordRaceKingdom = new KingdomAsset();
            DemonLordRaceKingdom.id = "DemonLordRace";
            DemonLordRaceKingdom.mobs = true;
            DemonLordRaceKingdom.addTag("DemonLordRace");
            /*DemonLordRaceKingdom.addTag("good");
			DemonLordRaceKingdom.addTag("nature_creature");
			DemonLordRaceKingdom.addFriendlyTag("DemonLordRace");
			DemonLordRaceKingdom.addFriendlyTag("MaleVampireRace");
			DemonLordRaceKingdom.addFriendlyTag("neutral");/*/
            DemonLordRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(DemonLordRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", DemonLordRaceKingdom);

        }

        public static void create_GojoSatoru()
        {
            var GojoSatoru = AssetManager.actor_library.clone("GojoSatoru", "_mob");
            GojoSatoru.nameLocale = "GojoSatoru";
            GojoSatoru.id = "GojoSatoru";
            GojoSatoru.nameTemplate = "dragonslayer_name";
            GojoSatoru.race = "GojoSatoruRace";
            GojoSatoru.kingdom = "GojoSatoruRace";
            GojoSatoru.base_stats[S.max_age] = 20;
            GojoSatoru.base_stats[S.knockback_reduction] = 10f;
            GojoSatoru.base_stats[S.critical_chance] = 50;
            GojoSatoru.base_stats[S.attack_speed] = 70f;
            GojoSatoru.base_stats[S.knockback] = 0f;
            GojoSatoru.base_stats[S.accuracy] = 70f;
            GojoSatoru.base_stats[S.health] = 150;
            GojoSatoru.base_stats[S.speed] = 30;
            GojoSatoru.base_stats[S.damage] = 100;
            GojoSatoru.base_stats[S.targets] = 5;
            GojoSatoru.base_stats[S.armor] = 10;
            GojoSatoru.base_stats[S.scale] = 0.12f;
            GojoSatoru.color = Toolbox.makeColor("#000000", -1f);
            GojoSatoru.immune_to_tumor = true;
            GojoSatoru.immune_to_injuries = true;
            GojoSatoru.immune_to_slowness = true;
            GojoSatoru.canBeKilledByDivineLight = false;
            GojoSatoru.canBeKilledByLifeEraser = true;
            GojoSatoru.ignoredByInfinityCoin = false;
            GojoSatoru.disableJumpAnimation = true;
            GojoSatoru.canBeMovedByPowers = true;
            GojoSatoru.canTurnIntoZombie = false;
            GojoSatoru.canAttackBuildings = true;
            GojoSatoru.hideFavoriteIcon = false;
            GojoSatoru.can_edit_traits = true;
            GojoSatoru.very_high_flyer = false;
            GojoSatoru.damagedByOcean = false;
            GojoSatoru.swampCreature = false;
            GojoSatoru.damagedByRain = false;
            GojoSatoru.oceanCreature = true;
            GojoSatoru.landCreature = true;
            GojoSatoru.dieOnGround = false;
            GojoSatoru.take_items = false;
            GojoSatoru.use_items = true;
            GojoSatoru.diet_meat = false;
            GojoSatoru.dieInLava = false;
            GojoSatoru.needFood = false;
            GojoSatoru.has_soul = true;
            GojoSatoru.flying = false;
            GojoSatoru.canBeCitizen = true;
            GojoSatoru.defaultWeapons = List.Of<string>(new string[]
               { "DemonStaff"});
            GojoSatoru.defaultWeaponsMaterial = List.Of<string>(new string[] { "adamantine" });
            GojoSatoru.sound_hit = "event:/SFX/HIT/HitMetal";
            GojoSatoru.action_liquid = new WorldAction(ActionLibrary.swimToIsland);
            GojoSatoru.animation_walk = "walk_0,walk_1,walk_2,walk_3";
            GojoSatoru.texture_path = "GojoSatoru";
            GojoSatoru.icon = "iconGojoSatoru";
            GojoSatoru.job = "attacker";
            AssetManager.actor_library.add(GojoSatoru);
            AssetManager.actor_library.CallMethod("loadShadow", GojoSatoru);
            LocalizationUtility.AddLocalization(GojoSatoru.nameLocale, GojoSatoru.nameLocale);


            KingdomAsset GojoSatoruRaceKingdom = new KingdomAsset();
            GojoSatoruRaceKingdom.id = "GojoSatoruRace";
            GojoSatoruRaceKingdom.mobs = true;
            GojoSatoruRaceKingdom.addTag("GojoSatoruRace");
            /*GojoSatoruRaceKingdom.addTag("good");
			GojoSatoruRaceKingdom.addTag("nature_creature");
			GojoSatoruRaceKingdom.addFriendlyTag("GojoSatoruRace");
			GojoSatoruRaceKingdom.addFriendlyTag("MaleVampireRace");
			GojoSatoruRaceKingdom.addFriendlyTag("neutral");/*/
            GojoSatoruRaceKingdom.default_kingdom_color = new ColorAsset("#BACADD", "#BACADD", "#BACADD");
            AssetManager.kingdoms.add(GojoSatoruRaceKingdom);
            MapBox.instance.kingdoms.CallMethod("newHiddenKingdom", GojoSatoruRaceKingdom);

        }

        public static void create_Sus()
        {
            var Sus = AssetManager.actor_library.clone("Sus", "_mob");
            Sus.nameLocale = "White Mage";
            Sus.body_separate_part_head = false;
            Sus.body_separate_part_hands = true;
            Sus.race = SK.good;
            Sus.hideOnMinimap = true;
            Sus.kingdom = SK.good;
            Sus.base_stats[S.health] = 300f;
            Sus.base_stats[S.speed] = 30f;
            Sus.base_stats[S.damage] = 1f;
            Sus.base_stats[S.knockback] = 1f;
            Sus.base_stats[S.targets] = 1f;
            Sus.dieInLava = true;
            Sus.needFood = false;
            Sus.color = Toolbox.makeColor("#EE3A42", -1f);
            Sus.canTurnIntoZombie = true;
            Sus.zombieID = SA.zombie;
            Sus.skeletonID = "skeleton_cursed";
            Sus.job = "white_mage";
            Sus.attack_spells = List.Of<string>(new string[]
            {
            "teleportRandom",
            "bloodRain",
            "bloodRain",
            "shield",
            "lightning",
            "tornado",
            "fire",
            "curse",
            "spawnSkeleton",
            "cure",
            "spawnskeleton"
            });
            Sus.effect_teleport = "fx_teleport_blue";
            Sus.effect_cast_top = "fx_cast_top_blue";
            Sus.effect_cast_ground = "fx_cast_ground_blue";
            Sus.use_items = true;
            Sus.defaultWeapons = List.Of<string>(new string[]
            {
            "white_staff"
            });
            Sus.disableJumpAnimation = true;
            Sus.has_soul = true;
            Sus.kingdom = SK.evil;
            Sus.action_death = (WorldAction)Delegate.Combine(Sus.action_death, new WorldAction(ActionLibrary.mageSlayer));
            AssetManager.actor_library.CallMethod("addTrait", "fire_proof");
            AssetManager.actor_library.CallMethod("addTrait", "freeze_proof");
            AssetManager.actor_library.CallMethod("addTrait", "immortal");
            AssetManager.actor_library.CallMethod("addTrait", "regeneration");
            Sus.fmod_spawn = "event:/SFX/UNITS/WhiteMage/WhiteMageSpawn";
            Sus.fmod_attack = "event:/SFX/UNITS/WhiteMage/WhiteMageAttack";
            Sus.fmod_idle = "event:/SFX/UNITS/WhiteMage/WhiteMageIdle";
            Sus.fmod_death = "event:/SFX/UNITS/WhiteMage/WhiteMageDeath";
            AssetManager.actor_library.add(Sus);
            AssetManager.actor_library.CallMethod("loadShadow", Sus);
            LocalizationUtility.AddLocalization(Sus.nameLocale, Sus.nameLocale);
            NeoModLoader.General.LM.ApplyLocale(Sus.nameLocale);
        }

        public static bool MagnetShroom_1(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            MagEnd(a);
            return true;
        }
        public static bool MagEnd(Actor a)
        {
            a.animationContainer = ActorAnimationLoader.loadAnimationUnit("actors/ArchMage", a.asset);
            a.checkSpriteHead();
            ActorRenderer.drawActor(a);
            return true;
        }
    }
}
