using ai.behaviours;

public class CreateKingdom : BehaviourActionActor
{
    public override BehResult execute(Actor actor)
    {
        WorldTile tile = actor.currentTile;

        if (tile.building == null && !tile.hasUnits())
        {
            City newCity = World.world.cities.buildNewCity(tile.zone, actor.race, null);  // Correctly initializes the city
            newCity._cityTile = tile;  // Sets the primary tile for the city

            Kingdom newKingdom = World.world.kingdoms.makeNewCivKingdom(newCity, actor.race);  // Creates kingdom with the city

            if (newKingdom != null)
            {
                actor.joinCity(newCity);  // Actor joins the new city
                newKingdom.addCity(newCity);
                return BehResult.Continue;
            }
        }
        return BehResult.Stop;
    }
}
