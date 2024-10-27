using System.Collections.Generic;
using UnityEngine;

public class DefensiveShieldEffect : MonoBehaviour
{
    public float radius = 3f;
    public float rotationSpeed = 100f;
    public int shieldSegments = 6;

    private List<GameObject> shieldSegmentsList = new List<GameObject>();
    private Actor actor;

    public void Initialize()
    {
        actor = GetComponent<Actor>();

        for (int i = 0; i < shieldSegments; i++)
        {
            CreateShieldSegment(i);
        }

        Debug.Log($"Created {shieldSegmentsList.Count} shield segments."); // Debug log
    }

    private void CreateShieldSegment(int index)
    {
        GameObject segment = new GameObject($"ShieldSegment_{index}");
        SpriteRenderer renderer = segment.AddComponent<SpriteRenderer>();

        // Create a simple texture for the segment
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white); // Use a white pixel as the texture
        texture.Apply();

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        renderer.sprite = sprite;
        renderer.sortingLayerName = "EffectsTop";
        renderer.sortingOrder = 10; // Ensure the sprite is rendered on top

        segment.transform.SetParent(actor.transform);
        segment.transform.localScale = new Vector3(0.5f, 0.5f, 1); // Adjust the scale as needed

        shieldSegmentsList.Add(segment);
        Debug.Log($"Shield segment {index} created."); // Debug log
    }

    private void Update()
    {
        if (shieldSegmentsList.Count != shieldSegments)
        {
            Debug.LogError("Mismatch in shield segments count.");
            return;
        }

        float angleStep = 360f / shieldSegments;
        float time = Time.time * rotationSpeed;

        for (int i = 0; i < shieldSegmentsList.Count; i++)
        {
            float angle = (time + i * angleStep) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            shieldSegmentsList[i].transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
