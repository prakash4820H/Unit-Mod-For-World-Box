using System.Collections.Generic;
using System.Reflection;
using Unit;
using UnityEngine;

public class TraitColorCycle : ActorTrait
{
    public float cycleSpeed = 1.0f;
    private Renderer actorRenderer;
    private float cycleProgress = 0f;

    public TraitColorCycle()
    {
        this.id = "color_cycle";
        this.path_icon = "ui/Icons/RGBicon";
        this.group_id = UnitTraitGroup.Unit;
        this.action_special_effect = ApplyColorCycle;
        PlayerConfig.unlockTrait("color_cycle");
    }

    public static bool ApplyColorCycle(BaseSimObject self, WorldTile tile)
    {
        Actor actor = self as Actor;
        if (actor == null) return false;

        ColorCycleBehaviour behavior = actor.gameObject.GetComponent<ColorCycleBehaviour>();
        if (behavior == null)
        {
            actor.gameObject.AddComponent<ColorCycleBehaviour>();
        }
        return true;
    }
}

public class ColorCycleBehaviour : MonoBehaviour
{
    public float cycleSpeed = 1.0f;
    private Renderer actorRenderer;
    private float cycleProgress = 0f;

    void Start()
    {
        actorRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        cycleProgress += Time.deltaTime * cycleSpeed;
        Color newColor = GetColorFromCycle(cycleProgress);
        actorRenderer.material.color = newColor;
    }

    private Color GetColorFromCycle(float progress)
    {
        float r = Mathf.Sin(progress) * 0.5f + 0.5f;
        float g = Mathf.Sin(progress + 2 * Mathf.PI / 3) * 0.5f + 0.5f;
        float b = Mathf.Sin(progress + 4 * Mathf.PI / 3) * 0.5f + 0.5f;
        return new Color(r, g, b);
    }
}

public static class TraitColorCycleManager
{
    public static void RegisterColorCycleTrait()
    {
        ActorTrait colorCycleTrait = new TraitColorCycle();
        AssetManager.traits.add(colorCycleTrait);
    }

    public static void ApplyColorCycleTrait(Actor actor)
    {
        if (!actor.data.traits.Contains("color_cycle"))
        {
            actor.data.traits.Add("color_cycle");
        }
    }

    public static void AddTraitToLocalizedLibrary(string id, string description)
    {
        string language = LocalizedTextManager.instance.language;
        Dictionary<string, string> dictionary = (Dictionary<string, string>)typeof(LocalizedTextManager)
            .GetField("localizedText", BindingFlags.Instance | BindingFlags.NonPublic)
            .GetValue(LocalizedTextManager.instance);

        dictionary.Add("trait_" + id, id);
        dictionary.Add("trait_" + id + "_info", description);
    }
}

public class ModInitializerColorCycle
{
    public static void Init()
    {
        TraitColorCycleManager.RegisterColorCycleTrait();
        TraitColorCycleManager.AddTraitToLocalizedLibrary("color_cycle", "Applies a color cycling effect to the actor.");
    }
}
