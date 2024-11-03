using ai;
using NeoModLoader.General;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    class UnitItems
    {
        public static void Init()
        {

            ItemAsset DragonSlayerSword = AssetManager.items.clone("DragonSlayerSword", "_melee");
            DragonSlayerSword.id = "DragonSlayerSword";
            DragonSlayerSword.name_templates = Toolbox.splitStringIntoList(new string[]
            {
          "sword_name#30",
          "sword_name_king#3",
          "weapon_name_city",
          "weapon_name_kingdom",
          "weapon_name_culture",
          "weapon_name_enemy_king",
          "weapon_name_enemy_kingdom"
            });
            DragonSlayerSword.materials = List.Of<string>(new string[] { "adamantine" });
            DragonSlayerSword.base_stats[S.attack_speed] = 20;
            DragonSlayerSword.base_stats[S.damage] = 30;
            DragonSlayerSword.base_stats[S.speed] = 30f;
            DragonSlayerSword.base_stats[S.range] = 2;
            DragonSlayerSword.base_stats[S.armor] = 10;
            DragonSlayerSword.equipment_value = 10000;
            DragonSlayerSword.path_slash_animation = "effects/slashes/slash_sword";
            DragonSlayerSword.tech_needed = "weapon_sword";
            DragonSlayerSword.equipmentType = EquipmentType.Weapon;
            DragonSlayerSword.name_class = "item_class_weapon";
            DragonSlayerSword.action_attack_target = new AttackAction(Swordeffect);
            AssetManager.items.add(DragonSlayerSword);
            LocalizationUtility.AddLocalization("item_DragonSlayerSword", "Dragon Slayer Sword");
            addGunsSprite(DragonSlayerSword.id, DragonSlayerSword.materials[0]);


            ItemAsset MageSlayerSword = AssetManager.items.clone("MageSlayerSword", "_melee");
            MageSlayerSword.id = "MageSlayerSword";
            MageSlayerSword.name_templates = Toolbox.splitStringIntoList(new string[]
            {
          "sword_name#30",
          "sword_name_king#3",
          "weapon_name_city",
          "weapon_name_kingdom",
          "weapon_name_culture",
          "weapon_name_enemy_king",
          "weapon_name_enemy_kingdom"
            });
            MageSlayerSword.materials = List.Of<string>(new string[] { "adamantine" });
            MageSlayerSword.base_stats[S.attack_speed] = 20;
            MageSlayerSword.base_stats[S.damage] = 30;
            MageSlayerSword.base_stats[S.speed] = 30f;
            MageSlayerSword.base_stats[S.range] = 10;
            MageSlayerSword.base_stats[S.armor] = 10;
            MageSlayerSword.equipment_value = 10000;
            MageSlayerSword.path_slash_animation = "effects/slashes/slash_sword";
            MageSlayerSword.tech_needed = "weapon_sword";
            MageSlayerSword.equipmentType = EquipmentType.Weapon;
            MageSlayerSword.name_class = "item_class_weapon";
            AssetManager.items.add(MageSlayerSword);
            LocalizationUtility.AddLocalization("item_MageSlayerSword", "Mage Slayer Sword");
            addGunsSprite(MageSlayerSword.id, MageSlayerSword.materials[0]);


            ItemAsset GiantAxe = AssetManager.items.clone("GiantAxe", "_melee");
            GiantAxe.id = "GiantAxe";
            GiantAxe.name_templates = Toolbox.splitStringIntoList(new string[]
            {
          "sword_name#30",
          "sword_name_king#3",
          "weapon_name_city",
          "weapon_name_kingdom",
          "weapon_name_culture",
          "weapon_name_enemy_king",
          "weapon_name_enemy_kingdom"
            });
            GiantAxe.materials = List.Of<string>(new string[] { "adamantine" });
            GiantAxe.base_stats[S.attack_speed] = 5;
            GiantAxe.base_stats[S.damage] = 30;
            GiantAxe.base_stats[S.speed] = 30f;
            GiantAxe.base_stats[S.range] = 10;
            GiantAxe.base_stats[S.armor] = 10;
            GiantAxe.equipment_value = 10000;
            GiantAxe.path_slash_animation = "effects/slashes/slash_sword";
            GiantAxe.tech_needed = "weapon_sword";
            GiantAxe.equipmentType = EquipmentType.Weapon;
            GiantAxe.name_class = "item_class_weapon";
            AssetManager.items.add(GiantAxe);
            LocalizationUtility.AddLocalization("item_GiantAxe", "Giant Axe");
            addGunsSprite(GiantAxe.id, GiantAxe.materials[0]);

            ItemAsset RedStoneBow = AssetManager.items.clone("RedStoneBow", "_range");
            RedStoneBow.id = "RedStoneBow";
            RedStoneBow.name_templates = Toolbox.splitStringIntoList(new string[]
            {
          "bow_name#30",
          "sword_name_king#3",
          "weapon_name_city",
          "weapon_name_kingdom",
          "weapon_name_culture",
          "weapon_name_enemy_king",
          "weapon_name_enemy_kingdom"
            });
            RedStoneBow.materials = List.Of<string>(new string[] { "adamantine" });
            RedStoneBow.projectile = "RedStoneArrow";
            RedStoneBow.base_stats[S.attack_speed] = 20;
            RedStoneBow.base_stats[S.damage] = 2;
            RedStoneBow.base_stats[S.speed] = 30f;
            RedStoneBow.base_stats[S.range] = 2f;
            RedStoneBow.base_stats[S.armor] = 10;
            RedStoneBow.base_stats[S.critical_chance] = 0.1f;
            RedStoneBow.base_stats[S.damage_range] = 0.6f;
            // RedStoneBow.base_stats[S.projectiles] = 20f;
            RedStoneBow.action_attack_target = new AttackAction(RedSAeffect);
            RedStoneBow.equipment_value = 10000;
            RedStoneBow.path_slash_animation = "effects/slashes/slash_punch";
            RedStoneBow.tech_needed = "weapon_bow";
            RedStoneBow.equipmentType = EquipmentType.Weapon;
            RedStoneBow.name_class = "item_class_weapon";
            AssetManager.items.add(RedStoneBow);
            LocalizationUtility.AddLocalization("item_RedStoneBow", "RedStoneBow");
            addGunsSprite(RedStoneBow.id, RedStoneBow.materials[0]);


            ItemAsset WhiteSword = AssetManager.items.clone("WhiteSword", "_melee");
            WhiteSword.id = "WhiteSword";
            WhiteSword.name_templates = Toolbox.splitStringIntoList(new string[]
            {
          "sword_name#30",
          "sword_name_king#3",
          "weapon_name_city",
          "weapon_name_kingdom",
          "weapon_name_culture",
          "weapon_name_enemy_king",
          "weapon_name_enemy_kingdom"
            });
            WhiteSword.materials = List.Of<string>(new string[] { "adamantine" });
            WhiteSword.base_stats[S.attack_speed] = 20;
            WhiteSword.base_stats[S.damage] = 30;
            WhiteSword.base_stats[S.speed] = 30f;
            WhiteSword.base_stats[S.range] = 2f;
            WhiteSword.base_stats[S.armor] = 10;
            WhiteSword.action_attack_target = Forced;
            WhiteSword.equipment_value = 10000;
            WhiteSword.path_slash_animation = "effects/slashes/slash_sword";
            WhiteSword.tech_needed = "weapon_sword";
            WhiteSword.equipmentType = EquipmentType.Weapon;
            WhiteSword.name_class = "item_class_weapon";
            WhiteSword.action_attack_target = new AttackAction(Forced);
            AssetManager.items.add(WhiteSword);
            LocalizationUtility.AddLocalization("item_WhiteSword", "White Sword");
            addGunsSprite(WhiteSword.id, WhiteSword.materials[0]);


            ItemAsset GrandStaff = AssetManager.items.clone("GrandStaff", "_range");
            GrandStaff.id = "GrandStaff";
            GrandStaff.name_templates = Toolbox.splitStringIntoList(new string[]
            {
            "white_staff_name"
            });
            GrandStaff.projectile = "WindMagic";
            GrandStaff.materials = List.Of<string>(new string[] { "adamantine" });
            GrandStaff.base_stats[S.attack_speed] = 20;
            GrandStaff.base_stats[S.damage] = 30;
            GrandStaff.base_stats[S.speed] = 30f;
            GrandStaff.base_stats[S.range] = 2f;
            GrandStaff.base_stats[S.armor] = 10;
            GrandStaff.equipment_value = 1000;
            GrandStaff.path_slash_animation = "effects/slashes/slash_sword";
            GrandStaff.tech_needed = "weapon_sword";
            GrandStaff.base_stats[S.projectiles] = 2f;
            GrandStaff.equipmentType = EquipmentType.Weapon;
            GrandStaff.name_class = "item_class_weapon";
            AssetManager.items.add(GrandStaff);
            LM.Add("item_GrandStaff", "Grand Staff", "hi");
            addGunsSprite(GrandStaff.id, GrandStaff.materials[0]);


            ItemAsset EStaff = AssetManager.items.clone("EStaff", "_range");
            EStaff.id = "EStaff";
            EStaff.name_templates = Toolbox.splitStringIntoList(new string[]
            {
            "white_staff_name"
            });
            EStaff.projectile = "WindMagic";
            EStaff.materials = List.Of<string>(new string[] { "adamantine" });
            EStaff.base_stats[S.attack_speed] = 20;
            EStaff.base_stats[S.damage] = 30;
            EStaff.base_stats[S.speed] = 30f;
            EStaff.base_stats[S.range] = 2f;
            EStaff.base_stats[S.armor] = 10;
            EStaff.equipment_value = 10000;
            EStaff.action_special_effect = new WorldAction(PC);
            EStaff.path_slash_animation = "effects/slashes/slash_sword";
            EStaff.tech_needed = "weapon_sword";
            EStaff.base_stats[S.projectiles] = 2f;
            EStaff.equipmentType = EquipmentType.Weapon;
            EStaff.name_class = "item_class_weapon";
            AssetManager.items.add(EStaff);
            LocalizationUtility.AddLocalization("item_EStaff", "Grand Staff");
            addGunsSprite(EStaff.id, EStaff.materials[0]);



            ItemAsset LichStaff = AssetManager.items.clone("LichStaff", "_range");
            LichStaff.id = "LichStaff";
            LichStaff.name_templates = Toolbox.splitStringIntoList(new string[]
            {
            "white_staff_name"
            });
            LichStaff.projectile = "blackMagic";
            LichStaff.materials = List.Of<string>(new string[] { "adamantine" });
            LichStaff.base_stats[S.attack_speed] = 20;
            LichStaff.base_stats[S.damage] = 30;
            LichStaff.base_stats[S.speed] = 30f;
            LichStaff.base_stats[S.range] = 2f;
            LichStaff.base_stats[S.armor] = 10;
            LichStaff.equipment_value = 10000;
            LichStaff.path_slash_animation = "effects/slashes/slash_sword";
            LichStaff.tech_needed = "weapon_sword";
            LichStaff.equipmentType = EquipmentType.Weapon;
            LichStaff.action_attack_target = Mercy;
            LichStaff.name_class = "item_class_weapon";
            AssetManager.items.add(LichStaff);
            LocalizationUtility.AddLocalization("item_LichStaff", "Lich Staff");
            addGunsSprite(LichStaff.id, LichStaff.materials[0]);


            ItemAsset itemAsset9 = AssetManager.items.clone("ArmorD", "_equipment");
            itemAsset9.id = "ArmorD";
            itemAsset9.name_templates = Toolbox.splitStringIntoList("white_staff_name");
            itemAsset9.projectile = "freeze_orb";
            itemAsset9.materials = new List<string> { "adamantine" };
            itemAsset9.base_stats[S.speed] = 30f;
            itemAsset9.base_stats[S.armor] = 10f;
            itemAsset9.equipment_value = 10000;
            itemAsset9.equipmentType = EquipmentType.Armor;
            itemAsset9.name_class = "item_class_armor";
            itemAsset9.path_icon = "ui/Icons/items/icon_armorD"; // Specify the icon path
            AssetManager.items.add(itemAsset9);
            LocalizationUtility.AddLocalization("item_ArmorD", "ArmorD");
            addGunsSprite(itemAsset9.id, itemAsset9.materials[0]);


            ItemAsset DemonStaff = AssetManager.items.clone("DemonStaff", "_range");
            DemonStaff.id = "DemonStaff";
            DemonStaff.name_templates = Toolbox.splitStringIntoList(new string[]
            {
            "white_staff_name"
            });
            DemonStaff.projectile = "freeze_orb";
            DemonStaff.materials = List.Of<string>(new string[] { "adamantine" });
            DemonStaff.base_stats[S.attack_speed] = 20;
            DemonStaff.base_stats[S.damage] = 30;
            DemonStaff.base_stats[S.speed] = 30f;
            DemonStaff.base_stats[S.range] = 2f;
            DemonStaff.base_stats[S.armor] = 10;
            DemonStaff.action_attack_target = new AttackAction((attacker, target, pTile) =>
            {
                bool success = ActionLibrary.restoreHealthOnHit(attacker, target, pTile);
                Actor attackerActor = ReflectionUtility.GetFieldValue<Actor>(attacker, "a");
                if (attackerActor != null)
                {
                    attackerActor.base_data.health += 100;
                    attackerActor.base_data.health = Math.Min(attackerActor.base_data.health, (int)attackerActor.stats.get(S.health));
                }

                return success;
            });
            DemonStaff.equipment_value = 10000;
            DemonStaff.path_slash_animation = "effects/slashes/slash_sword";
            DemonStaff.tech_needed = "weapon_sword";
            DemonStaff.equipmentType = EquipmentType.Weapon;
            DemonStaff.name_class = "item_class_weapon";
            AssetManager.items.add(DemonStaff);
            LocalizationUtility.AddLocalization("item_DemonStaff", "Lord Staff");
            addGunsSprite(DemonStaff.id, DemonStaff.materials[0]);



            ItemAsset LordStaff = AssetManager.items.clone("LordStaff", "_range");
            LordStaff.id = "LordStaff";
            LordStaff.name_templates = Toolbox.splitStringIntoList(new string[]
            {
            "white_staff_name"
            });
            LordStaff.projectile = "freeze_orb";
            LordStaff.materials = List.Of<string>(new string[] { "adamantine" });
            LordStaff.base_stats[S.attack_speed] = 20;
            LordStaff.base_stats[S.damage] = 30;
            LordStaff.base_stats[S.speed] = 30f;
            LordStaff.base_stats[S.range] = 2f;
            LordStaff.base_stats[S.armor] = 10;
            LordStaff.equipment_value = 10000;
            LordStaff.path_slash_animation = "effects/slashes/slash_sword";
            LordStaff.tech_needed = "weapon_sword";
            LordStaff.equipmentType = EquipmentType.Weapon;
            LordStaff.name_class = "item_class_weapon";
            AssetManager.items.add(LordStaff);
            LocalizationUtility.AddLocalization("item_LordStaff", "Lord Staff");
            addGunsSprite(LordStaff.id, LordStaff.materials[0]);

            ProjectileAsset RedStoneArrow = new ProjectileAsset();
            RedStoneArrow.id = "RedStoneArrow";
            RedStoneArrow.texture = "RedStoneArrow";
            RedStoneArrow.trailEffect_enabled = true;
            RedStoneArrow.trailEffect_id = "fx_red_trail_t";
            RedStoneArrow.texture_shadow = "shadow_arrow";
            RedStoneArrow.trailEffect_scale = 0.1f;
            RedStoneArrow.trailEffect_timer = 0.1f;
            RedStoneArrow.look_at_target = true;
            RedStoneArrow.terraformOption = "plasma_ball";
            RedStoneArrow.endEffect = "fx_fireball_explosion";
            RedStoneArrow.terraformRange = 2;
            RedStoneArrow.draw_light_area = true;
            RedStoneArrow.draw_light_size = 0.1f;
            RedStoneArrow.sound_launch = "event:/SFX/WEAPONS/WeaponPlasmaBallStart";
            RedStoneArrow.sound_impact = "event:/SFX/WEAPONS/WeaponPlasmaBallLand";
            RedStoneArrow.startScale = 0.035f;
            RedStoneArrow.targetScale = 0.2f;
            RedStoneArrow.parabolic = false;
            RedStoneArrow.speed = 20f;
            AssetManager.projectiles.add(RedStoneArrow);

            ProjectileAsset WindMagic = new ProjectileAsset();
            WindMagic.id = "WindMagic";
            WindMagic.texture = "fx_wind_trail_t";
            WindMagic.trailEffect_enabled = true;
            WindMagic.trailEffect_id = "fx_wind_trail_t";
            WindMagic.texture_shadow = "shadow_arrow";
            WindMagic.trailEffect_scale = 0.1f;
            WindMagic.trailEffect_timer = 0.1f;
            WindMagic.look_at_target = true;
            WindMagic.terraformRange = 2;
            WindMagic.draw_light_area = true;
            WindMagic.draw_light_size = 0.1f;
            WindMagic.sound_launch = "event:/SFX/WEAPONS/WeaponPlasmaBallStart";
            WindMagic.sound_impact = "event:/SFX/WEAPONS/WeaponPlasmaBallLand";
            WindMagic.startScale = 0.035f;
            WindMagic.targetScale = 0.2f;
            WindMagic.parabolic = false;
            WindMagic.speed = 20f;
            AssetManager.projectiles.add(WindMagic);


            ProjectileAsset blackMagic = new ProjectileAsset();
            blackMagic.id = "blackMagic";
            blackMagic.texture = "fx_dark_trail_t";
            blackMagic.trailEffect_enabled = true;
            blackMagic.trailEffect_id = "fx_dark_trail_t";
            blackMagic.texture_shadow = "shadow_arrow";
            blackMagic.trailEffect_scale = 0.1f;
            blackMagic.trailEffect_timer = 0.1f;
            blackMagic.look_at_target = true;
            blackMagic.terraformRange = 2;
            blackMagic.draw_light_area = true;
            blackMagic.draw_light_size = 0.1f;
            blackMagic.sound_launch = "event:/SFX/WEAPONS/WeaponPlasmaBallStart";
            blackMagic.sound_impact = "event:/SFX/WEAPONS/WeaponPlasmaBallLand";
            blackMagic.startScale = 0.035f;
            blackMagic.targetScale = 0.2f;
            blackMagic.parabolic = false;
            blackMagic.speed = 20f;
            AssetManager.projectiles.add(blackMagic);
        }


        public static bool Swordeffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetFieldValue<Actor>(pTarget, "a");
                if (Toolbox.randomChance(0.5f))
                {
                    ReflectionUtility.CallMethod(pTarget, "addStatusEffect", "burning", -1f);
                }
            }
            return false;
        }

        public static bool PC(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetFieldValue<Actor>(pTarget, "a");
            if (a.base_data.health < 800)
            {
                ItemAsset EStaff = AssetManager.items.get("EStaff");
                EStaff.projectile = "blackMagic";
            }
            if (a.base_data.health > 800)
            {
                ItemAsset EStaff = AssetManager.items.get("EStaff");
                EStaff.projectile = "WindMagic";
            }
            return false;
        }

        public static bool RedSAeffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetFieldValue<Actor>(pTarget, "a");
                if (Toolbox.randomChance(0.2f))
                {
                    ReflectionUtility.CallMethod(pTarget, "addStatusEffect", "stun", -1f);
                }
            }
            return false;
        }

        public static bool Mercy(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetFieldValue<Actor>(pTarget, "a");
                if (pTarget.base_data.health < 50)
                {
                    ActionLibrary.removeUnit(a);
                    ReflectionUtility.CallMethod(pTarget, "getHit", 10000000f);
                    Actor act = World.world.units.createNewUnit("frog", pTile);
                    ActorTool.copyUnitToOtherUnit(a, act);
                }
            }
            return false;
        }


        public static bool Forced(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetFieldValue<Actor>(pTarget, "a");
                MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", pTile);
                World.world.applyForce(pTile, 10, 1.5f, pForceOut: true, useOnNature: true);
                EffectsLibrary.spawnExplosionWave(pTile.posV3, 10f);
                return true;
            }
            return false;
        }

        public static void addGunsSprite(string id, string material)
        {
            var dictItems = ReflectionUtility.GetStaticFieldValue<Dictionary<string, Sprite>>(typeof(ActorAnimationLoader), "dictItems");
            var sprite = Resources.Load<Sprite>("Weapons/w_" + id + "_" + material);
            if (sprite != null && dictItems != null)
            {
                dictItems.Add(sprite.name, sprite);
                Debug.Log($"Added sprite with name: {sprite.name}");
            }
            else
            {
                Debug.LogError("Sprite or dictionary is null. Sprite: " + sprite + ", Dictionary: " + dictItems);
            }
        }
    }
}