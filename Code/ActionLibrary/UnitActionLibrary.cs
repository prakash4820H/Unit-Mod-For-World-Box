using ai;
using ReflectionUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Unit
{

    public class UnitActionLibrary
    {
        // Random Powers

        private Actor actor;
        private ActorAsset originalAsset;
        private ActorAsset modifiedAsset;


        public static bool Powers(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
                if (a != null)
                {
                    if (Toolbox.randomChance(0.5f))
                    {
                        ActionLibrary.addPoisonedEffectOnTarget(null, pTarget, null);
                    }
                    if (Toolbox.randomChance(0.03f))
                    {
                        pTarget.CallMethod("addStatusEffect", "slowness", -1f);
                    }
                    if (Toolbox.randomChance(0.01f))
                    {
                        pTarget.CallMethod("addStatusEffect", "frozen", 15f);
                    }
                    if (Toolbox.randomChance(0.02f))
                    {
                        pTarget.CallMethod("addStatusEffect", "stun", 15f);
                    }
                }
            }
            return false;
        }

        public static void removeTraitRain(WorldTile pTile, List<string> pList)
        {
            if (pList.Count == 0)
            {
                return;
            }

            int num = 0;
            while (num < pList.Count)
            {
                string pID = pList[num];
                ActorTrait actorTrait = AssetManager.traits.get(pID);
                if (actorTrait == null || !actorTrait.can_be_removed)
                {
                    pList.RemoveAt(num);
                }
                else
                {
                    num++;
                }
            }
            World.world.getObjectsInChunks(pTile, 3, MapObjectType.Actor);
            for (int i = 0; i < World.world.temp_map_objects.Count; i++)
            {
                Actor actor = World.world.temp_map_objects[i].a;
                if (!actor.asset.can_edit_traits)
                {
                    continue;
                }
                foreach (string traitID in pList)
                {
                    if (actor.hasTrait(traitID))
                    {
                        actor.removeTrait(traitID);
                    }
                }
                if (actor.hasTrait("scar_of_divinity"))
                {
                    actor.removeTrait("scar_of_divinity");
                }
            }
        }


        // Gives unit a VIP pass to void 
        public static bool remove_unit(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (a != null)
            {
                ActionLibrary.removeUnit(a);
                return true;
            }
            return false;
        }
        public static bool Spike_end3(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (a != null)
            {
                // Check if the actor's asset ID is already "unit_human"
                if (a.asset.id == "unit_human")
                {
                    return false; // If it is, do not run the rest of the code
                }

                // Load the new asset and clear the current sprites
                a.loadAsset(AssetManager.actor_library.get("wolf"));
                a.clearSprites();
                a.updateStats();
                a.resetTransformName();

                // Ensure the spriteRenderer is properly set up
                a.spriteRenderer = a.gameObject.GetComponent<SpriteRenderer>();
                if (a.spriteRenderer == null)
                {
                    a.spriteRenderer = a.gameObject.AddComponent<SpriteRenderer>();
                }

                // Update the sprite
                a.checkSpriteToRender();

                return true;
            }
            return false;
        }

        public static bool Spike_end2(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (a != null)
            {
                a.loadAsset(AssetManager.actor_library.get("unit_human"));
                a.setHeadSprite(null);
                a.clearSprites();
                a.updateStats();
                a.cancelAllBeh(null);
                a.resetTransformName();
                return true;
            }
            return false;
        }

        public static bool idk(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (a != null)
            {
                BaseEffect baseEffect = EffectsLibrary.spawnAt("fx_shield_hit", pTarget.currentPosition, 1f);
                return true;
            }
            return false;
        }

        public static bool zelkovaTouchEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (!Toolbox.randomChance(0.3f))
            {
                return false;
            }
            UnitActionLibrary.checkZelkovaTerraform(pTarget.a.currentTile);
            return true;
        }

        // In the MapAction or equivalent class
        public static void checkZelkovaTerraform(WorldTile pTile)
        {
            if (pTile.isTemporaryFrozen())
            {
                pTile.unfreeze(99);
            }
            if (pTile.top_type != null && pTile.top_type.wasteland)
            {
                return;
            }
            if (pTile.top_type != null)
            {
                MapAction.decreaseTile(pTile, "flash");
                return;
            }
            if (pTile.Type.ground)
            {
                if (pTile.isTileRank(TileRank.Low))
                {
                    MapAction.terraformTop(pTile, AssetManager.topTiles.get("zelkova_low"));
                }
                else if (pTile.isTileRank(TileRank.High))
                {
                    MapAction.terraformTop(pTile, AssetManager.topTiles.get("zelkova_high"));
                }
                AchievementLibrary.achievementLetsNot.check(null, null, null);
            }
        }


        public static bool Spike_end1(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (a != null)
            {
                a.addTrait("fire_proof");

                // Create a deep copy of the ActorAsset for this specific actor
                ActorAsset originalAsset = a.asset;
                ActorAsset copiedAsset = ActorAssetUtility.DeepCopyActorAsset(originalAsset);
                a.asset = copiedAsset;

                a.asset.base_stats.CopyFrom(originalAsset.base_stats);
                ActorAssetUtility.ModifyActorAsset(a.asset, "canBeKilledByStuff", false);
                ActorAssetUtility.ModifyActorAsset(a.asset, "canBeKilledByLifeEraser", false);
                a.updateStats();

                return true;
            }
            return false;
        }

        // Finishes all status effects
        public static bool FinishAllEffects(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (a != null)
            {
                a.finishAllStatusEffects();
                a.removeTrait("scar_of_divinity");
                return true;
            }
            return false;
        }

        public static bool Items(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
                if (Toolbox.randomChance(0.5f))
                {
                    a.takeItems(a, a.asset.take_items_ignore_range_weapons);
                }
            }
            return false;
        }

        // Sets units's job to warrior
        public static bool WarriorJob(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (a.city != null)
            {
                a.setProfession(UnitProfession.Warrior, true);
                if (a.equipment != null && a.equipment.weapon.isEmpty())
                {
                    City.giveItem(a, a.city.getEquipmentList(EquipmentType.Amulet), a.city);
                    City.giveItem(a, a.city.getEquipmentList(EquipmentType.Weapon), a.city);
                    City.giveItem(a, a.city.getEquipmentList(EquipmentType.Ring), a.city);
                    City.giveItem(a, a.city.getEquipmentList(EquipmentType.Helmet), a.city);
                    City.giveItem(a, a.city.getEquipmentList(EquipmentType.Armor), a.city);
                    City.giveItem(a, a.city.getEquipmentList(EquipmentType.Boots), a.city);
                }
            }
            a.removeTrait("Warrior Job");
            a.removeTrait("scar_of_divinity");
            return true;
        }
        public static bool FreezeTile(BaseSimObject pTarget, WorldTile pTile = null)
        {
            World.world.loopWithBrush(pTarget.currentTile, Brush.get(25, "circ_"), new PowerActionWithID(PowerLibrary.drawTemperatureMinus), "coldAura");
            return true;
        }

        public static bool Test11(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
                if (Toolbox.randomChance(0.5f))
                {
                    ActionLibrary.addPoisonedEffectOnTarget(null, pTarget, null);
                }
                if (Toolbox.randomChance(0.5f))
                {
                    pTarget.CallMethod("addStatusEffect", "stun", 15f);
                }
            }
            return false;
        }

        // Lightning sus
        public static void spawnLightningB(WorldTile pTile, float pScale = 0.25f)
        {
            BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_small", pTile, pScale);
            if (!(baseEffect == null))
            {
                int pRad = (int)(pScale * 100f);
                MapAction.damageWorld(pTile, pRad, AssetManager.terraform.get("lightning_normal"));
                baseEffect.sprRenderer.flipX = Toolbox.randomBool();
                MapAction.checkLightningAction(pTile.pos, pRad);
            }
        }

        public static bool Lightning(BaseSimObject pTarget, WorldTile pTile = null)
        {
            UnitActionLibrary.spawnLightningB(pTarget.currentTile);
            return true;
        }



        // Teleporting method
        public static bool Teleport(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (Toolbox.randomChance(0.01f))
            {
                ActionLibrary.teleportRandom(null, pTarget, null);
            }
            return false;
        }

        // Grand mage special attack
        public static bool GrandMageAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
                if (Toolbox.randomChance(0.5f))
                {
                    // ActionLibrary.castCurses(null, pTarget, null);
                    ActionLibrary.addPoisonedEffectOnTarget(null, pTarget, null);
                }
                if (Toolbox.randomChance(0.03f))
                {
                    pTarget.CallMethod("addStatusEffect", "burning", 15f);
                }
                if (Toolbox.randomChance(0.03f))
                {
                    pTarget.CallMethod("addStatusEffect", "stun", 15f);
                }
            }
            return false;
        }

        // Adds a status effects that opposes all other effects
        public static bool Immunity(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (Toolbox.randomChance(100f))
            {
                // pTarget.CallMethod("addStatusEffect", "invincible", 400f);
                pTarget.CallMethod("addStatusEffect", "Chillin", 400f);
                //    pTarget.CallMethod("addStatusEffect", "Torchwood2", 3f);
            }
            return false;
        }

        public static bool Aura(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (Toolbox.randomChance(100f))
            {
                // pTarget.CallMethod("addStatusEffect", "invincible", 400f);
                pTarget.CallMethod("addStatusEffect", "FE", 400f);
                //                   BaseEffect baseEffect = EffectsLibrary.spawnAtTile("fx_lightning_small", pTile, pScale);
            }
            return false;
        }

        // Spawns Meteor
        public static bool Meteore(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
            if (Toolbox.randomChance(100.0f))
            {
                EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f);
            }
            return false;
        }

        // Invisible unit's opponent behaviour
        public static bool Weird(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
                if (pSelf.hasStatus("Invisible"))
                {
                    pTarget.CallMethod("makeWait", 1f);
                    pTarget.CallMethod("ignoreTarget", pSelf);
                }
            }
            return true;
        }

        // Adds Temporary Rage effect
        public static bool Stemp(BaseSimObject pSelf, BaseSimObject pAttackedBy = null, WorldTile pTile = null)
        {
            if (Toolbox.randomChance(0.3f))
            {
                pSelf.CallMethod("addStatusEffect", "tempRage", 15f);
            }
            return false;
        }

        public static bool FS(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
        {
            if (pAttackedBy != null)
            {
                Actor enemy = ReflectionUtility.GetField<Actor>(pAttackedBy, "a");
                Actor defender = ReflectionUtility.GetField<Actor>(pSelf, "a");

                // Check if the enemy is below level 3
                if (enemy.hasStatus("invincible"))
                {
                    enemy.finishStatusEffect("invincible");
                    enemy.addStatusEffect("stun");
                }
            }
            return false;
        }


        // Revival Effect
        public static bool LastChance(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                Actor a = ReflectionUtility.GetField<Actor>(pTarget, "a");
                var act = World.world.units.createNewUnit(a.asset.id, pTile);
                a.removeTrait("Revive");
                act.data.head = a.data.head;
                act.data.gender = a.data.gender;
                act.data.skin_set = a.data.skin_set;
                act.data.skin = a.data.skin;
                act.data.clan = a.data.clan;
                ActorTool.copyUnitToOtherUnit(a, act);
                act.removeTrait("Revive");
                act.kingdom = pTarget.kingdom;
            }
            return true;
        }

        public static void action_cloning_rain(WorldTile pTile, string pDropID = null)
        {
            if (pTile == null || !pTile.hasUnits()) return;
            List<Actor> actorsOnTile = new List<Actor>();
            pTile.doUnits((Actor actor) =>
            {
                if (actor.isAlive())
                {
                    actorsOnTile.Add(actor);
                }
            });
            if (actorsOnTile.Count == 0) return;
            Actor selectedActor = actorsOnTile[UnityEngine.Random.Range(0, actorsOnTile.Count)];
            WorldTile targetTile = FindEmptyAdjacentTile(pTile);
            if (targetTile == null)
            {
                Debug.Log("No empty tile found nearby to place the clone.");
                return;
            }
            Actor clonedActor = CloneActor(selectedActor, targetTile);
            SpawnEffectAtTile("fx_spawn_big_0", targetTile);
            PlaySpawnSound(clonedActor);
            Debug.Log($"{selectedActor.getName()} was cloned and placed at ({targetTile.pos.x}, {targetTile.pos.y}).");
        }

        private static Actor CloneActor(Actor originalActor, WorldTile targetTile)
        {
            Actor clonedActor = World.world.units.createNewUnit(originalActor.asset.id, targetTile);
            EffectsLibrary.spawn("fx_spawn", clonedActor.currentTile, null, null, 0f, -1f, -1f);
            if (clonedActor != null)
            {
                clonedActor.data.gender = originalActor.data.gender;
                clonedActor.data.skin = originalActor.data.skin;
                clonedActor.data.head = originalActor.data.head;
                clonedActor.data.skin_set = originalActor.data.skin_set;
                clonedActor.data.profession = originalActor.data.profession;
                clonedActor.data.created_time = originalActor.data.created_time;
                clonedActor.data.clan = originalActor.data.clan;
                clonedActor.data.traits = new List<string>(originalActor.data.traits);
                clonedActor.updateStats();
            }
            return clonedActor;
        }

        private static WorldTile FindEmptyAdjacentTile(WorldTile originTile)
        {
            WorldTile[] neighbours = originTile.neighboursAll;

            foreach (WorldTile tile in neighbours)
            {
                if (!tile.hasUnits())
                {
                    return tile;
                }
            }

            return null;
        }

        private static void SpawnEffectAtTile(string effectID, WorldTile targetTile)
        {
            BaseEffect effect = EffectsLibrary.spawnAt(effectID, targetTile.posV3, 1f);
            if (effect != null)
            {
                effect.prepare(targetTile.posV3, 1f);
            }
        }

        private static void PlaySpawnSound(Actor clonedActor)
        {
            if (!string.IsNullOrEmpty(clonedActor.asset.fmod_spawn))
            {
                MusicBox.playSound(clonedActor.asset.fmod_spawn, clonedActor.currentTile.pos.x, clonedActor.currentTile.pos.y);
            }
        }

        public static bool action_convert_city_to_another_kingdom(WorldTile pTile, string powerID)
        {
            City selectedCityA = pTile.zone.city;
            if (selectedCityA == null)
            {
                WorldTip.showNow("No city selected.", true, "top", 3f);
                return false;
            }

            // Notify the player that the first city has been selected
            WorldTip.showNow("Select the second city.", true, "top", 3f);

            City selectedCityB = Config.selectedCity;  // Second city to be selected
            if (selectedCityB == null || selectedCityA == selectedCityB)
            {
                WorldTip.showNow("Invalid city selection. Try again.", true, "top", 3f);
                return false;
            }

            Kingdom oldKingdom = selectedCityA.kingdom;
            Kingdom newKingdom = selectedCityB.kingdom;

            if (oldKingdom == newKingdom)
            {
                WorldTip.showNow($"{selectedCityA.getCityName()} is already part of {newKingdom.name}!", true, "top", 3f);
                return false;
            }

            // Convert the first city to the kingdom of the second city
            selectedCityA.joinAnotherKingdom(newKingdom);
            WorldTip.showNow($"{selectedCityA.getCityName()} has joined the kingdom of {newKingdom.name}!", true, "top", 3f);

            return true;
        }

        public static void action_clear_kingdom_resources(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            City city = pTile.zone.city;
            Kingdom kingdom = city.kingdom;

            if (kingdom == null)
            {
                // Notify that there is no kingdom associated
                LocalizationUtility.AddOrSet("no_kingdom", "This city does not belong to a kingdom!");
                WorldTip.showNow("no_kingdom", true, "top", 3f);
                return;
            }

            // Iterate through all cities in the kingdom and clear their storage
            foreach (City kingdomCity in kingdom.cities)
            {
                // Access the storage from CityData
                CityStorage cityStorage = kingdomCity.data.storage;

                // Clear all resources
                foreach (var resource in cityStorage.resources)
                {
                    resource.Value.amount = 0;  // Set the amount of each resource to 0
                }

                // Clear food resources
                foreach (var food in cityStorage.resourcesFood)
                {
                    food.Value.amount = 0;
                }

                // Clear weapons and other items
                cityStorage.items_weapons.Clear();
                cityStorage.items_armor.Clear();
                cityStorage.items_helmets.Clear();
                cityStorage.items_boots.Clear();
                cityStorage.items_rings.Clear();
                cityStorage.items_amulets.Clear();

                // Notify the player that resources have been cleared
                LocalizationUtility.AddOrSet("resources_cleared", $"Resources cleared for city: {kingdomCity.getCityName()}");
                WorldTip.showNow("resources_cleared", true, "top", 3f);
            }

            // Notify the player that all resources in the kingdom have been cleared
            LocalizationUtility.AddOrSet("kingdom_resources_cleared", $"All resources in the kingdom of {kingdom.name} have been cleared!");
            WorldTip.showNow("kingdom_resources_cleared", true, "top", 3f);
        }

        public static void action_make_capital(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            City city = pTile.zone.city;
            Kingdom kingdom = city.kingdom;

            // Check if the city is already the capital
            if (kingdom.capital == city)
            {
                // Notify that the city is already the capital
                LocalizationUtility.AddOrSet("already_capital", $"{city.getCityName()} is already the capital of {kingdom.name}!");
                WorldTip.showNow("already_capital", true, "top", 3f);
                return;
            }

            // Set the city as the new capital
            kingdom.capital = city;
            kingdom.data.capitalID = city.data.id;  // Update kingdom data to reflect the new capital
            kingdom.location = city.cityCenter;     // Update kingdom's main location to the new capital

            // Notify the player of the capital change
            LocalizationUtility.AddOrSet("new_capital_assigned", $"{city.getCityName()} has been made the capital of {kingdom.name}!");
            WorldTip.showNow("new_capital_assigned", true, "top", 3f);
        }

        public static void assignRandomAssetToActor(WorldTile pTile, string pDropID = null)
        {
            // Ensure the tile is valid
            if (pTile == null) return;

            // List of all actors in the world
            List<Actor> allActors = World.world.units.getSimpleList();

            // Find all actors within a 3-tile radius
            List<Actor> actorsInRange = allActors
                .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= 3f)
                .ToList();

            foreach (var selectedActor in actorsInRange)
            {
                if (selectedActor == null || selectedActor.asset == null) continue;

                // Define the available unit asset IDs
                Dictionary<string, List<string>> assetGroups = new Dictionary<string, List<string>>()
        {
            { "unit_human", new List<string> { "unit_orc", "unit_elf", "unit_dwarf" } },
            { "unit_orc", new List<string> { "unit_human", "unit_elf", "unit_dwarf" } },
            { "unit_elf", new List<string> { "unit_human", "unit_orc", "unit_dwarf" } },
            { "unit_dwarf", new List<string> { "unit_human", "unit_orc", "unit_elf" } }
        };

                // Check if the actor's asset belongs to one of the defined groups
                if (!assetGroups.ContainsKey(selectedActor.asset.id)) continue;

                // Get the list of possible new assets, excluding the actor's current asset
                List<string> possibleAssets = assetGroups[selectedActor.asset.id];

                // Ensure the current asset is removed from the list
                possibleAssets.Remove(selectedActor.asset.id);

                // Randomly select a new asset from the remaining options
                if (possibleAssets.Count == 0) continue;
                string randomAssetID = possibleAssets[UnityEngine.Random.Range(0, possibleAssets.Count)];

                // Retrieve the new ActorAsset object using the randomly selected asset ID
                ActorAsset newAsset = AssetManager.actor_library.get(randomAssetID);
                if (newAsset == null) continue;

                // Load the new asset into the actor
                selectedActor.loadAsset(newAsset);

                // Set the corresponding race based on the asset traits
                if (!string.IsNullOrEmpty(newAsset.race))
                {
                    Race newRace = AssetManager.raceLibrary.get(newAsset.race);
                    if (newRace != null)
                    {
                        selectedActor.race = newRace; // Correctly set the new race via asset
                        selectedActor.data.generateTraits(newAsset, newRace); // Regenerate the traits
                    }
                }

                // Clear actor's sprites and update stats
                selectedActor.clearSprites();
                selectedActor.updateStats();
                selectedActor.checkHeadID();

                // Optional: Log the change for debugging purposes
                //   Debug.Log($"{selectedActor.getName()} was reassigned from {selectedActor.asset.id} to {newAsset.id}, Race: {selectedActor.race?.id}");
            }
        }

        public static void action_make_army_leader(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            City city = pTile.zone.city;
            List<Actor> allActors = World.world.units.getSimpleList();
            List<Actor> actorsInRange = allActors
                .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= 3f)
                .ToList();

            foreach (var pActor in actorsInRange)
            {
                if (!pActor.asset.unit || pActor.city != city) continue;

                UnitGroup cityArmy = city.army;
                if (cityArmy == null)
                {
                    cityArmy = MapBox.instance.unitGroupManager.createNewGroup(city);
                    city.army = cityArmy;
                }
                if (pActor.data.profession != UnitProfession.Warrior)
                {
                    // Convert actor to a soldier
                    pActor.data.profession = UnitProfession.Warrior;
                    city.addNewUnit(pActor, pSetKingdom: false);
                }
                if (cityArmy.groupLeader != null)
                {
                    cityArmy.groupLeader.setGroupLeader(false);
                }
                cityArmy.setGroupLeader(pActor);
                pActor.setGroupLeader(true);
                LocalizationUtility.AddOrSet("army_leader_assigned", $"{pActor.getName()} is now the army leader of {city.getCityName()}!");
                WorldTip.showNow("army_leader_assigned", true, "top", 3f);
                break;
            }
        }

        public static void action_make_CityLeader(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            City city = pTile.zone.city;
            if (city.leader != null)
            {
                city.leader.setProfession(UnitProfession.Unit);
                city.leader = null;
            }
            List<Actor> allActors = World.world.units.getSimpleList();
            List<Actor> actorsInRange = allActors
                .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= 3f)
                .ToList();
            foreach (var pActor in actorsInRange)
            {
                if (!pActor.asset.unit || pActor.city != city) continue;
                City.makeLeader(pActor, city);
                LocalizationUtility.AddOrSet("leader_assigned", $"{pActor.getName()} is now the leader of the city {city.getCityName()}!");
                WorldTip.showNow("leader_assigned", true, "top", 3f);
                break;
            }
        }

        public static void action_make_clan_leader(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            List<Actor> allActors = World.world.units.getSimpleList();
            List<Actor> actorsInRange = allActors
                .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= 3f)
                .ToList();

            foreach (var pActor in actorsInRange)
            {
                if (!pActor.asset.unit) continue;

                Clan existingClan = pActor.getClan();
                if (existingClan != null)
                {
                    if (existingClan.data.chief_id != pActor.data.id)
                    {
                        existingClan.data.chief_id = pActor.data.id;
                        LocalizationUtility.AddOrSet("clan_leader_assigned", $"{pActor.getName()} is now the leader of the clan {existingClan.data.name}!");
                        WorldTip.showNow("clan_leader_assigned", true, "top", 3f);
                    }
                    else
                    {
                        // Actor is already the leader, no further action needed
                        LocalizationUtility.AddOrSet("already_clan_leader", $"{pActor.getName()} is already the leader of the clan {existingClan.data.name}.");
                        WorldTip.showNow("already_clan_leader", true, "top", 3f);
                    }
                }
                else
                {
                    // Create a new clan for the actor
                    Clan newClan = World.world.clans.newClan(pActor);
                    newClan.data.chief_id = pActor.data.id;
                    newClan.generateBanner(pReset: true);
                    newClan.units.Add(pActor.data.id, pActor);

                    pActor.data.clan = newClan.data.id;

                    LocalizationUtility.AddOrSet("new_clan_created", $"{pActor.getName()} has created and become the leader of a new clan!");
                    WorldTip.showNow("new_clan_created", true, "top", 3f);
                }

                BannerLoaderClans bannerLoader = pActor.GetComponent<BannerLoaderClans>();
                if (bannerLoader != null)
                {
                    bannerLoader.load(pActor.getClan());
                }
                else
                {
                    // Log an error if the banner loader component is missing
                    //          Debug.LogError($"BannerLoaderClans component not found on {pActor.getName()}");
                }

                break;
            }
        }

        public static void action_make_Peasant(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            City newCity = pTile.zone.city;
            List<Actor> allActors = World.world.units.getSimpleList();
            List<Actor> actorsInRange = allActors
                .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= 3f)
                .ToList();

            foreach (var pActor in actorsInRange)
            {
                if (!pActor.asset.unit || pActor.city != newCity) continue;
                if (pActor.isProfession(UnitProfession.King))
                {
                    pActor.setProfession(UnitProfession.Unit);
                    newCity.kingdom.removeKing();
                }
                else
                {
                    pActor.setProfession(UnitProfession.Unit);
                }
                LocalizationUtility.AddOrSet("civillian_assigned", $"{pActor.getName()} is now a civilian in {newCity.getCityName()}.");
                WorldTip.showNow("civillian_assigned", true, "top", 3f);
            }
        }

        public static void action_make_king(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            City newCity = pTile.zone.city;
            Kingdom kingdom = newCity.kingdom;
            if (kingdom == null) return;
            List<Actor> allActors = World.world.units.getSimpleList();
            List<Actor> actorsInRange = allActors
                .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= 3f)
                .ToList();
            if (kingdom.hasKing())
            {
                kingdom.removeKing();
            }
            foreach (var pActor in actorsInRange)
            {
                if (!pActor.asset.unit || pActor.city != newCity) continue;
                kingdom.setKing(pActor);
                LocalizationUtility.AddOrSet("king_appointed", $"{pActor.getName()} has been crowned as the King of {kingdom.name}!");
                WorldTip.showNow("king_appointed", true, "top", 3f);
                WorldLog.logNewKing(kingdom);
                break;
            }
        }

        public static void action_force_join(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null || pTile.zone == null || pTile.zone.city == null) return;

            City newCity = pTile.zone.city;
            float radius = 3.0f;
            List<Actor> allActors = World.world.units.getSimpleList();
            List<Actor> actorsInRange = allActors
                .Where(actor => Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= radius)
                .Where(actor => actor.asset.unit == true) // Check if the actor's asset has unit = true
                .ToList();

            foreach (var pActor in actorsInRange)
            {
                try
                {
                    LocalizationUtility.AddOrSet("force_citizen_success", $"{pActor.getName()} has joined the city {newCity.getCityName()}!");
                    pActor.becomeCitizen(newCity);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to convert {pActor.getName()} to citizen: {ex.Message}");
                }
            }

            WorldTip.showNow($"{actorsInRange.Count} citizens have joined the city {newCity.getCityName()}!", true, "top", 3f);
        }

        public static void action_warrior(WorldTile pTile = null, string pDropID = null)
        {
            if (pTile == null) return;
            List<Actor> allActors = World.world.units.getSimpleList();
            float radius = 3.0f;
            List<Actor> actorsInRange = new List<Actor>();
            foreach (Actor actor in allActors)
            {
                if (Toolbox.DistVec2(actor.currentTile.pos, pTile.pos) <= radius)
                {
                    actorsInRange.Add(actor);
                }
            }
            List<Actor> updatedActors = new List<Actor>();

            foreach (var pActor in actorsInRange)
            {
                if (!IsValidWarriorCandidate(pActor)) continue;
                MakeWarrior(pActor);
                updatedActors.Add(pActor);
            }
            foreach (var pActor in updatedActors)
            {
                pActor.setStatsDirty();
                pActor.startShake(0.3f, 0.1f, true, true);
                pActor.startColorEffect(ActorColorEffect.White);
            }
        }

        private static bool IsValidWarriorCandidate(Actor pActor)
        {
            return pActor != null && pActor.asset.unit && pActor.city != null;
        }

        private static void MakeWarrior(Actor pActor)
        {
            if (pActor.asset.baby)
            {
                pActor.removeTrait("peaceful");
            }

            pActor.CallMethod("setProfession", UnitProfession.Warrior, true);
            var pAI = (AiSystemActor)Reflection.GetField(typeof(Actor), pActor, "ai");

            if (pActor.equipment?.weapon?.isEmpty() ?? true)
            {
                City.giveItem(pActor, pActor.city.getEquipmentList(EquipmentType.Weapon), pActor.city);
            }

            if (pActor.city.getArmy() == 0 && pActor.city.army == null)
            {
                UnitGroup army = MapBox.instance.unitGroupManager.createNewGroup(pActor.city);
                pActor.city.army = army;
            }

            pActor.city.status.warriors_current++;
            pAI.setJob("attacker");
        }

        public static void callSpawndefencetowers(WorldTile pTile, string pPowerID)
        {
            if (pTile != null && pTile.zone != null && pTile.zone.hasCity())
            {
                // Get the city and kingdom from the tile's zone
                City city = pTile.zone.city;
                Kingdom kingdom = city.kingdom;

                if (kingdom != null)
                {
                    // Determine the race of the kingdom to decide which building to spawn
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
                            buildingId = "watch_tower_human"; // Default if race is unknown
                            break;
                    }

                    // Spawn the building
                    BuildingAsset building = AssetManager.buildings.get(buildingId);
                    MapBox.instance.buildings.addBuilding(buildingId, pTile);

                    // Get the newly spawned building
                    Building spawnedBuilding = pTile.building;

                    if (spawnedBuilding != null)
                    {
                        // Set the kingdom and city for the building
                        spawnedBuilding.setKingdom(kingdom);
                        city.addBuilding(spawnedBuilding);

                        Debug.Log($"Building {buildingId} spawned and assigned to {kingdom.data.name} ({raceId}) in city {city.data.name}");
                    }
                    else
                    {
                        Debug.LogError("Failed to spawn the building.");
                    }
                }
                else
                {
                    Debug.LogError("Tile does not belong to any kingdom.");
                }
            }
            else
            {
                Debug.LogError("Tile does not belong to any valid zone or city.");
            }
        }

        public static bool DefenderEffect(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
        {
            if (pAttackedBy != null)
            {
                Actor enemy = ReflectionUtility.GetField<Actor>(pAttackedBy, "a");
                Actor defender = ReflectionUtility.GetField<Actor>(pSelf, "a");

                // Check if the enemy is below level 3
                if (enemy.data.level < defender.data.level)
                {
                    // Make the enemy stop
                    enemy.addTrait("peaceful");
                    enemy.has_status_frozen = false;
                    enemy.CallMethod("makeWait", 5f);

                    // Make the defender not attack the enemy
                    defender.ai.setTask("end_job", true, true);

                    // Add peaceful trait to the defender
                    defender.addTrait("peaceful");

                    // Start a coroutine to kill the enemy after 5 seconds and remove the peaceful trait
                    defender.StartCoroutine(KillEnemyAndRemovePeacefulTraitAfterDelay(defender, enemy, 5f));
                }
            }
            return false;
        }

        private static IEnumerator KillEnemyAndRemovePeacefulTraitAfterDelay(Actor defender, Actor enemy, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (enemy != null && enemy.base_data.health > 0)
            {
                enemy.killHimself();// Kill the enemy
            }

            // Remove peaceful trait from the defender
            defender.removeTrait("peaceful");
        }

        /*   Will be useful when day comes, idk what this does tbh forgot about it done this last year

        private static Dictionary<string, float> assetChangeCooldowns = new Dictionary<string, float>();

        public static void ChangeActorAsset(Actor actor, string newAssetId, float cooldown = 10f)
        {
            if (actor == null)
            {
                Debug.LogError("Actor is null.");
                return;
            }

            if (assetChangeCooldowns.TryGetValue(actor.asset.id, out float lastChangeTime))
            {
                if (Time.time - lastChangeTime < cooldown)
                {
                    Debug.Log("Cooldown active. Cannot change asset yet.");
                    return;
                }
            }

            ActorAsset newAsset = AssetManager.actor_library.get(newAssetId) as ActorAsset;
            if (newAsset == null)
            {
                Debug.LogError("Asset with ID " + newAssetId + " not found.");
                return;
            }

            var loadAssetMethod = actor.GetType().GetMethod("loadAsset", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var updateStatsMethod = actor.GetType().GetMethod("updateStats", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (loadAssetMethod == null || updateStatsMethod == null)
            {
                Debug.LogError("Required method not found on actor.");
                return;
            }

            loadAssetMethod.Invoke(actor, new object[] { newAsset });
            updateStatsMethod.Invoke(actor, null);

            assetChangeCooldowns[actor.asset.id] = Time.time;

            Debug.Log("Actor asset changed successfully to " + newAssetId);
        }

                */

    }
}
