using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Unit
{
    public static class ActorAssetUtility
    {

        public static void CopyActorAsset(ActorAsset source, ActorAsset destination)
        {
            if (source == null || destination == null)
            {
                Debug.LogError("Source or destination ActorAsset is null");
                return;
            }

            // Use the extension method to copy base stats
            destination.base_stats.CopyFrom(source.base_stats);

            // Copy other properties
            destination.split_ai_update = source.split_ai_update;
            destination.has_ai_system = source.has_ai_system;
            destination.fmod_idle_loop = source.fmod_idle_loop;
            destination.fmod_spawn = source.fmod_spawn;
            destination.fmod_idle = source.fmod_idle;
            destination.fmod_death = source.fmod_death;
            destination.fmod_attack = source.fmod_attack;
            destination.sound_hit = source.sound_hit;
            destination.fmod_theme = source.fmod_theme;
            destination.fmod_theme_civ = source.fmod_theme_civ;
            destination.texture_atlas = source.texture_atlas;
            destination.ignoredByInfinityCoin = source.ignoredByInfinityCoin;
            destination.newBeh = source.newBeh;
            destination.race = source.race;
            destination.nameLocale = source.nameLocale;
            destination.allowed_status_tiers = source.allowed_status_tiers;
            destination.render_status_effects = source.render_status_effects;
            destination.kingdom = source.kingdom;
            destination.actorSize = source.actorSize;
            destination.animation_walk = source.animation_walk;
            destination.animation_walk_speed = source.animation_walk_speed;
            destination.animation_swim = source.animation_swim;
            destination.animation_swim_speed = source.animation_swim_speed;
            destination.animation_idle_speed = source.animation_idle_speed;
            destination.animation_idle = source.animation_idle;
            destination.max_shake_timer = source.max_shake_timer;
            destination.texture_heads = source.texture_heads;
            destination.base_stats = new BaseStats();
            destination.tech = source.tech;
            destination.defaultAttack = source.defaultAttack;
            destination.immune_to_tumor = source.immune_to_tumor;
            destination.immune_to_slowness = source.immune_to_slowness;
            destination.aggression = source.aggression;
            destination.shadow = source.shadow;
            destination.shadowTexture = source.shadowTexture;
            destination.shadow_sprite = source.shadow_sprite;
            destination.hit_fx_alternative_offset = source.hit_fx_alternative_offset;
            destination.canLevelUp = source.canLevelUp;
            destination.nameTemplate = source.nameTemplate;
            destination.years_to_grow_to_adult = source.years_to_grow_to_adult;
            destination.use_items = source.use_items;
            destination.take_items = source.take_items;
            destination.take_items_ignore_range_weapons = source.take_items_ignore_range_weapons;
            destination.useSkinColors = source.useSkinColors;
            destination.color_sets = source.color_sets != null ? new List<string>(source.color_sets) : null;
            destination.canBeKilledByStuff = source.canBeKilledByStuff;
            destination.canBeKilledByLifeEraser = source.canBeKilledByLifeEraser;
            destination.canBeKilledByDivineLight = source.canBeKilledByDivineLight;
            destination.canBeCitizen = source.canBeCitizen;
            destination.ignoreTileSpeedMod = source.ignoreTileSpeedMod;
            destination.skipFightLogic = source.skipFightLogic;
            destination.canAttackBuildings = source.canAttackBuildings;
            destination.canAttackBrains = source.canAttackBrains;
            destination.ignoreJobs = source.ignoreJobs;
            destination.countAsUnit = source.countAsUnit;
            destination.egg = source.egg;
            destination.flag_tornado = source.flag_tornado;
            destination.flag_ufo = source.flag_ufo;
            destination.flag_finger = source.flag_finger;
            destination.flag_turtle = source.flag_turtle;
            destination.animal = source.animal;
            destination.unit = source.unit;
            destination.baby = source.baby;
            destination.texture_path = source.texture_path;
            destination.body_separate_part_head = source.body_separate_part_head;
            destination.body_separate_part_hands = source.body_separate_part_hands;
            destination.hovering_min = source.hovering_min;
            destination.hovering_max = source.hovering_max;
            destination.hovering = source.hovering;
            destination.flying = source.flying;
            destination.very_high_flyer = source.very_high_flyer;
            destination.disableJumpAnimation = source.disableJumpAnimation;
            destination.rotatingAnimation = source.rotatingAnimation;
            destination.dieOnBlocks = source.dieOnBlocks;
            destination.ignoreBlocks = source.ignoreBlocks;
            destination.moveFromBlock = source.moveFromBlock;
            destination.run_to_water_when_on_fire = source.run_to_water_when_on_fire;
            destination.dieOnGround = source.dieOnGround;
            destination.damagedByOcean = source.damagedByOcean;
            destination.damagedByRain = source.damagedByRain;
            destination.water_damage_value = source.water_damage_value;
            destination.cancelBehOnLand = source.cancelBehOnLand;
            destination.oceanCreature = source.oceanCreature;
            destination.landCreature = source.landCreature;
            destination.swampCreature = source.swampCreature;
            destination.isBoat = source.isBoat;
            destination.drawBoatMark = source.drawBoatMark;
            destination.drawBoatMark_big = source.drawBoatMark_big;
            destination.speedModLiquid = source.speedModLiquid;
            destination.procreate = source.procreate;
            destination.procreate_age = source.procreate_age;
            destination.animal_baby_making_around_limit = source.animal_baby_making_around_limit;
            destination.layEggs = source.layEggs;
            destination.eggStatsID = source.eggStatsID;
            destination.prefab = source.prefab;
            destination.dieInLava = source.dieInLava;
            destination.canBeMovedByPowers = source.canBeMovedByPowers;
            destination.canBeHurtByPowers = source.canBeHurtByPowers;
            destination.canTurnIntoIceOne = source.canTurnIntoIceOne;
            destination.canTurnIntoZombie = source.canTurnIntoZombie;
            destination.canTurnIntoMush = source.canTurnIntoMush;
            destination.canTurnIntoTumorMonster = source.canTurnIntoTumorMonster;
            destination.has_soul = source.has_soul;
            destination.canReceiveTraits = source.canReceiveTraits;
            destination.zombieID = source.zombieID;
            destination.skeletonID = source.skeletonID;
            destination.mushID = source.mushID;
            destination.tumorMonsterID = source.tumorMonsterID;
            destination.can_turn_into_demon_in_age_of_chaos = source.can_turn_into_demon_in_age_of_chaos;
            destination.showIconInspectWindow = source.showIconInspectWindow;
            destination.showIconInspectWindow_id = source.showIconInspectWindow_id;
            destination.hideFavoriteIcon = source.hideFavoriteIcon;
            destination.canBeFavorited = source.canBeFavorited;
            destination.canBeInspected = source.canBeInspected;
            destination.inspectAvatarScale = source.inspectAvatarScale;
            destination.inspectAvatar_offset_x = source.inspectAvatar_offset_x;
            destination.inspectAvatar_offset_y = source.inspectAvatar_offset_y;
            destination.maxHunger = source.maxHunger;
            destination.can_edit_traits = source.can_edit_traits;
            destination.needFood = source.needFood;
            destination.finish_scale_on_creation = source.finish_scale_on_creation;
            destination.path_movement_timeout = source.path_movement_timeout;
            destination.diet_berries = source.diet_berries;
            destination.diet_crops = source.diet_crops;
            destination.diet_flowers = source.diet_flowers;
            destination.diet_grass = source.diet_grass;
            destination.diet_vegetation = source.diet_vegetation;
            destination.diet_meat = source.diet_meat;
            destination.diet_meat_insect = source.diet_meat_insect;
            destination.diet_meat_same_race = source.diet_meat_same_race;
            destination.source_meat = source.source_meat;
            destination.source_meat_insect = source.source_meat_insect;
            destination.defaultZ = source.defaultZ;
            destination.updateZ = source.updateZ;
            destination.hideOnMinimap = source.hideOnMinimap;
            destination.inspect_stats = source.inspect_stats;
            destination.inspect_children = source.inspect_children;
            destination.inspect_kills = source.inspect_kills;
            destination.inspect_experience = source.inspect_experience;
            destination.inspect_home = source.inspect_home;
            destination.immune_to_injuries = source.immune_to_injuries;
            destination.job = source.job;
            destination.effect_cast_top = source.effect_cast_top;
            destination.effect_cast_ground = source.effect_cast_ground;
            destination.effect_teleport = source.effect_teleport;
            destination.attack_spells = new List<string>(source.attack_spells);
            destination.heads = source.heads;
            destination.effectDamage = source.effectDamage;
            destination.specialAnimation = source.specialAnimation;
            destination.canFlip = source.canFlip;
            destination.specialDeadAnimation = source.specialDeadAnimation;
            destination.deathAnimationAngle = source.deathAnimationAngle;
            destination.status_tiers = source.status_tiers;
            destination.has_sprite_renderer = source.has_sprite_renderer;
            destination.dieByLightning = source.dieByLightning;
            destination.has_skin = source.has_skin;
            destination.growIntoID = source.growIntoID;
            destination.icon = source.icon;
            destination.skipSave = source.skipSave;
            destination.color = source.color;
            destination.cost = source.cost;
            destination.traits = new List<string>(source.traits);
            destination.defaultWeapons = new List<string>(source.defaultWeapons);
            destination.defaultWeaponsMaterial = new List<string>(source.defaultWeaponsMaterial);
            destination.flags = new List<string>(source.flags);
            destination.flags_dict = source.flags_dict != null ? new Dictionary<string, bool>(source.flags_dict) : null;
            destination.disablePunchAttackAnimation = source.disablePunchAttackAnimation;
            destination.get_override_sprite = source.get_override_sprite;
            destination.has_override_sprite = source.has_override_sprite;
            destination.maxRandomAmount = source.maxRandomAmount;
            destination.currentAmount = source.currentAmount;
            destination.action_click = source.action_click;
            destination.action_death = source.action_death;
            destination.action_dead_animation = source.action_dead_animation;
            destination.action_get_hit = source.action_get_hit;
            destination.action_liquid = source.action_liquid;
            destination.check_flip = source.check_flip;
            destination.action_recalc_stats = source.action_recalc_stats;
        }


        public static void ModifyActorAsset(ActorAsset asset, string propertyName, object value)
        {
            var property = asset.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var field = asset.GetType().GetField(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (property != null && property.CanWrite)
            {
                property.SetValue(asset, Convert.ChangeType(value, property.PropertyType), null);
            }
            else if (field != null)
            {
                field.SetValue(asset, Convert.ChangeType(value, field.FieldType));
            }
            else
            {
                Debug.LogError($"Property or field '{propertyName}' not found on ActorAsset.");
            }
        }

        public static string GetAnimationWalk(ActorAsset asset)
        {
            return asset.animation_walk;
        }

        public static void SetAnimationWalk(ActorAsset asset, string animationWalk)
        {
            asset.animation_walk = animationWalk;
        }

        public static string GetAnimationSwim(ActorAsset asset)
        {
            return asset.animation_swim;
        }

        public static ActorAsset DeepCopyActorAsset(ActorAsset original)
        {
            ActorAsset copy = new ActorAsset();
            CopyActorAsset(original, copy);
            return copy;
        }
    }
}