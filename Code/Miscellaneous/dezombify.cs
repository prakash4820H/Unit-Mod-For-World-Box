using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

public class DezombifyAction
{
    // Class to store full information about infected actors
    public class InfectedActorData
    {
        public string assetId;
        public int head;
        public int skin;
        public int skinSet;
        public ActorGender gender;
        public Race race;
        public string kingdomId;
        public string clanId;
        public string cityId;
        public int level; // Track level
        public double createdTime; // Track birth date
        public int experience;
        public string profession;
        public int children;
        public string culture;
        public string mood;
        public string favoriteFood;
        public List<string> originalTraits;
        public string originalName;
        public List<string> originalEquipment; // New: Track original equipment (as IDs or objects)
        public List<string> activeStatusEffects;
    }

    // Dictionary to store detailed data of infected actors
    public static Dictionary<string, InfectedActorData> infectedActorData = new Dictionary<string, InfectedActorData>();

    // Method to track infected actors with full information
    public static bool InfectedSpecialAction(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        if (pTarget == null || pTarget.a == null) return false;

        Actor actor = pTarget.a;

        if (actor.hasTrait("infected"))
        {
            string assetId = actor.asset.id;

            // Store full details of the actor
            if (!infectedActorData.ContainsKey(assetId))
            {
                InfectedActorData data = new InfectedActorData
                {
                    assetId = assetId,
                    head = actor.data.head,
                    skin = actor.data.skin,
                    skinSet = actor.data.skin_set,
                    gender = actor.data.gender,
                    kingdomId = actor.kingdom?.id,
                    race = actor.race,
                    level = actor.data.level,
                    clanId = actor.data.clan,
                    children = actor.data.children,
                    culture = actor.data.culture,
                    originalTraits = new List<string>(actor.data.traits), // Capture original traits
                    originalName = actor.getName() // Capture original name

                };
                infectedActorData[assetId] = data;
                Debug.Log($"Tracked infected actor with Asset ID: {assetId}");
            }
            return true;
        }
        return false;
    }

    // Dezombify action to revert a zombie to its original state

    public static void DezombifyDropAction(WorldTile pTile = null, string pDropID = null)
    {
        if (pTile == null) return;

        Actor zombie = ActionLibrary.getActorFromTile(pTile);

        if (zombie != null && zombie.hasTrait("zombie"))
        {
            if (infectedActorData.Count > 0)
            {
                // Pick a random stored Asset ID
                List<string> assetIds = new List<string>(infectedActorData.Keys);
                string assetId = assetIds[UnityEngine.Random.Range(0, assetIds.Count)];

                // Retrieve the detailed data
                InfectedActorData originalData = infectedActorData[assetId];

                // Restore the actor's attributes
                if (originalData != null)
                {
                    ActorAsset originalAsset = AssetManager.actor_library.get(originalData.assetId);
                    Race originalRace = originalData.race;

                    if (originalAsset != null && originalRace != null)
                    {
                        // Restore appearance and stats
                        zombie.loadAsset(originalAsset);
                        zombie.race = originalRace;
                        zombie.data.head = originalData.head;
                        zombie.data.skin = originalData.skin;
                        zombie.data.skin_set = originalData.skinSet;
                        zombie.data.gender = originalData.gender;
                        zombie.data.level = originalData.level;
                        zombie.data.experience = originalData.experience;
                        zombie.data.children = originalData.children;
                        zombie.data.mood = originalData.mood;
                        zombie.data.children = originalData.children;
                        zombie.data.culture = originalData.culture;
                        zombie.data.favoriteFood = originalData.favoriteFood;


                        // Restore original kingdom
                        if (!string.IsNullOrEmpty(originalData.kingdomId))
                        {
                            Kingdom originalKingdom = MapBox.instance.kingdoms.getKingdomByID(originalData.kingdomId);
                            if (originalKingdom != null)
                            {
                                zombie.setKingdom(originalKingdom);
                                Debug.Log($"Restored kingdom to: {originalKingdom.name}");
                            }
                        }
                        // Clear unwanted traits
                        List<string> zombieTraits = new List<string>(zombie.data.traits);
                        foreach (string trait in zombieTraits)
                        {
                            if (trait.Contains("zombie") || trait.Contains("infected"))
                            {
                                zombie.finishStatusEffect(trait);
                            }
                        }

                        // Restore original clan
                        if (!string.IsNullOrEmpty(originalData.clanId))
                        {
                            Clan originalClan = MapBox.instance.clans.get(originalData.clanId);
                            if (originalClan != null)
                            {
                                originalClan.addUnit(zombie);
                                Debug.Log($"Restored clan to: {originalClan.data.name}");
                            }
                        }

                        // Restore original city
                        if (!string.IsNullOrEmpty(originalData.cityId))
                        {
                            City originalCity = MapBox.instance.cities.get(originalData.cityId);
                            if (originalCity != null)
                            {
                                zombie.joinCity(originalCity);
                                Debug.Log($"Restored city to: {originalCity.getCityName()}");
                            }
                        }

                        // Restore original traits, excluding "infected"
                        zombie.data.traits = new List<string>(originalData.originalTraits.Where(trait => trait != "infected"));

                        // Restore original name, removing unwanted prefixes like "un"
                        zombie.data.setName(originalData.originalName);

                        // Refresh visual aspects
                        zombie.clearSprites();
                        zombie.updateStats();
                        zombie.checkHeadID();

                        LocalizationUtility.AddOrSet("dezombify_success", $"{zombie.getName()} has been cured and reverted to their original form!");

                        Debug.Log($"Dezombified actor: {zombie.getName()}");
                    }
                    else
                    {
                        Debug.LogError("Failed to retrieve asset or race for dezombifying.");
                    }

                    // Remove the actor data from tracking after successful dezombification
                    infectedActorData.Remove(assetId);
                }
            }
            else
            {
                Debug.Log("No more infected actors to revert.");
            }
        }
        else
        {
            Debug.Log("No valid zombie found on the tile.");
        }
    }
}

// Patch to hook into the infection process
[HarmonyPatch(typeof(ActionLibrary), "turnIntoZombie")]
public class TurnIntoZombiePatch
{
    static void Prefix(BaseSimObject pTarget)
    {
        Actor a = pTarget.a;
        if (a != null)
        {
            DezombifyAction.InfectedSpecialAction(a);
        }
    }
}
