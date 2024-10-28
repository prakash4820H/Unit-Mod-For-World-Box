using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public static class LeaderTraitInit
{
    public static void Init()
    {
        ActorTrait trait = new ActorTrait
        {
            id = "leader_trait",
            path_icon = "ui/Icons/leader_icon.png",
            group_id = UnitTraitGroup.Unit,
            action_special_effect = ApplyTrait
        };
        AssetManager.traits.add(trait);
        LocalizationUtility.addTraitToLocalizedLibrary(trait.id, "Nearby humans follow this leader.");
        PlayerConfig.unlockTrait("leader_trait");
    }

    public static bool ApplyTrait(BaseSimObject pSelf, WorldTile pTile = null)
    {
        Actor actor = pSelf as Actor;
        if (actor == null || !actor.hasTrait("leader_trait"))
        {
            return false;
        }
        if (!actor.gameObject.GetComponent<LeaderBehaviour>())
        {
            actor.gameObject.AddComponent<LeaderBehaviour>();
        }
        return true;
    }
}

public class LeaderBehaviour : MonoBehaviour
{
    private Actor leader;
    private float followRadius = 20f; // Define radius within which actors will follow
    private List<Actor> followers = new List<Actor>();

    void Start()
    {
        leader = GetComponent<Actor>();
        if (leader == null)
        {
            Debug.LogError("Leader Actor component is missing.");
            return;
        }
    }

    void Update()
    {
        FindFollowers();
        CommandFollowers();
    }

    private void FindFollowers()
    {
        followers.Clear();
        foreach (Actor unit in MapBox.instance.units.getSimpleList())
        {
            if (unit.asset.id == "unit_human" && Vector2.Distance(leader.currentPosition, unit.currentPosition) <= followRadius)
            {
                if (!followers.Contains(unit))
                {
                    followers.Add(unit);
                }
            }
        }
    }

    private void CommandFollowers()
    {
        foreach (Actor follower in followers)
        {
            if (follower != null && follower.isAlive() && follower.beh_actor_target != leader)
            {
                follower.beh_actor_target = leader;
                follower.goTo(leader.currentTile, pPathOnWater: false, pWalkOnBlocks: false);
            }
        }
    }
}
