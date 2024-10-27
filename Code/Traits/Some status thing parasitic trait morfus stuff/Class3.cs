using Unit;
using UnityEngine;

public class ExternalParasiteInvisibility : MonoBehaviour
{
    private Actor actor;
    private SpriteRenderer spriteRenderer;
    private float invisibilityDuration = 10f;
    private ActorAsset originalAsset;
    private ActorAsset modifiedAsset;

    public static void ApplyTo(Actor actor)
    {
        var invisibility = actor.gameObject.AddComponent<ExternalParasiteInvisibility>();
        invisibility.StartInvisibility();
    }

    private void StartInvisibility()
    {
        actor = GetComponent<Actor>();
        spriteRenderer = actor.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        }

        // Copy the original asset and modify it
        originalAsset = actor.asset;
        modifiedAsset = ActorAssetUtility.DeepCopyActorAsset(originalAsset);
        modifiedAsset.canBeKilledByStuff = false;
        modifiedAsset.canBeHurtByPowers = false;

        actor.asset = modifiedAsset;
        actor.updateStats();
        actor.has_status_frozen = true;
        actor.setShowShadow(false);
        Invoke(nameof(EndInvisibility), invisibilityDuration);
    }

    private void EndInvisibility()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }

        // Revert to the original asset
        actor.asset = originalAsset;
        actor.updateStats();
        actor.has_status_frozen = false;
        actor.setShowShadow(true);
        Destroy(this);
    }
}
