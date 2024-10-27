using System;
using System.Collections.Generic;
using UnityEngine;

public class TraitReplicateEnemyPositiveTraits : ActorTrait
{
    private float proximityRadius = 5.0f;  // Set the proximity radius

    // Initialization method to load the trait into the game
    public static void Init()
    {
        ActorTrait replicatorTrait = new ActorTrait();
        replicatorTrait.id = "replicator_positive_traits";
        replicatorTrait.path_icon = "ui/Icons/iconReplicator";  // Ensure this icon path exists
        replicatorTrait.group_id = "special";  // Assign the trait to the special group
        replicatorTrait.inherit = 10f;  // Inherit percentage for trait transfer to future generations
        replicatorTrait.opposite = "non_replicator";  // Opposite trait that cannot coexist with this one

        replicatorTrait.action_special_effect = (WorldAction)Delegate.Combine(replicatorTrait.action_special_effect, new WorldAction(ReplicatePositiveTraitsAndStatsEffect));

        AssetManager.traits.add(replicatorTrait);
        addTraitToLocalizedLibrary(replicatorTrait.id, "Replicates positive traits and stats from nearby enemies.");
        PlayerConfig.unlockTrait("replicator_positive_traits");

        Debug.Log("Replicator positive traits and stats trait has been initialized and registered.");
    }

    // Method to perform the replication effect when nearby an enemy
    public static bool ReplicatePositiveTraitsAndStatsEffect(BaseSimObject pTarget, WorldTile pTile)
    {
        Actor actor = pTarget as Actor;
        if (actor != null && actor.data.traits.Contains("replicator_positive_traits"))
        {
            Actor nearestEnemy = FindNearestEnemy(actor, 5.0f);
            if (nearestEnemy != null)
            {
                // Replicate stats
                actor.stats.CopyFrom(nearestEnemy.stats);
                Debug.Log($"Replicated stats from: {nearestEnemy.name}");

                // Filter and replicate positive traits
                foreach (string traitId in nearestEnemy.data.traits)
                {
                    ActorTrait enemyTrait = AssetManager.traits.get(traitId);
                    if (enemyTrait != null && enemyTrait.type == TraitType.Positive && !actor.hasTrait(traitId))
                    {
                        actor.addTrait(traitId);  // Add trait by ID (string)
                        Debug.Log($"Replicated positive trait: {enemyTrait.id}");
                    }
                }

                actor.setStatsDirty();  // Ensure the actor updates its stats and visuals based on the new traits
            }
            return true;  // Return true when replication is successfully applied
        }
        return false;  // Return false if no replication occurred
    }

    // Helper method to find the nearest enemy within a certain radius
    private static Actor FindNearestEnemy(Actor actor, float radius)
    {
        List<Actor> nearbyEnemies = MapBox.instance.units.getSimpleList();
        foreach (Actor enemy in nearbyEnemies)
        {
            if (Vector2.Distance(actor.currentPosition, enemy.currentPosition) <= radius && IsEnemy(actor, enemy))
            {
                return enemy;  // Return the first enemy found within range
            }
        }
        return null;  // Return null if no enemy is found nearby
    }

    // Helper method to check if an actor is an enemy
    private static bool IsEnemy(Actor actor, Actor enemy)
    {
        return actor.kingdom.isEnemy(enemy.kingdom);  // Compare kingdom alignment
    }

    // Method to add trait information to the localized library
    public static void addTraitToLocalizedLibrary(string id, string description)
    {
        LocalizedTextManager.instance.localizedText.Add("trait_" + id, id);
        LocalizedTextManager.instance.localizedText.Add("trait_" + id + "_info", description);
    }
}

