using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

public class MahoragaEffect : MonoBehaviour
{
    public int hitsToAdapt = 3; // Number of hits required to adapt
    public float hitCooldown = 3.0f; // Cooldown period in seconds to register the next hit
    public float statusEffectDurationThreshold = 3.0f; // Duration in seconds to gain immunity to status effects
    public float infinityAdaptTime = 5.0f; // Time required to adapt to Infinity

    private int meleeHits = 0;
    private int rangedHits = 0;
    private Actor actor;
    private bool meleeImmune = false;
    private bool rangedImmune = false;
    private float lastHitTime = 0;
    private float mountainTime = 0f;
    private bool mountainImmune = false;
    private bool permanentlyImmune = false;
    private float lavaTime = 0f;
    private bool lavaImmune = false;
    private bool permanentlyLavaImmune = false;

    private bool infinityImmune = false; // Track immunity to Infinity
    private float timeInInfinity = 0f; // Track time spent in Infinity


    private Dictionary<string, float> activeStatusEffects = new Dictionary<string, float>();
    private HashSet<string> permanentImmunities = new HashSet<string>();
    private HashSet<string> adaptedTraits = new HashSet<string>();
    private GameObject iconObject;
    private Dictionary<string, float> traitTimestamps = new Dictionary<string, float>();
    private HashSet<string> specificTraitsToAdapt = new HashSet<string>
{
    "cursed",
    "infected",
    "plague",
    "mushSpores",
    "tumorInfection",
    "death_mark",
    "madness"
};



    public void Initialize()
    {
        actor = GetComponent<Actor>();
    }

    private Sprite mahoragaIcon;

    private void Start()
    {
        actor = GetComponent<Actor>();

        // Load the Mahoraga icon sprite
        mahoragaIcon = Resources.Load<Sprite>("ui/Icons/mahoraga_icon");

        if (mahoragaIcon == null)
        {
            Debug.LogError("Mahoraga icon sprite not found.");
            return;
        }

        CreateMahoragaIcon();
    }

    public bool IsImmuneToInfinity()
    {
        return infinityImmune; // Check the immunity status
    }

    private void GrantMountainImmunity()
    {
        mountainImmune = true;
        permanentlyImmune = true;
        Debug.Log("Adapted to mountain damage");
    }

    public bool IsImmuneToMountainDamage()
    {
        return mountainImmune;
    }

    public bool IsPermanentlyImmuneToMountainDamage()
    {
        return permanentlyImmune;
    }

    private void GrantLavaImmunity()
    {
        lavaImmune = true;
        permanentlyLavaImmune = true;
        Debug.Log("Adapted to lava damage");
    }

    public bool IsImmuneToLavaDamage()
    {
        return lavaImmune;
    }

    public bool IsPermanentlyImmuneToLavaDamage()
    {
        return permanentlyLavaImmune;
    }

    private void CreateMahoragaIcon()
    {
        iconObject = new GameObject("MahoragaIcon");
        SpriteRenderer renderer = iconObject.AddComponent<SpriteRenderer>();
        renderer.sprite = mahoragaIcon;
        renderer.sortingLayerName = "EffectsBack";
        iconObject.transform.SetParent(actor.transform);
        iconObject.transform.localPosition = new Vector3(0.1096f, -3.1f, 0); // Adjust the Y value (second parameter) to move the image down
        iconObject.transform.localScale = new Vector3(0.06f, 0.06f, 1); // Adjust the scale as needed
        //  iconObject.transform.localPosition = new Vector3(0f, -6f, 0); // Adjust the Y value (second parameter) to move the image down
        //  iconObject.transform.localScale = new Vector3(0.08f, 0.08f, 1); // Adjust the scale as needed
    }

    private void Update()
    {
        UpdateStatusEffects();
        AdaptNegativeTraits();
        CheckInfinityAdaptation();
        if (actor.currentTile != null && actor.currentTile.Type == TileLibrary.mountains)
        {
            mountainTime += Time.deltaTime;
            if (mountainTime >= 5.0f && !mountainImmune)
            {
                GrantMountainImmunity();
            }
        }
        else
        {
            mountainTime = 0f; // Reset timer if not on a mountain tile
        }

        if (actor.currentTile.Type == TileLibrary.lava0 || actor.currentTile.Type == TileLibrary.lava1 || actor.currentTile.Type == TileLibrary.lava2 || actor.currentTile.Type == TileLibrary.lava3)
        {
            lavaTime += Time.deltaTime;
            if (lavaTime >= 5.0f && !lavaImmune)
            {
                GrantLavaImmunity();
            }
        }
        else
        {
            lavaTime = 0f; // Reset timer if not on a lava tile
        }

        if (iconObject != null)
        {
            // Rotate the icon (if necessary, this might not be needed here anymore)
            // iconObject.transform.Rotate(0, 0, 100 * Time.deltaTime); // Adjust rotation speed as needed
        }

    }

    private void CheckInfinityAdaptation()
    {
        if (IsInInfinity())
        {
            timeInInfinity += Time.deltaTime;
            if (timeInInfinity >= infinityAdaptTime && !infinityImmune)
            {
                GainInfinityImmunity();
            }
        }
        else
        {
            timeInInfinity = 0f; // Reset timer if not in Infinity
        }
    }

    private bool IsInInfinity()
    {
        Actor gojoActor = FindActorWithInfinity();
        if (gojoActor == null)
        {
            return false; // Ensure we return false if no actor with Infinity is found
        }

        float distance = Vector2.Distance(actor.currentPosition, gojoActor.currentPosition);
        return distance < gojoActor.GetComponent<InfinityTrait>().GetInfinityRange();
    }

    private Actor FindActorWithInfinity()
    {
        List<Actor> allActors = MapBox.instance.units.getSimpleList();
        foreach (Actor otherActor in allActors)
        {
            if (otherActor.isAlive() && otherActor.hasTrait("infinity_trait"))
            {
                return otherActor;
            }
        }
        return null; // Return null if no actor with Infinity is found
    }


    private void GainInfinityImmunity()
    {
        infinityImmune = true;
        Debug.Log("Adapted to Infinity and gained immunity.");

        // Add to the immune list in InfinityTrait
        InfinityTrait infinityTrait = FindActorWithInfinity()?.GetComponent<InfinityTrait>();
        if (infinityTrait != null)
        {
            infinityTrait.AddImmuneActor(actor);
        }
    }


    private void AdaptNegativeTraits()
    {
        // Record the current time
        float currentTime = Time.time;

        // Iterate over the traits and add timestamps if they don't already exist
        foreach (var traitID in actor.data.traits)
        {
            if (!traitTimestamps.ContainsKey(traitID))
            {
                traitTimestamps[traitID] = currentTime;
            }
        }

        List<string> traitsToRemove = new List<string>();

        // Iterate over the traits to find those that need to be removed
        foreach (var traitID in actor.data.traits.ToList()) // Convert to list to avoid modification issues
        {
            ActorTrait trait = AssetManager.traits.get(traitID);
            if (trait == null)
            {
                Debug.LogWarning($"Trait {traitID} not found in AssetManager.");
                continue;
            }

            // Check if the trait has been present for at least 5 seconds
            if ((trait.type == TraitType.Negative || trait.type == TraitType.Other || specificTraitsToAdapt.Contains(traitID)) && !adaptedTraits.Contains(traitID))
            {
                if (currentTime - traitTimestamps[traitID] >= 5.0f)
                {
                    traitsToRemove.Add(traitID);
                    adaptedTraits.Add(traitID);
                    traitTimestamps.Remove(traitID); // Remove the timestamp entry
                    actor.updateStats();
                    Debug.Log($"Adapted and removed trait: {traitID}");
                }
            }
        }

        // Remove the traits after the iteration
        foreach (var traitID in traitsToRemove)
        {
            actor.data.traits.Remove(traitID);
        }

        // Confirm the traits list after removal
        if (Time.frameCount % 60 == 0) // Log once every 60 frames (roughly once per second)
        {
            //         Debug.Log($"Current traits on actor after adaptation: {string.Join(", ", actor.data.traits)}");
        }
    }


    public bool IsTraitAdapted(string traitID)
    {
        return adaptedTraits.Contains(traitID);
    }

    public void RegisterHit(bool isMelee)
    {
        // Ensure that hits are only counted after the cooldown period
        if (Time.time - lastHitTime < hitCooldown)
        {
            return;
        }
        lastHitTime = Time.time;

        if (isMelee)
        {
            meleeHits++;
            rangedHits = 0;

            Debug.Log($"Melee hit registered. Melee Hits: {meleeHits}, Ranged Hits: {rangedHits}");

            if (meleeHits >= hitsToAdapt)
            {
                GainMeleeImmunity();
            }
        }
        else
        {
            rangedHits++;
            meleeHits = 0;

            Debug.Log($"Ranged hit registered. Melee Hits: {meleeHits}, Ranged Hits: {rangedHits}");

            if (rangedHits >= hitsToAdapt)
            {
                GainRangedImmunity();
            }
        }
    }


    private void GainMeleeImmunity()
    {
        if (!meleeImmune)
        {
            meleeImmune = true;
            rangedImmune = false;
            meleeHits = 0; // Reset hit count
            Debug.Log("Gained melee immunity");
        }
    }

    private void GainRangedImmunity()
    {
        if (!rangedImmune)
        {
            rangedImmune = true;
            meleeImmune = false;
            rangedHits = 0; // Reset hit count
            Debug.Log("Gained ranged immunity");
        }
    }

    public bool IsImmuneToMelee()
    {
        return meleeImmune;
    }

    public bool IsImmuneToRanged()
    {
        return rangedImmune;
    }

    private void UpdateStatusEffects()
    {
        // Create a separate dictionary to store updates
        Dictionary<string, float> updatedStatusEffects = new Dictionary<string, float>(activeStatusEffects);

        foreach (var statusEffect in activeStatusEffects)
        {
            if (permanentImmunities.Contains(statusEffect.Key))
            {
                updatedStatusEffects.Remove(statusEffect.Key);
                continue;
            }

            updatedStatusEffects[statusEffect.Key] += Time.deltaTime;

            if (updatedStatusEffects[statusEffect.Key] >= statusEffectDurationThreshold)
            {
                permanentImmunities.Add(statusEffect.Key);
                Debug.Log($"Gained permanent immunity to {statusEffect.Key}");
                updatedStatusEffects.Remove(statusEffect.Key);
            }
        }

        // Apply the updates after the loop completes
        activeStatusEffects = updatedStatusEffects;
    }

    public void RegisterStatusEffect(string statusEffect)
    {
        if (!permanentImmunities.Contains(statusEffect))
        {
            if (!activeStatusEffects.ContainsKey(statusEffect))
            {
                activeStatusEffects[statusEffect] = 0f;
            }
        }
    }

    public bool IsImmuneToStatusEffect(string statusEffect)
    {
        return permanentImmunities.Contains(statusEffect);
    }

}
