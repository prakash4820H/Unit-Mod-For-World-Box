using Unit;
using UnityEngine;

public static class WhisperofAlliance
{
    public static bool selectWhisperOfAlliance(string powerID)
    {
        Kingdom selectedKingdomA = Config.selectedKingdom;
        if (selectedKingdomA == null)
        {
            WorldTip.showNow("No kingdom selected.", true, "top", 3f);
            return false;
        }

        WorldTile tileA = World.world.GetTile(Mathf.FloorToInt(selectedKingdomA.capital.cityCenter.x), Mathf.FloorToInt(selectedKingdomA.capital.cityCenter.y));

        if (tileA == null)
        {
            WorldTip.showNow("No valid capital for the selected kingdom.", true, "top", 3f);
            return false;
        }

        WorldTip.showNow("Select the second kingdom", true, "top", 3f);
        Kingdom selectedKingdomB = Config.selectedKingdom; // Next step to select the second kingdom
        if (selectedKingdomB == null || selectedKingdomA == selectedKingdomB)
        {
            WorldTip.showNow("Invalid kingdom selection. Try again.", true, "top", 3f);
            return false;
        }

        checkAndFormAlliance(selectedKingdomA, selectedKingdomB);
        return true;
    }

    public static void showWhisperTip(string tipID)
    {
        switch (tipID)
        {
            case "whisper_selected_first":
                LocalizationUtility.AddOrSet(tipID, "First kingdom selected. Please select the second kingdom.");
                break;
            case "whisper_cancelled":
                LocalizationUtility.AddOrSet(tipID, "Alliance formation canceled. Please try again.");
                break;
            case "whisper_new_alliance":
                LocalizationUtility.AddOrSet(tipID, "New alliance formed successfully!");
                break;
            default:
                LocalizationUtility.AddOrSet(tipID, "Alliance selection feedback.");
                break;
        }

        WorldTip.showNow(tipID, true, "top", 3f);
    }

    public static bool checkAndFormAlliance(Kingdom kingdomA, Kingdom kingdomB)
    {
        Alliance allianceA = kingdomA.getAlliance();
        Alliance allianceB = kingdomB.getAlliance();
        if (allianceA != null && allianceB != null && allianceA != allianceB)
        {
            mergeAlliances(allianceA, allianceB);
            return true;
        }
        if (allianceA != null && allianceB == null)
        {
            addKingdomToAlliance(allianceA, kingdomB);
            return true;
        }

        if (allianceB != null && allianceA == null)
        {
            addKingdomToAlliance(allianceB, kingdomA);
            return true;
        }
        if (allianceA == null && allianceB == null)
        {
            formNewAlliance(kingdomA, kingdomB);
            return true;
        }
        return false;
    }

    public static void mergeAlliances(Alliance allianceA, Alliance allianceB)
    {
        foreach (var kingdom in allianceB.kingdoms_list)
        {
            allianceA.join(kingdom);
        }
        World.world.alliances.dissolveAlliance(allianceB);
        WorldTip.showNow("Two alliances have merged into one larger alliance!", true, "top", 3f);
    }

    public static void addKingdomToAlliance(Alliance alliance, Kingdom kingdom)
    {
        alliance.join(kingdom);
        WorldTip.showNow($"{kingdom.data.name} has joined an existing alliance!", true, "top", 3f);
    }


    public static void formNewAlliance(Kingdom kingdomA, Kingdom kingdomB)
    {
        AllianceManager allianceManager = World.world.alliances;
        Alliance newAlliance = allianceManager.newAlliance(kingdomA, kingdomB);
        WorldTip.showNow($"{kingdomA.data.name} and {kingdomB.data.name} have formed a new alliance!", true, "top", 3f);
    }

    public static bool clickWhisperOfAlliance(WorldTile pTile, string powerID)
    {
        City city = pTile.zone.city;
        if (city == null)
        {
            return false;
        }
        Kingdom kingdom = city.kingdom;
        if (Config.whisperA == null)
        {
            Config.whisperA = kingdom;
            showWhisperTip("whisper_selected_first");
            return false;
        }
        if (Config.whisperB == null && Config.whisperA == kingdom)
        {
            showWhisperTip("whisper_cancelled");
            Config.whisperA = null;
            Config.whisperB = null;
            return false;
        }
        if (Config.whisperB == null)
        {
            Config.whisperB = kingdom;
        }
        if (Config.whisperB != Config.whisperA)
        {
            AllianceManager allianceManager = World.world.alliances;
            allianceManager.newAlliance(Config.whisperA, Config.whisperB);
            showWhisperTip("whisper_new_alliance");

            // Reset the selection
            Config.whisperA = null;
            Config.whisperB = null;
        }

        return true;
    }

    public static bool resolveWarBeforeAlliance(Kingdom kingdomA, Kingdom kingdomB)
    {
        War existingWar = World.world.wars.getWar(kingdomA, kingdomB);
        if (existingWar != null)
        {
            World.world.wars.endWar(existingWar);
            WorldTip.showNow("The war between the kingdoms has ended!", true, "top", 3f);
            return true;
        }
        return false;
    }
}
