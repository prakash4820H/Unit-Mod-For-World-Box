using ai.behaviours;

public class MoveToTile : BehaviourActionActor
{
    public override BehResult execute(Actor actor)
    {
        if (actor.currentTile == actor.beh_tile_target)
        {
            return BehResult.Continue;
        }
        actor.moveTo(actor.beh_tile_target);  // Uses existing method for movement
        return BehResult.Continue;
    }
}
