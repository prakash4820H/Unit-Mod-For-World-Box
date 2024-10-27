using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    class UnitTraits : MonoBehaviour
    {

        public static void init()
        {

            ActorTrait ArchMagePower = new ActorTrait();
            ArchMagePower.id = "ArchMage";
            ArchMagePower.path_icon = "ui/Icons/iconArchMage";
            ArchMagePower.group_id = UnitTraitGroup.Unit;
            ArchMagePower.opposite = "cursed";
            ArchMagePower.type = TraitType.Positive;
            ArchMagePower.can_be_given = true;
            ArchMagePower.action_attack_target = new AttackAction(ActionLibrary.restoreHealthOnHit);
            ArchMagePower.action_attack_target += UnitActionLibrary.Powers;
            ArchMagePower.action_special_effect += UnitActionLibrary.Teleport;
            AssetManager.traits.add(ArchMagePower);
            LocalizationUtility.addTraitToLocalizedLibrary(ArchMagePower.id, "The ArchMage Power.");
            PlayerConfig.unlockTrait("ArchMage");

            ActorTrait GrandMagePower = new ActorTrait();
            GrandMagePower.id = "GrandMage";
            GrandMagePower.path_icon = "ui/Icons/iconGrandMage";
            GrandMagePower.group_id = UnitTraitGroup.Unit;
            GrandMagePower.opposite = "cursed";
            GrandMagePower.can_be_given = true;
            GrandMagePower.type = TraitType.Positive;
            GrandMagePower.action_attack_target += UnitActionLibrary.GrandMageAttack;
            GrandMagePower.action_special_effect += UnitActionLibrary.Teleport;
            GrandMagePower.action_death += UnitActionLibrary.Lightning;
            AssetManager.traits.add(GrandMagePower);
            LocalizationUtility.addTraitToLocalizedLibrary(GrandMagePower.id, "The GrandMage Power.");
            PlayerConfig.unlockTrait("GrandMage");


            ActorTrait DefendersTrait = new ActorTrait();
            DefendersTrait.id = "DefendersTrait";
            DefendersTrait.path_icon = "ui/Icons/iconDefendersTrait";
            DefendersTrait.group_id = UnitTraitGroup.Unit;
            DefendersTrait.can_be_given = true;
            DefendersTrait.action_get_hit += UnitActionLibrary.DefenderEffect;
            AssetManager.traits.add(DefendersTrait);
            LocalizationUtility.addTraitToLocalizedLibrary(DefendersTrait.id, "Defenders Trait. Stops and kills enemy units below level 3 who try to attack.");
            PlayerConfig.unlockTrait("DefendersTrait");

            ActorTrait Test1 = new ActorTrait();
            Test1.id = "Trails";
            Test1.path_icon = "ui/Icons/iconMageLord";
            Test1.group_id = UnitTraitGroup.Unit;
            Test1.opposite = "cursed";
            //    Test1.action_attack_target = new AttackAction(UnitActionLibrary.DashAttackAction);

            //   Test1.action_special_effect = UnitActionLibrary.anti;
            //     Test1.action_special_effect = UnitActionLibrary.Spike_end3;
            AssetManager.traits.add(Test1);
            LocalizationUtility.addTraitToLocalizedLibrary(Test1.id, "The test.");
            PlayerConfig.unlockTrait("Trails");


            ActorTrait AutoImmune = new ActorTrait();
            AutoImmune.id = "AutoImmune";
            AutoImmune.path_icon = "ui/Icons/iconAutoImmune";
            AutoImmune.group_id = UnitTraitGroup.Unit;
            AutoImmune.opposite = "cursed";
            AutoImmune.type = TraitType.Positive;
            AutoImmune.can_be_given = true;
            AutoImmune.action_special_effect += UnitActionLibrary.Immunity;
            AssetManager.traits.add(AutoImmune);
            LocalizationUtility.addTraitToLocalizedLibrary(AutoImmune.id, "He is immune to anything and everything");
            PlayerConfig.unlockTrait("AutoImmune");


            ActorTrait DragonSlayerAura = new ActorTrait();
            DragonSlayerAura.id = "DragonSlayerAura";
            DragonSlayerAura.path_icon = "ui/Icons/iconBurningFeet";
            DragonSlayerAura.group_id = UnitTraitGroup.Unit;
            DragonSlayerAura.opposite = "cursed";
            DragonSlayerAura.type = TraitType.Positive;
            DragonSlayerAura.can_be_given = true;
            DragonSlayerAura.action_special_effect += UnitActionLibrary.Aura;
            AssetManager.traits.add(DragonSlayerAura);
            LocalizationUtility.addTraitToLocalizedLibrary(DragonSlayerAura.id, "He is the DragonSlayer");
            PlayerConfig.unlockTrait("DragonSlayerAura");


            ActorTrait cursed = AssetManager.traits.get("cursed");
            cursed.type = TraitType.Negative;

            ActorTrait blessed = AssetManager.traits.get("blessed");
            blessed.type = TraitType.Positive;

            ActorTrait miracle_born = AssetManager.traits.get("miracle_born");
            miracle_born.type = TraitType.Positive;

            ActorTrait scar_of_divinity = AssetManager.traits.clone("scar_of_divinity", "scar_of_divinity");
            scar_of_divinity.id = "scar_of_divinity";
            scar_of_divinity.can_be_given = true;
            scar_of_divinity.can_be_removed = true;

            ActorTrait Life = new ActorTrait();
            Life.id = "Revive";
            Life.group_id = UnitTraitGroup.Unit;
            Life.path_icon = "ui/Icons/iconRevive";
            Life.action_death += UnitActionLibrary.LastChance;
            AssetManager.traits.add(Life);
            LocalizationUtility.addTraitToLocalizedLibrary(Life.id, "The Last Chance");
            PlayerConfig.unlockTrait("Revive");


            ActorTrait FS = new ActorTrait();
            FS.id = "FS";
            FS.group_id = UnitTraitGroup.Unit;
            FS.path_icon = "ui/Icons/iconFS";
            FS.action_special_effect = UnitActionLibrary.FinishAllEffects;
            AssetManager.traits.add(FS);
            LocalizationUtility.addTraitToLocalizedLibrary(FS.id, "FS");
            PlayerConfig.unlockTrait("FS");

            ActorTrait Void = new ActorTrait();
            Void.id = "Erased";
            Void.group_id = UnitTraitGroup.Unit;
            Void.path_icon = "ui/Icons/iconVoid";
            Void.action_special_effect += UnitActionLibrary.remove_unit;
            AssetManager.traits.add(Void);
            LocalizationUtility.addTraitToLocalizedLibrary(Void.id, "The erasing ");
            PlayerConfig.unlockTrait("Erased");

            ActorTrait STemp = new ActorTrait();
            STemp.id = "Short Temper";
            STemp.path_icon = "ui/Icons/iconSoulRage1";
            STemp.group_id = UnitTraitGroup.Unit;
            STemp.can_be_given = true;
            STemp.type = TraitType.Positive;
            STemp.action_get_hit += UnitActionLibrary.Stemp;
            STemp.action_get_hit += UnitActionLibrary.FS;
            AssetManager.traits.add(STemp);
            LocalizationUtility.addTraitToLocalizedLibrary(STemp.id, "Bro has Short Temper");
            PlayerConfig.unlockTrait("Short Temper");

            ActorTrait ZelkovaTouch = new ActorTrait();
            ZelkovaTouch.id = "zelkova touch";
            ZelkovaTouch.path_icon = "ui/Icons/iconSoulRage";
            ZelkovaTouch.group_id = UnitTraitGroup.Unit;
            ZelkovaTouch.can_be_given = true;
            ZelkovaTouch.type = TraitType.Positive;
            ZelkovaTouch.action_special_effect += UnitActionLibrary.zelkovaTouchEffect;
            AssetManager.traits.add(ZelkovaTouch);
            LocalizationUtility.addTraitToLocalizedLibrary(ZelkovaTouch.id, "zelkova touch");
            PlayerConfig.unlockTrait("zelkova touch");

            var zombie_infection = AssetManager.traits.get("infected");
            zombie_infection.action_special_effect = DezombifyAction.InfectedSpecialAction;

            var gg = AssetManager.drops.get("delta_rain");
            gg.action_landed = new DropsAction(action_delta_rain1);
        }

        public static void action_delta_rain1(WorldTile pTile = null, string pDropID = null)
        {
            List<string> trait_editor_delta = PlayerConfig.instance.data.trait_editor_delta;
            UnitActionLibrary.removeTraitRain(pTile, trait_editor_delta);
        }
    }
}
