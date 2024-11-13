﻿using ai.behaviours;
// USING SOME STUFF 4 TEST
public class EstablishKingdomTask : BehaviourTaskActor
{
    public EstablishKingdomTask()
    {
        id = "establish_kingdom_task";
    }

    public void init()
    {  // Removed `override` if it's not inheriting an `init` method
        addBeh(new FindUnoccupiedTile());
        addBeh(new MoveToTile());
        addBeh(new CreateKingdom());
    }
}