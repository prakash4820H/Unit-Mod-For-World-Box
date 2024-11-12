using ai.behaviours;
using ai;

public class FindUnoccupiedTile : BehaviourActionActor
{
    public override BehResult execute(Actor actor)
    {
        WorldTile targetTile = ActorTool.getRandomTileForBoat(actor);
        if (targetTile != null)
        {
            actor.beh_tile_target = targetTile;
            return BehResult.Continue;
        }
        return BehResult.Stop;
    }
}
