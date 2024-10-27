public class WhisperOfConversionAction
{
    // Selection method
    public static bool selectWhisperOfConversion(string pPowerID)
    {
        WorldTip.showNow("city conversion tool selected", true, "top", 3f);
        Config1.whisperCityA = null;
        Config1.whisperCityB = null;
        return false;
    }

    // Clicking method to handle city conversion logic
    public static bool clickWhisperOfConversion(WorldTile pTile, string pPowerID)
    {
        City selectedCity = pTile.zone.city;
        if (selectedCity == null)
        {
            return false;
        }

        // Set the first city (source)
        if (Config1.whisperCityA == null)
        {
            Config1.whisperCityA = selectedCity;
            ActionLibrary.showWhisperTip("First city is selected , now it will convert to a second city that you will select again");
            return false;
        }

        // Set the second city (target) and proceed with conversion
        if (Config1.whisperCityB == null && Config1.whisperCityA != selectedCity)
        {
            Config1.whisperCityB = selectedCity;
            if (Config1.whisperCityA.kingdom != Config1.whisperCityB.kingdom)
            {
                // Convert the first city to the second city's kingdom
                Config1.whisperCityA.joinAnotherKingdom(Config1.whisperCityB.kingdom);
                ActionLibrary.showWhisperTip("City  conversion succeeded");
            }
            else
            {
                // Same kingdom, cancel the conversion
                ActionLibrary.showWhisperTip("bruh, its the same kingdom");
                Config1.whisperCityA = null;
                Config1.whisperCityB = null;
                return false;
            }
        }

        // Reset Config variables after the action is complete
        Config1.whisperCityA = null;
        Config1.whisperCityB = null;
        return true;
    }
    public static void showWhisperTip(string tipKey)
    {
        WorldTip.showNow(tipKey, true, "top", 3f);
    }
}

// Config class to hold temporary city references
public static class Config1
{
    public static City whisperCityA;
    public static City whisperCityB;
}
