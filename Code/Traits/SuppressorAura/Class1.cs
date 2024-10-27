using System.Collections.Generic;
using UnityEngine;

public class SuppressorAura : MonoBehaviour
{
    public static void Init()
    {
        ActorTrait suppressorAura = new ActorTrait
        {
            id = "suppressor_aura",
            path_icon = "ui/Icons/iconSuppressorAura",
            group_id = "special",
            action_special_effect = ApplySuppressorAura,
            special_effect_interval = 1f // Trigger every second
        };
        AssetManager.traits.add(suppressorAura);
        addTraitToLocalizedLibrary(suppressorAura.id, "Suppresses special effects in a radius.");
        PlayerConfig.unlockTrait("suppressor_aura");
    }

    public static bool ApplySuppressorAura(BaseSimObject pTarget = null, WorldTile pTile = null)
    {
        // Suppression logic handled in patch
        return true;
    }

    public static void addTraitToLocalizedLibrary(string id, string description)
    {
        string language = LocalizedTextManager.instance.language;
        Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(LocalizedTextManager)
            .GetField("localizedText", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .GetValue(LocalizedTextManager.instance);
        dictionary.Add("trait_" + id, id);
        dictionary.Add("trait_" + id + "_info", description);
    }
}
