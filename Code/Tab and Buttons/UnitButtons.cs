using NeoModLoader.General;
using ReflectionUtility;
using System;
using UnityEngine;

namespace Unit
{

    class UnitButtons
    {
        public static void init()
        {
            Debug.Log("UnitButtons.init called.");
            UnitTab.CreateTab("Button_Tab_Unit", "Tab_Unit", "Unit", "Spawn Unit World beings directly in your world!!", -150);

            loadButtons();
        }

        private static void loadButtons()
        {
            PowersTab UnitworldTab = PowerButtonCreator.GetTab("Tab_Unit");
            if (UnitworldTab == null)
            {
                Debug.LogError("UnitworldTab is null. Ensure the tab exists and is correctly named.");
                return;
            }
            var DragonSlayer = new GodPower();
            DragonSlayer.id = "spawnDragonSlayer";
            DragonSlayer.actorSpawnHeight = 3f;
            DragonSlayer.name = "spawnDragonSlayer";
            DragonSlayer.multiple_spawn_tip = true;
            DragonSlayer.spawnSound = "spawndragon";
            DragonSlayer.actor_asset_id = "DragonSlayer";
            DragonSlayer.showSpawnEffect = true;
            DragonSlayer.multiple_spawn_tip = true;
            DragonSlayer.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(DragonSlayer);

            var buttonDragonSlayer = PowerButtonCreator.CreateGodPowerButton(
            "spawnDragonSlayer",
            Resources.Load<Sprite>("units_embed/units/iconDragonSlayer"),
                UnitworldTab.transform,
                new Vector2(72, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonDragonSlayer, UnitworldTab, new Vector2(72, 18));


            var MaleVampire = new GodPower();
            MaleVampire.id = "spawnMaleVampire";
            MaleVampire.actorSpawnHeight = 3f;
            MaleVampire.name = "spawnMaleVampire";
            MaleVampire.multiple_spawn_tip = true;
            MaleVampire.spawnSound = "spawndragon";
            MaleVampire.actor_asset_id = "MaleVampire";
            MaleVampire.showSpawnEffect = true;
            MaleVampire.multiple_spawn_tip = true;
            MaleVampire.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(MaleVampire);

            var buttonMaleVampire = PowerButtonCreator.CreateGodPowerButton(
             "spawnMaleVampire",
                          Resources.Load<Sprite>("units_embed/units/iconMaleVampire.png"),
             UnitworldTab.transform,
             new Vector2(72, -18)
             );
            PowerButtonCreator.AddButtonToTab(buttonMaleVampire, UnitworldTab, new Vector2(72, -18));

            var FeMaleVampire = new GodPower();
            FeMaleVampire.id = "spawnFeMaleVampire";
            FeMaleVampire.actorSpawnHeight = 3f;
            FeMaleVampire.name = "spawnFeMaleVampire";
            FeMaleVampire.multiple_spawn_tip = true;
            FeMaleVampire.spawnSound = "spawndragon";
            FeMaleVampire.actor_asset_id = "FeMaleVampire";
            FeMaleVampire.showSpawnEffect = true;
            FeMaleVampire.multiple_spawn_tip = true;
            FeMaleVampire.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(FeMaleVampire);

            var buttonFeMaleVampire = PowerButtonCreator.CreateGodPowerButton(
             "spawnFeMaleVampire",
                          Resources.Load<Sprite>("units_embed/units/iconFeMaleVampire.png"),
            UnitworldTab.transform,
            new Vector2(112, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonFeMaleVampire, UnitworldTab, new Vector2(112, 18));

            var SoulStealer = new GodPower();
            SoulStealer.id = "spawnSoulStealer";
            SoulStealer.actorSpawnHeight = 3f;
            SoulStealer.name = "spawnSoulStealer";
            SoulStealer.multiple_spawn_tip = true;
            SoulStealer.spawnSound = "spawndragon";
            SoulStealer.actor_asset_id = "SoulStealer";
            SoulStealer.showSpawnEffect = true;
            SoulStealer.multiple_spawn_tip = true;
            SoulStealer.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(SoulStealer);

            var buttonSoulStealer = PowerButtonCreator.CreateGodPowerButton(
            "spawnSoulStealer",
                         Resources.Load<Sprite>("units_embed/units/iconSoulStealer.png"),
            UnitworldTab.transform,
            new Vector2(112, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonSoulStealer, UnitworldTab, new Vector2(112, -18));


            var MageHunter = new GodPower();
            MageHunter.id = "spawnMageHunter";
            MageHunter.actorSpawnHeight = 3f;
            MageHunter.name = "spawnMageHunter";
            MageHunter.multiple_spawn_tip = true;
            MageHunter.spawnSound = "spawndragon";
            MageHunter.actor_asset_id = "MageHunter";
            MageHunter.showSpawnEffect = true;
            MageHunter.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(MageHunter);

            var buttonMageHunter = PowerButtonCreator.CreateGodPowerButton(
                "spawnMageHunter",
                             Resources.Load<Sprite>("units_embed/units/iconMageHunter.png"),
                UnitworldTab.transform,
                new Vector2(152, 18)
            );

            PowerButtonCreator.AddButtonToTab(buttonMageHunter, UnitworldTab, new Vector2(152, 18));


            var ArchMage = new GodPower();
            ArchMage.id = "spawnArchMage";
            ArchMage.actorSpawnHeight = 3f;
            ArchMage.name = "spawnArchMage";
            ArchMage.multiple_spawn_tip = true;
            ArchMage.spawnSound = "spawndragon";
            ArchMage.actor_asset_id = "ArchMage";
            ArchMage.showSpawnEffect = true;
            ArchMage.multiple_spawn_tip = true;
            ArchMage.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(ArchMage);

            var buttonArchMage = PowerButtonCreator.CreateGodPowerButton(
            "spawnArchMage",
                         Resources.Load<Sprite>("units_embed/units/iconArchMage.png"),
            UnitworldTab.transform,
            new Vector2(152, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonArchMage, UnitworldTab, new Vector2(152, -18));

            var evilfish = new GodPower();
            evilfish.id = "spawnevilfish";
            evilfish.actorSpawnHeight = 3f;
            evilfish.name = "spawnevilfish";
            evilfish.multiple_spawn_tip = true;
            evilfish.spawnSound = "spawndragon";
            evilfish.actor_asset_id = "evilfish";
            evilfish.showSpawnEffect = true;
            evilfish.multiple_spawn_tip = true;
            evilfish.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(evilfish);

            var buttonevilfish = PowerButtonCreator.CreateGodPowerButton(
            "spawnevilfish",
                         Resources.Load<Sprite>("units_embed/units/iconevilfish.png"),
            UnitworldTab.transform,
            new Vector2(192, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonevilfish, UnitworldTab, new Vector2(192, 18));

            var GrandMage = new GodPower();
            GrandMage.id = "spawnGrandMage";
            GrandMage.actorSpawnHeight = 3f;
            GrandMage.name = "spawnGrandMage";
            GrandMage.multiple_spawn_tip = true;
            GrandMage.spawnSound = "spawndragon";
            GrandMage.actor_asset_id = "GrandMage";
            GrandMage.showSpawnEffect = true;
            GrandMage.multiple_spawn_tip = true;
            GrandMage.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(GrandMage);

            var buttonGrandMage = PowerButtonCreator.CreateGodPowerButton(
            "spawnGrandMage",
                         Resources.Load<Sprite>("units_embed/units/iconGrandMage.png"),
            UnitworldTab.transform,
            new Vector2(192, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonGrandMage, UnitworldTab, new Vector2(192, -18));

            var MageLord = new GodPower();
            MageLord.id = "spawnMageLord";
            MageLord.actorSpawnHeight = 3f;
            MageLord.name = "spawnMageLord";
            MageLord.multiple_spawn_tip = true;
            MageLord.spawnSound = "spawndragon";
            MageLord.actor_asset_id = "MageLord";
            MageLord.showSpawnEffect = true;
            MageLord.multiple_spawn_tip = true;
            MageLord.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(MageLord);

            var buttonMageLord = PowerButtonCreator.CreateGodPowerButton(
            "spawnMageLord",
                         Resources.Load<Sprite>("units_embed/units/iconMageLord.png"),
            UnitworldTab.transform,
            new Vector2(232, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonMageLord, UnitworldTab, new Vector2(232, 18));


            var Lich = new GodPower();
            Lich.id = "spawnLich";
            Lich.actorSpawnHeight = 3f;
            Lich.name = "spawnLich";
            Lich.multiple_spawn_tip = true;
            Lich.spawnSound = "spawndragon";
            Lich.actor_asset_id = "Lich";
            Lich.showSpawnEffect = true;
            Lich.multiple_spawn_tip = true;
            Lich.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(Lich);

            var buttonLich = PowerButtonCreator.CreateGodPowerButton(
            "spawnLich",
                         Resources.Load<Sprite>("units_embed/units/iconLich.png"),
            UnitworldTab.transform,
            new Vector2(232, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonLich, UnitworldTab, new Vector2(232, -18));

            var Robot = new GodPower();
            Robot.id = "spawnRobot";
            Robot.actorSpawnHeight = 3f;
            Robot.name = "spawnRobot";
            Robot.multiple_spawn_tip = true;
            Robot.spawnSound = "spawndragon";
            Robot.actor_asset_id = "Robot";
            Robot.showSpawnEffect = true;
            Robot.multiple_spawn_tip = true;
            Robot.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(Robot);

            var buttonRobot = PowerButtonCreator.CreateGodPowerButton(
            "spawnRobot",
                         Resources.Load<Sprite>("units_embed/units/iconRobot.png"),
            UnitworldTab.transform,
            new Vector2(272, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonRobot, UnitworldTab, new Vector2(272, 18));

            var BlueFish = new GodPower();
            BlueFish.id = "spawnBlueFish";
            BlueFish.actorSpawnHeight = 3f;
            BlueFish.name = "spawnBlueFish";
            BlueFish.multiple_spawn_tip = true;
            BlueFish.spawnSound = "spawndragon";
            BlueFish.actor_asset_id = "BlueFish";
            BlueFish.showSpawnEffect = true;
            BlueFish.multiple_spawn_tip = true;
            BlueFish.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(BlueFish);

            var buttonBlueFish = PowerButtonCreator.CreateGodPowerButton(
            "spawnBlueFish",
                         Resources.Load<Sprite>("units_embed/units/iconBlueFish.png"),
            UnitworldTab.transform,
            new Vector2(272, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonBlueFish, UnitworldTab, new Vector2(272, -18));

            var Salmon = new GodPower();
            Salmon.id = "spawnSalmon";
            Salmon.actorSpawnHeight = 3f;
            Salmon.name = "spawnSalmon";
            Salmon.multiple_spawn_tip = true;
            Salmon.spawnSound = "spawndragon";
            Salmon.actor_asset_id = "Salmon";
            Salmon.showSpawnEffect = true;
            Salmon.multiple_spawn_tip = true;
            Salmon.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(Salmon);

            var buttonSalmon = PowerButtonCreator.CreateGodPowerButton(
            "spawnSalmon",
                         Resources.Load<Sprite>("units_embed/units/iconSalmon.png"),
            UnitworldTab.transform,
            new Vector2(312, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonSalmon, UnitworldTab, new Vector2(312, 18));

            var DemonLord = new GodPower();
            DemonLord.id = "spawnDemonLord";
            DemonLord.actorSpawnHeight = 3f;
            DemonLord.name = "spawnDemonLord";
            DemonLord.multiple_spawn_tip = true;
            DemonLord.spawnSound = "spawndragon";
            DemonLord.actor_asset_id = "DemonLord";
            DemonLord.showSpawnEffect = true;
            DemonLord.multiple_spawn_tip = true;
            DemonLord.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(DemonLord);

            var buttonDemonLord = PowerButtonCreator.CreateGodPowerButton(
            "spawnDemonLord",
                         Resources.Load<Sprite>("units_embed/units/iconDemonLord.png"),
            UnitworldTab.transform,
            new Vector2(312, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonDemonLord, UnitworldTab, new Vector2(312, -18));

            var GojoSatoru = new GodPower();
            GojoSatoru.id = "spawnGojoSatoru";
            GojoSatoru.actorSpawnHeight = 3f;
            GojoSatoru.name = "spawnGojoSatoru";
            GojoSatoru.multiple_spawn_tip = true;
            GojoSatoru.spawnSound = "spawndragon";
            GojoSatoru.actor_asset_id = "GojoSatoru";
            GojoSatoru.showSpawnEffect = true;
            GojoSatoru.multiple_spawn_tip = true;
            GojoSatoru.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(GojoSatoru);

            var buttonGojoSatoru = PowerButtonCreator.CreateGodPowerButton(
            "spawnGojoSatoru",
                         Resources.Load<Sprite>("units_embed/units/iconGojoSatoru.png"),
            UnitworldTab.transform,
            new Vector2(352, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonGojoSatoru, UnitworldTab, new Vector2(352, -18));

            #region spider

            var spider = new GodPower();
            spider.id = "spawnspider";
            spider.actorSpawnHeight = 3f;
            spider.name = "spawnspider";
            spider.multiple_spawn_tip = true;
            spider.spawnSound = "spawndragon";
            spider.actor_asset_id = "spider";
            spider.showSpawnEffect = true;
            spider.multiple_spawn_tip = true;
            spider.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(spider);

            var buttonspider = PowerButtonCreator.CreateGodPowerButton(
            "spawnspider",
                         Resources.Load<Sprite>("units_embed/units/iconspider.png"),
            UnitworldTab.transform,
            new Vector2(388, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonspider, UnitworldTab, new Vector2(388, -18));

            /*
            // Button setup in your UI code
            var allUnitsButton = new GodPower();
            allUnitsButton.id = "show_all_units";
            allUnitsButton.name = "Show All Units";
            allUnitsButton.click_action = new PowerActionWithID(UnitButtons1.callShowAllUnitsWindow);
            AssetManager.powers.add(allUnitsButton);

            // Create the button and add it to the UI
            var buttonAllUnits = PowerButtonCreator.CreateGodPowerButton(
                "show_all_units",
                Resources.Load<Sprite>("ui/icons/iconCultureList.png"),
                UnitworldTab.transform,
                new Vector2(604, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonAllUnits, UnitworldTab, new Vector2(604, 18));

            */

            #region newone

            var newone = new GodPower();
            newone.id = "spawnnewone";
            newone.actorSpawnHeight = 3f;
            newone.name = "spawnnewone";
            newone.multiple_spawn_tip = true;
            newone.spawnSound = "spawndragon";
            newone.actor_asset_id = "newone";
            newone.showSpawnEffect = true;
            newone.multiple_spawn_tip = true;
            newone.click_action = new PowerActionWithID(callSpawnUnit);
            AssetManager.powers.add(newone);

            var buttonnewone = PowerButtonCreator.CreateGodPowerButton(
            "spawnnewone",
                         Resources.Load<Sprite>("units_embed/units/iconDinosaw.png"),
            UnitworldTab.transform,
            new Vector2(388, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonnewone, UnitworldTab, new Vector2(388, 18));

            #endregion

            DropAsset spawnGrass = AssetManager.drops.clone("spawnZelkovaDrop", "_spawn_building");
            spawnGrass.path_texture = "drops/drop_seed_enchanted";
            spawnGrass.fallingSpeed = 3f;
            spawnGrass.action_landed = new DropsAction(action_seeds_zelkova);
            AssetManager.drops.add(spawnGrass);

            GodPower Grass_Drop = AssetManager.powers.clone("zelkova_drop", "_drops");
            Grass_Drop.showToolSizes = true;
            Grass_Drop.holdAction = true;
            Grass_Drop.fallingChance = 0.05f;
            Grass_Drop.dropID = "spawnZelkovaDrop";
            Grass_Drop.name = "Zelkova Seeds";
            Grass_Drop.click_power_action = new PowerAction(action_Drop);
            Grass_Drop.click_power_brush_action = new PowerAction(action_zelkova);
            AssetManager.powers.add(Grass_Drop);
            LocalizationUtility.addTraitToLocalizedLibrary(Grass_Drop.dropID, "Zelkova Seeds");


            var buttonZelkova = PowerButtonCreator.CreateGodPowerButton(
            "zelkova_drop",
                         Resources.Load<Sprite>("units_embed/Icon/iconZelkova.png"),
            UnitworldTab.transform,
            new Vector2(352, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonZelkova, UnitworldTab, new Vector2(352, 18));



            DropAsset warriorDrop = AssetManager.drops.clone("warrior_drop", "blessing");
            warriorDrop.action_landed = new DropsAction(UnitActionLibrary.action_warrior);

            GodPower warriorPower = AssetManager.powers.clone("warrior_drop", "_drops");
            warriorPower.id = "warrior_drop";
            warriorPower.name = "warrior_drop";
            warriorPower.dropID = "warrior_drop";
            warriorPower.fallingChance = 0.01f;
            warriorPower.click_power_action = new PowerAction(callSpawnDrops);
            warriorPower.click_power_brush_action = new PowerAction(callLoopBrush);
            AssetManager.powers.add(warriorPower);
            LocalizationUtility.addTraitToLocalizedLibrary(warriorPower.dropID, "warrior conversion");


            var buttonwarrior = PowerButtonCreator.CreateGodPowerButton(
            "warrior_drop",
             Resources.Load<Sprite>("ui/Icons/iconArmy.png"),
             UnitworldTab.transform,
             new Vector2(496, -18)
             );
            PowerButtonCreator.AddButtonToTab(buttonwarrior, UnitworldTab, new Vector2(496, -18));


            DropAsset greencardDrop = AssetManager.drops.clone("greencard_drop", "blessing");
            greencardDrop.action_landed = new DropsAction(UnitActionLibrary.action_force_join);

            GodPower greencard = AssetManager.powers.clone("greencard_drop", "_drops");
            greencard.id = "greencard_drop";
            greencard.name = "greencard_drop";
            greencard.dropID = "greencard_drop";
            greencard.fallingChance = 0.01f;
            greencard.click_power_action = new PowerAction(callSpawnDrops);
            greencard.click_power_brush_action = new PowerAction(callLoopBrush);
            AssetManager.powers.add(greencard);
            LocalizationUtility.addTraitToLocalizedLibrary(greencard.dropID, "got green card baby");


            var buttongreencard = PowerButtonCreator.CreateGodPowerButton(
            "greencard_drop",
             Resources.Load<Sprite>("ui/Icons/iconLucky.png"),
             UnitworldTab.transform,
             new Vector2(460, -18)
             );
            PowerButtonCreator.AddButtonToTab(buttongreencard, UnitworldTab, new Vector2(460, -18));



            DropAsset KingDrop = AssetManager.drops.clone("King_drop", "blessing");
            KingDrop.action_landed = new DropsAction(UnitActionLibrary.action_make_king);

            GodPower King = AssetManager.powers.clone("King_drop", "_drops");
            King.id = "King_drop";
            King.name = "The Crown";
            King.dropID = "King_drop";
            King.fallingChance = 0.01f;
            King.click_power_action = new PowerAction(callSpawnDrops);
            King.click_power_brush_action = new PowerAction(callLoopBrush);
            AssetManager.powers.add(King);
            LocalizationUtility.addTraitToLocalizedLibrary(King.dropID, "why me king");


            var buttonKing = PowerButtonCreator.CreateGodPowerButton(
            "King_drop",
             Resources.Load<Sprite>("ui/Icons/iconKings.png"),
             UnitworldTab.transform,
             new Vector2(460, 18)
             );
            PowerButtonCreator.AddButtonToTab(buttonKing, UnitworldTab, new Vector2(460, 18));


            DropAsset PeasantDrop = AssetManager.drops.clone("Peasant_drop", "blessing");
            PeasantDrop.action_landed = new DropsAction(UnitActionLibrary.action_make_Peasant);

            GodPower Peasant = AssetManager.powers.clone("Peasant_drop", "_drops");
            Peasant.id = "Peasant_drop";
            Peasant.name = "Peasant_drop";
            Peasant.dropID = "Peasant_drop";
            Peasant.fallingChance = 0.01f;
            Peasant.click_power_action = new PowerAction(callSpawnDrops);
            Peasant.click_power_brush_action = new PowerAction(callLoopBrush);
            AssetManager.powers.add(Peasant);
            LocalizationUtility.addTraitToLocalizedLibrary(Peasant.dropID, "why me Peasant");


            var buttonPeasant = PowerButtonCreator.CreateGodPowerButton(
            "Peasant_drop",
             Resources.Load<Sprite>("ui/Icons/iconPeasant.png"),
             UnitworldTab.transform,
             new Vector2(532, 18)
             );
            PowerButtonCreator.AddButtonToTab(buttonPeasant, UnitworldTab, new Vector2(532, 18));

            DropAsset CityLeaderDrop = AssetManager.drops.clone("CityLeader_drop", "blessing");
            CityLeaderDrop.action_landed = new DropsAction(UnitActionLibrary.action_make_CityLeader);

            GodPower CityLeader = AssetManager.powers.clone("CityLeader_drop", "_drops");
            CityLeader.id = "CityLeader_drop";
            CityLeader.name = "CityLeader_drop";
            CityLeader.dropID = "CityLeader_drop";
            CityLeader.fallingChance = 0.01f;
            CityLeader.click_power_action = new PowerAction(callSpawnDrops);
            CityLeader.click_power_brush_action = new PowerAction(callLoopBrush);
            AssetManager.powers.add(CityLeader);
            LocalizationUtility.addTraitToLocalizedLibrary(CityLeader.dropID, "why me CityLeader");


            var buttonCityLeader = PowerButtonCreator.CreateGodPowerButton(
            "CityLeader_drop",
             Resources.Load<Sprite>("ui/Icons/iconCrownSilver.png"),
             UnitworldTab.transform,
             new Vector2(532, -18)
             );
            PowerButtonCreator.AddButtonToTab(buttonCityLeader, UnitworldTab, new Vector2(532, -18));


            DropAsset ClanLeaderDrop = AssetManager.drops.clone("ClanLeader_drop", "blessing");
            ClanLeaderDrop.action_landed = new DropsAction(UnitActionLibrary.action_make_clan_leader);

            GodPower ClanLeader = AssetManager.powers.clone("ClanLeader_drop", "_drops");
            ClanLeader.id = "ClanLeader_drop";
            ClanLeader.name = "ClanLeader_drop";
            ClanLeader.dropID = "ClanLeader_drop";
            ClanLeader.fallingChance = 0.01f;
            ClanLeader.click_power_action = new PowerAction(callSpawnDrops);
            ClanLeader.click_power_brush_action = new PowerAction(callLoopBrush);
            AssetManager.powers.add(ClanLeader);
            LocalizationUtility.addTraitToLocalizedLibrary(ClanLeader.dropID, "why me ClanLeader");


            var buttonClanLeader = PowerButtonCreator.CreateGodPowerButton(
            "ClanLeader_drop",
             Resources.Load<Sprite>("ui/Icons/iconClan.png"),
             UnitworldTab.transform,
             new Vector2(496, 18)
             );
            PowerButtonCreator.AddButtonToTab(buttonClanLeader, UnitworldTab, new Vector2(496, 18));


            DropAsset ArmyGroupLeaderDrop = AssetManager.drops.clone("ArmyGroupLeader_drop", "delta_rain");
            ArmyGroupLeaderDrop.action_landed = new DropsAction(UnitActionLibrary.action_make_army_leader);
            //   ArmyGroupLeaderDrop.action_landed = new DropsAction(DezombifyAction.DezombifyDropAction);
            ArmyGroupLeaderDrop.default_scale = 0.1f;
            ArmyGroupLeaderDrop.fallingSpeed = 3f;
            ArmyGroupLeaderDrop.material = "mat_world_object_lit";

            GodPower ArmyGroupLeader = AssetManager.powers.clone("ArmyGroupLeader_drop", "_drops");
            ArmyGroupLeader.id = "ArmyGroupLeader_drop";
            ArmyGroupLeader.name = "ArmyGroupLeader_drop";
            ArmyGroupLeader.dropID = "ArmyGroupLeader_drop";
            ArmyGroupLeader.fallingChance = 0.01f;
            ArmyGroupLeader.click_power_action = new PowerAction(callSpawnDrops);
            ArmyGroupLeader.click_power_brush_action = new PowerAction(callLoopBrush);
            AssetManager.powers.add(ArmyGroupLeader);
            LocalizationUtility.addTraitToLocalizedLibrary(ArmyGroupLeader.dropID, "why me ArmyGroupLeader");


            var buttonArmyGroupLeader = PowerButtonCreator.CreateGodPowerButton(
            "ArmyGroupLeader_drop",
             Resources.Load<Sprite>("ui/Icons/iconArmor.png"),
             UnitworldTab.transform,
             new Vector2(568, 18)
             );
            PowerButtonCreator.AddButtonToTab(buttonArmyGroupLeader, UnitworldTab, new Vector2(568, 18));


            // Create the GodPower for Whisper of Alliance
            GodPower whisperOfAlliance = new GodPower
            {
                id = "whisper_of_alliance",
                name = "Whisper of Alliance",
                force_map_text = MapMode.Kingdoms,
                path_icon = "iconAllianceWhisper"
            };

            whisperOfAlliance.select_button_action = new PowerButtonClickAction(WhisperofAlliance.selectWhisperOfAlliance);
            whisperOfAlliance.click_special_action = new PowerActionWithID(WhisperofAlliance.clickWhisperOfAlliance);
            AssetManager.powers.add(whisperOfAlliance);


            var buttonWhisperOfAlliance = PowerButtonCreator.CreateGodPowerButton(
                "whisper_of_alliance",
                Resources.Load<Sprite>("ui/icons/iconUnity.png"), // Use an alliance icon
                UnitworldTab.transform,
                new Vector2(568, -18)  // Adjust position as needed
            );
            PowerButtonCreator.AddButtonToTab(buttonWhisperOfAlliance, UnitworldTab, new Vector2(568, -18));
            LocalizationUtility.addTraitToLocalizedLibrary("whisper_of_alliance", "Form an Alliance between two kingdoms!");



            GodPower convertCityGodPower = new GodPower
            {
                id = "convert_city_to_kingdom",
                name = "Convert City to Another Kingdom",
                force_map_text = MapMode.Kingdoms,
                path_icon = "iconCityConversion"
            };

            convertCityGodPower.select_button_action = (PowerButtonClickAction)Delegate.Combine(convertCityGodPower.select_button_action, new PowerButtonClickAction(WhisperOfConversionAction.selectWhisperOfConversion));
            convertCityGodPower.click_special_action = (PowerActionWithID)Delegate.Combine(convertCityGodPower.click_special_action, new PowerActionWithID(WhisperOfConversionAction.clickWhisperOfConversion));
            convertCityGodPower.tester_enabled = false;
            AssetManager.powers.add(convertCityGodPower);

            var buttonConvertCity = PowerButtonCreator.CreateGodPowerButton(
                "convert_city_to_kingdom",
                Resources.Load<Sprite>("ui/icons/iconKingdom.png"),
                UnitworldTab.transform,
                new Vector2(640, -18)
            );
            PowerButtonCreator.AddButtonToTab(buttonConvertCity, UnitworldTab, new Vector2(640, -18));
            LocalizationUtility.addTraitToLocalizedLibrary("convert_city_to_kingdom", "Convert the selected city to the kingdom of another city!");


            DropAsset RacialDrop = AssetManager.drops.clone("Racial_drop", "blessing");
            RacialDrop.action_landed = new DropsAction(UnitActionLibrary.assignRandomAssetToActor);

            GodPower Racial = AssetManager.powers.clone("Racial_drop", "_drops");
            Racial.id = "Racial_drop";
            Racial.name = "Racial_drop";
            Racial.dropID = "Racial_drop";
            Racial.fallingChance = 0.01f;
            Racial.click_power_action = AssetManager.powers.spawnDrops;
            Racial.click_power_brush_action = AssetManager.powers.loopWithCurrentBrushPower;
            AssetManager.powers.add(Racial);
            LocalizationUtility.addTraitToLocalizedLibrary(Racial.dropID, "why me Racial");


            var buttonRacial = PowerButtonCreator.CreateGodPowerButton(
            "Racial_drop",
             Resources.Load<Sprite>("ui/Icons/iconBlessing.png"),
             UnitworldTab.transform,
             new Vector2(604, 18)
             );
            PowerButtonCreator.AddButtonToTab(buttonRacial, UnitworldTab, new Vector2(604, 18));


            DropAsset StrongestItemDrop = AssetManager.drops.clone("StrongestItem_drop", "blessing");
            StrongestItemDrop.action_landed = new DropsAction(ItemManager.AssignStrongestItemsToActor);

            GodPower StrongestItem = AssetManager.powers.clone("StrongestItem_drop", "_drops");
            StrongestItem.id = "StrongestItem_drop";
            StrongestItem.name = "StrongestItem_drop";
            StrongestItem.dropID = "StrongestItem_drop";
            StrongestItem.fallingChance = 0.01f;
            StrongestItem.click_power_action = new PowerAction(callSpawnDrops);
            StrongestItem.click_power_brush_action = AssetManager.powers.loopWithCurrentBrushPower;
            AssetManager.powers.add(StrongestItem);
            LocalizationUtility.addTraitToLocalizedLibrary(StrongestItem.dropID, "why me StrongestItem");


            var buttonStrongestItem = PowerButtonCreator.CreateGodPowerButton(
            "StrongestItem_drop",
             Resources.Load<Sprite>("ui/Icons/icon_tech_weapon_production.png"),
             UnitworldTab.transform,
             new Vector2(604, -18)
             );
            PowerButtonCreator.AddButtonToTab(buttonStrongestItem, UnitworldTab, new Vector2(604, -18));

            var spawnTestPlant = new GodPower
            {
                id = "spawnTestPlant",
                name = "Test Plant",
                spawnSound = "spawnbuilding", // Example, replace with actual sound if any
                click_action = new PowerActionWithID(callSpawnBuilding)
            };
            AssetManager.powers.add(spawnTestPlant);

            var buttonTestPlant = PowerButtonCreator.CreateGodPowerButton(
                "spawnTestPlant",
                Resources.Load<Sprite>("ui/Icons/icon_tech_weapon_production.png"), // Adjust the path to your actual sprite
                UnitworldTab.transform,
                new Vector2(640, 18)
            );
            PowerButtonCreator.AddButtonToTab(buttonTestPlant, UnitworldTab, new Vector2(640, 18));


            DropAsset CapitalconversionDrop = AssetManager.drops.clone("Capitalconversion_drop", "blessing");
            CapitalconversionDrop.action_landed = new DropsAction(UnitActionLibrary.action_make_capital);

            GodPower Capitalconversion = AssetManager.powers.clone("Capitalconversion_drop", "_drops");
            Capitalconversion.id = "Capitalconversion_drop";
            Capitalconversion.name = "Capitalconversion_drop";
            Capitalconversion.dropID = "Capitalconversion_drop";
            Capitalconversion.fallingChance = 0.01f;
            Capitalconversion.click_power_action = AssetManager.powers.spawnDrops;
            Capitalconversion.click_power_brush_action = AssetManager.powers.loopWithCurrentBrushPower;
            AssetManager.powers.add(Capitalconversion);
            LocalizationUtility.addTraitToLocalizedLibrary(Capitalconversion.dropID, "why me Capitalconversion");


            var buttonCapitalconversion = PowerButtonCreator.CreateGodPowerButton(
            "Capitalconversion_drop",
             Resources.Load<Sprite>("ui/Icons/iconCapital.png"),
             UnitworldTab.transform,
             new Vector2(676, 18)
             );
            PowerButtonCreator.AddButtonToTab(buttonCapitalconversion, UnitworldTab, new Vector2(676, 18));

            // Create Acid Drop GodPower
            var AcidDrop = new GodPower();
            AcidDrop.id = "spawnAcidDrop";
            AcidDrop.name = "Acid Drop";
            AcidDrop.dropID = "acid";
            AcidDrop.fallingChance = 0.01f;
            AcidDrop.click_power_action = new PowerAction(callSpawnDrops);
            AcidDrop.click_power_brush_action = AssetManager.powers.loopWithCurrentBrushPower;
            AssetManager.powers.add(AcidDrop);

            // Localization (Optional: Adjust as needed)
            LocalizationUtility.addTraitToLocalizedLibrary(AcidDrop.dropID, "Corrosive acid drop");

            // Create Button for Acid Drop
            var buttonAcidDrop = PowerButtonCreator.CreateGodPowerButton(
                "spawnAcidDrop",
                Resources.Load<Sprite>("ui/Icons/iconJungle.png"), // Make sure the path is correct to the acid icon
                UnitworldTab.transform,
                new Vector2(712, -18) // Adjust the position as necessary
            );

            // Add Button to the Tab
            PowerButtonCreator.AddButtonToTab(buttonAcidDrop, UnitworldTab, new Vector2(712, -18));

            // Create Custom Molten Rock Drop
            var BloodPool = new DropAsset();
            BloodPool.id = "molten_rock_drop";
            BloodPool.default_scale = 0.2f;
            BloodPool.fallingHeight = new Vector2(30f, 45f);
            BloodPool.sound_drop = "event:/SFX/DROPS/DropAcid";
            BloodPool.path_texture = "drops/drop_seed_enchanted";
            BloodPool.action_landed = new DropsAction(ActionBlood.ActionBloodTile);
            BloodPool.material = "mat_world_object_lit";
            AssetManager.drops.add(BloodPool);

            // Integrate with GodPower
            GodPower BloodPoolPower = new GodPower();
            BloodPoolPower.id = "spawnBloodPool";
            BloodPoolPower.name = "Molten Rock Drop";
            BloodPoolPower.dropID = "molten_rock_drop";
            BloodPoolPower.fallingChance = 0.01f;
            BloodPoolPower.click_power_action = new PowerAction(callSpawnDrops);
            BloodPoolPower.click_power_brush_action = action_zelkova;
            AssetManager.powers.add(BloodPoolPower);

            // Localization (Optional)
            LocalizationUtility.addTraitToLocalizedLibrary(BloodPoolPower.dropID, "Fiery molten rock drop!");

            // Create Button for Molten Rock Drop
            var buttonBloodPool = PowerButtonCreator.CreateGodPowerButton(
                "spawnBloodPool",
                Resources.Load<Sprite>("ui/Icons/iconJungle.png"),
                UnitworldTab.transform,
                new Vector2(712, 18)
            );

            // Add Button to the Tab
            PowerButtonCreator.AddButtonToTab(buttonBloodPool, UnitworldTab, new Vector2(712, 18));
        }

        public static void action_spawnBloodPoolTile(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null) return;

            TileType newTileType;

            // Check the type of the current tile and decide the new tile type to spawn
            if (pTile.Type.id == "deep_ocean")
            {
                // Custom tile for deep ocean tiles
                newTileType = AssetManager.tiles.get("deep_blood"); // Ensure this custom tile type exists
                Debug.Log("Transformed deep ocean to Volcanic Deep!");
            }
            else if (pTile.Type.id == "shallow_waters")
            {
                // Custom tile for shallow waters
                newTileType = AssetManager.tiles.get("shallow_blood"); // Ensure this custom tile type exists
                Debug.Log("Transformed shallow waters to Volcanic Shallow!");
            }
            else if (pTile.Type.id == "pit_deep_ocean")
            {
                // Custom tile for shallow waters
                newTileType = AssetManager.tiles.get("deep_blood"); // Ensure this custom tile type exists
                Debug.Log("Transformed shallow waters to Volcanic Shallow!");
            }
            else if (pTile.Type.id == "pit_shallow_waters")
            {
                // Custom tile for shallow waters
                newTileType = AssetManager.tiles.get("shallow_blood"); // Ensure this custom tile type exists
                Debug.Log("Transformed shallow waters to Volcanic Shallow!");
            }
            else if (pTile.Type.id == "pit_close_ocean")
            {
                // Custom tile for shallow waters
                newTileType = AssetManager.tiles.get("close_blood"); // Ensure this custom tile type exists
                Debug.Log("Transformed shallow waters to Volcanic Shallow!");
            }
            else if (pTile.Type.id == "close_ocean")
            {
                // Example: transform sand to a glass-like tile due to heat
                newTileType = AssetManager.tiles.get("close_blood");
                Debug.Log("Transformed sand to Molten Glass!");
            }
            else
            {
                // Default transformation
                newTileType = AssetManager.tiles.get("deep_blood"); // Default to a basic volcanic tile
                Debug.Log("Transformed tile to Volcanic Rock!");
            }

            // Apply the transformation
            MapAction.terraformMain(pTile, newTileType, TerraformLibrary.flash);
        }

        public static bool callSpawnBuilding(WorldTile pTile, string pPowerID)
        {
            if (pTile != null && pTile.zone != null && pTile.zone.hasCity())
            {
                City city = pTile.zone.city;

                if (city != null)
                {
                    Kingdom kingdom = city.kingdom;
                    string raceId = kingdom.data.raceID;
                    string buildingId;
                    switch (raceId)
                    {
                        case "human":
                            buildingId = "watch_tower_human";
                            break;
                        case "elf":
                            buildingId = "watch_tower_elf";
                            break;
                        case "orc":
                            buildingId = "watch_tower_orc";
                            break;
                        case "dwarf":
                            buildingId = "watch_tower_dwarf";
                            break;
                        default:
                            buildingId = "watch_tower_human";
                            break;
                    }
                    BuildingAsset building = AssetManager.buildings.get(buildingId);
                    Building spawnedBuilding = MapBox.instance.buildings.addBuilding(buildingId, pTile);
                    if (spawnedBuilding != null)
                    {
                        spawnedBuilding.setKingdom(kingdom, true);
                        Debug.Log($"Spawned {buildingId} on tile belonging to {kingdom.data.name} ({raceId}) and assigned to the kingdom.");
                    }
                    else
                    {
                        Debug.LogError("Failed to spawn building.");
                        return false;
                    }

                    return true;
                }
                else
                {
                    Debug.LogError("Tile does not belong to any city.");
                    return false;
                }
            }
            else
            {
                Debug.LogError("Tile does not belong to any valid zone or city.");
                return false;
            }
        }


        public static bool CallSpawnUnit(WorldTile pTile, string pPowerID)
        {
            AssetManager.powers.CallMethod("spawnUnit", pTile, pPowerID);
            return true;
        }

        public static bool callLoopBrush(WorldTile pTile, GodPower pPower)
        {
            AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower);
            return true;
        }

        public static bool callSpawnDrops(WorldTile tTile, GodPower pPower)
        {
            AssetManager.powers.CallMethod("spawnDrops", tTile, pPower);
            return true;
        }

        public static bool callSpawnUnit(WorldTile pTile, string pPowerID)
        {
            AssetManager.powers.CallMethod("spawnUnit", pTile, pPowerID);
            return true;
        }

        public static bool action_Drop(WorldTile pTile, GodPower pPower)
        {
            AssetManager.powers.CallMethod("spawnDrops", pTile, pPower);
            return true;
        }
        public static bool action_zelkova(WorldTile pTile, GodPower pPower)
        {
            AssetManager.powers.CallMethod("loopWithCurrentBrushPower", pTile, pPower);
            return true;
        }

        public static void action_seeds_zelkova(WorldTile pTile = null, string pDropID = null)
        {
            DropsLibrary.useDropSeedOn(pTile, AssetManager.topTiles.get("zelkova_high"), AssetManager.topTiles.get("zelkova_low"));
        }

        public static void action_seeds_blood(WorldTile pTile = null, string pDropID = null)
        {
            DropsLibrary.useDropSeedOn(pTile, AssetManager.topTiles.get("deep_acid"), AssetManager.topTiles.get("shallow_acid"));
        }
    }

}
#endregion