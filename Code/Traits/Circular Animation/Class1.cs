using System.Collections.Generic;
using UnityEngine;

public class CircularSpriteEffect : MonoBehaviour
{
    public List<Sprite> sprites;
    public float radius = 7f;
    public float speed = 60f;
    public float followSpeed = 30f; // Speed for following the cursor
    public int initialSpriteCount = 3;
    public int maxSpriteCount = 3;
    public float sizeReduction = 0.5f;
    public float gap = 0f;
    public string sortingLayerName = "EffectsTop";
    public float animationSpeed = 0.1f;
    public float attackRange = 20f;
    public float damagePerSecond = 30f;
    public float unitGap = 10f; // Example increased gap for upward offset
    public float verticalOffset = 2f; // Adjust this for upward/downward movement


    public bool increaseSpritesOnKill = false;

    private List<GameObject> spriteObjects = new List<GameObject>();
    private List<Queue<Vector3>> spritePaths; // Store paths for each sprite
    private int pathResolution = 10; // Number of positions to store in path
    private Actor actor;
    private int currentFrame = 0;
    private float animationTimer = 0f;
    private Dictionary<GameObject, Actor> attackingSprites = new Dictionary<GameObject, Actor>();
    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    private int currentSpriteCount;
    private bool followCursor = false; // Track whether sprites should follow the cursor

    private void Start()
    {
        actor = GetComponent<Actor>();

        // Load all sprites from the specified path
        sprites = new List<Sprite>();
        for (int i = 1; i <= 10; i++)
        {
            Sprite sprite = Resources.Load<Sprite>($"ui/Icons/bluefire/frame{i}");
            if (sprite != null)
            {
                sprites.Add(sprite);
            }
        }

        if (sprites.Count == 0)
        {
            Debug.LogError("No sprites found in the specified path.");
            return;
        }

        currentSpriteCount = increaseSpritesOnKill ? initialSpriteCount : maxSpriteCount;

        for (int i = 0; i < currentSpriteCount; i++)
        {
            CreateSprite(i);
        }

        // Initialize paths for each sprite
        spritePaths = new List<Queue<Vector3>>(currentSpriteCount);
        for (int i = 0; i < currentSpriteCount; i++)
        {
            spritePaths.Add(new Queue<Vector3>());
        }
    }

    private void CreateSprite(int index)
    {
        GameObject spriteObject = new GameObject("RevolvingSprite_" + index);
        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprites[0];
        renderer.sortingLayerName = sortingLayerName;
        spriteObject.transform.SetParent(actor.transform);
        spriteObject.transform.localScale = new Vector3(sizeReduction, sizeReduction, 1);
        spriteObjects.Add(spriteObject);
        attackingSprites[spriteObject] = null;
    }

    private GameObject CreateNewSprite()
    {
        if (spriteObjects.Count >= maxSpriteCount)
        {
            return null;
        }

        GameObject spriteObject = new GameObject("RevolvingSprite_" + spriteObjects.Count);
        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprites[0];
        renderer.sortingLayerName = sortingLayerName;
        spriteObject.transform.SetParent(actor.transform);
        spriteObject.transform.localScale = new Vector3(sizeReduction, sizeReduction, 1);
        spriteObjects.Add(spriteObject);
        attackingSprites[spriteObject] = null;

        // Initialize path for the new sprite
        spritePaths.Add(new Queue<Vector3>());

        return spriteObject;
    }


    private void Update()
    {
        // Toggle sprite behavior with key press
        if (Input.GetKeyDown(KeyCode.K))
        {
            followCursor = !followCursor;
            ClearPaths();
        }

        if (spriteObjects.Count == 0) return;

        // Animate sprites continuously
        AnimateSprites();

        List<GameObject> spritesToAdd = new List<GameObject>();  // Temporary list for new sprites
        List<GameObject> spritesToRemove = new List<GameObject>();  // Temporary list for sprites to remove

        if (followCursor)
        {
            // Disable actor following while following the cursor
            MoveSpritesToCursorInOrder();
        }
        else
        {
            // Check if the actor has the DarkSouls trait
            if (!actor.hasTrait("DarkSouls"))
            {
                DestroyAllSprites();
                return;
            }

            DetectAndAttackEnemies();

            // Use a for loop with index to safely iterate and modify collections
            for (int i = 0; i < spriteObjects.Count; i++)
            {
                GameObject spriteObject = spriteObjects[i];
                if (spriteObject == null) continue;

                if (attackingSprites.TryGetValue(spriteObject, out Actor target) && target != null)
                {
                    MoveSpriteToEnemy(spriteObject, spritesToAdd);
                }
                else
                {
                    RevolveSprite(spriteObject);
                }
            }
        }

        // Apply additions and removals after iteration
        foreach (var spriteObject in spritesToAdd)
        {
            spriteObjects.Add(spriteObject);
            spritePaths.Add(new Queue<Vector3>());  // Initialize path for new sprite
            attackingSprites[spriteObject] = null;
        }

        foreach (var spriteObject in spritesToRemove)
        {
            if (spriteObjects.Remove(spriteObject))
            {
                // Find index to remove the corresponding path
                int index = spriteObjects.IndexOf(spriteObject);
                if (index >= 0 && index < spritePaths.Count)
                {
                    spritePaths.RemoveAt(index);
                }
            }
        }
    }



    private void MoveSpritesToCursorInOrder()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        for (int i = 0; i < spriteObjects.Count; i++)
        {
            GameObject spriteObject = spriteObjects[i];
            if (spriteObject == null) continue;

            // Ensure path exists for each sprite
            if (spritePaths.Count <= i)
            {
                spritePaths.Add(new Queue<Vector3>());
            }

            Queue<Vector3> path = spritePaths[i];

            if (i == 0)
            {
                // Lead sprite moves directly towards the cursor
                Vector3 direction = (cursorWorldPosition - spriteObject.transform.position).normalized;
                spriteObject.transform.position += direction * followSpeed * Time.deltaTime;

                // Add current position to the path
                if (path.Count == pathResolution)
                {
                    path.Dequeue();
                }
                path.Enqueue(spriteObject.transform.position);
            }
            else
            {
                // Follow the path of the previous sprite
                if (spritePaths[i - 1].Count > 0)
                {
                    Vector3 targetPosition = spritePaths[i - 1].Peek();
                    spriteObject.transform.position = Vector3.MoveTowards(spriteObject.transform.position, targetPosition, followSpeed * Time.deltaTime);

                    // Update path
                    if (Vector3.Distance(spriteObject.transform.position, targetPosition) < 0.1f)
                    {
                        path.Enqueue(targetPosition);
                        spritePaths[i - 1].Dequeue();
                    }
                }
            }

            // Handle revolving once reached the cursor
            if (Vector3.Distance(spriteObject.transform.position, cursorWorldPosition) < 0.1f)
            {
                RevolveAroundCursor(spriteObject, cursorWorldPosition, i, 1.5f);
            }
        }
    }


    private void RevolveAroundCursor(GameObject spriteObject, Vector3 center, int index, float radius)
    {
        float angleStep = 360f / spriteObjects.Count;
        float angle = Time.time * speed + index * angleStep;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius + verticalOffset; // Add vertical offset

        spriteObject.transform.position = new Vector3(center.x + x, center.y + y, 0);
    }


    private void ClearPaths()
    {
        // Clear all paths when switching target mode
        foreach (var path in spritePaths)
        {
            path.Clear();
        }
    }

    private void DestroyAllSprites()
    {
        foreach (var spriteObject in spriteObjects)
        {
            if (spriteObject != null)
            {
                Destroy(spriteObject);
            }
        }
        spriteObjects.Clear();
        attackingSprites.Clear();
        damageTimers.Clear();
    }

    private void DetectAndAttackEnemies()
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, attackRange);

        if (nearbyEnemies.Count == 0)
        {
            return;
        }

        if (nearbyEnemies.Count >= spriteObjects.Count)
        {
            for (int i = 0; i < spriteObjects.Count; i++)
            {
                attackingSprites[spriteObjects[i]] = nearbyEnemies[i];
            }
        }
        else
        {
            int spritesPerEnemy = spriteObjects.Count / nearbyEnemies.Count;
            int extraSprites = spriteObjects.Count % nearbyEnemies.Count;

            int spriteIndex = 0;
            foreach (var enemy in nearbyEnemies)
            {
                for (int i = 0; i < spritesPerEnemy; i++)
                {
                    attackingSprites[spriteObjects[spriteIndex]] = enemy;
                    spriteIndex++;
                }

                if (extraSprites > 0)
                {
                    attackingSprites[spriteObjects[spriteIndex]] = enemy;
                    spriteIndex++;
                    extraSprites--;
                }
            }
        }
    }

    private List<Actor> GetNearbyEnemies(Actor actor, float range)
    {
        List<Actor> nearbyEnemies = new List<Actor>();
        List<Actor> allActors = MapBox.instance.units.getSimpleList();

        foreach (Actor otherActor in allActors)
        {
            if (Vector2.Distance(actor.currentPosition, otherActor.currentPosition) <= range && IsEnemy(otherActor))
            {
                nearbyEnemies.Add(otherActor);
            }
        }

        return nearbyEnemies;
    }

    private bool IsEnemy(Actor enemy)
    {
        return actor.kingdom.isEnemy(enemy.kingdom);
    }

    private void MoveSpriteToEnemy(GameObject spriteObject, List<GameObject> spritesToAdd)
    {
        if (spriteObject == null || !attackingSprites.TryGetValue(spriteObject, out Actor targetEnemy) || targetEnemy == null)
        {
            ReturnSpriteToActor(spriteObject);
            return;
        }

        if (targetEnemy.data.health <= 0)
        {
            // Ensure the kill count is incremented only once per enemy
            if (!targetEnemy.data.traits.Contains("countedKill"))
            {
                TransferItemsToMainUnit(targetEnemy); // Transfer items to main unit
                actor.data.kills++; // Increment kill count
                actor.addExperience(20);
                targetEnemy.data.traits.Add("countedKill"); // Mark this enemy as counted

                if (increaseSpritesOnKill)
                {
                    var newSprite = CreateNewSprite();
                    if (newSprite != null)
                    {
                        spritesToAdd.Add(newSprite);
                    }
                }
            }

            Actor newTarget = FindNewTarget(spriteObject);
            if (newTarget != null)
            {
                attackingSprites[spriteObject] = newTarget;
            }
            else
            {
                ReturnSpriteToActor(spriteObject);
            }
            return;
        }

        spriteObject.transform.position = Vector3.MoveTowards(spriteObject.transform.position, targetEnemy.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(spriteObject.transform.position, targetEnemy.transform.position) < 0.1f)
        {
            // Increase sprite size when on the enemy
            spriteObject.transform.localScale = new Vector3(sizeReduction * 2, sizeReduction * 2, 1); // Double the size

            // Count the number of sprites attacking the same enemy
            int attackingSpriteCount = 0;
            foreach (var entry in attackingSprites)
            {
                if (entry.Value == targetEnemy)
                {
                    attackingSpriteCount++;
                }
            }

            // Apply damage in discrete intervals
            if (!damageTimers.ContainsKey(spriteObject))
            {
                damageTimers[spriteObject] = 0f;
            }

            damageTimers[spriteObject] += Time.deltaTime;
            if (damageTimers[spriteObject] >= 1f)
            {
                targetEnemy.getHit(damagePerSecond * attackingSpriteCount);
                damageTimers[spriteObject] = 0f;
            }
        }
    }

    private void AddNewSprite()
    {
        if (spriteObjects.Count >= maxSpriteCount) return;

        GameObject spriteObject = new GameObject("RevolvingSprite_" + spriteObjects.Count);
        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprites[0];
        renderer.sortingLayerName = sortingLayerName;
        spriteObject.transform.SetParent(actor.transform);
        spriteObject.transform.localScale = new Vector3(sizeReduction, sizeReduction, 1);
        spriteObjects.Add(spriteObject);
        attackingSprites[spriteObject] = null;
    }

    private Actor FindNewTarget(GameObject spriteObject)
    {
        List<Actor> nearbyEnemies = GetNearbyEnemies(actor, attackRange);

        foreach (var enemy in nearbyEnemies)
        {
            if (!attackingSprites.ContainsValue(enemy))
            {
                return enemy;
            }
        }
        return null;
    }

    private void ReturnSpriteToActor(GameObject spriteObject)
    {
        if (spriteObject == null) return;

        attackingSprites[spriteObject] = null;

        // Reset sprite size to normal when returning
        spriteObject.transform.localScale = new Vector3(sizeReduction, sizeReduction, 1);

        // Move sprite back to unit's position
        spriteObject.transform.localPosition = Vector3.zero;
    }

    private void TransferItemsToMainUnit(Actor enemy)
    {
        Dictionary<EquipmentType, ItemData> enemyEquipments = GetEquipmentFromActor(enemy);

        foreach (var equipment in enemyEquipments)
        {
            EquipStrongestItem(actor, equipment.Value);
        }
    }

    private Dictionary<EquipmentType, ItemData> GetEquipmentFromActor(Actor actor)
    {
        Dictionary<EquipmentType, ItemData> equipments = new Dictionary<EquipmentType, ItemData>();

        if (actor.equipment == null) return equipments;

        List<ActorEquipmentSlot> equipmentSlots = ActorEquipment.getList(actor.equipment);
        if (equipmentSlots == null) return equipments;

        foreach (ActorEquipmentSlot slot in equipmentSlots)
        {
            if (slot.data != null)
            {
                equipments[AssetManager.items.get(slot.data.id).equipmentType] = slot.data;
            }
        }

        return equipments;
    }

    private void EquipStrongestItem(Actor actor, ItemData newEquipment)
    {
        ItemAsset itemAsset = AssetManager.items.get(newEquipment.id);
        if (itemAsset == null) return;

        EquipmentType equipmentType = itemAsset.equipmentType;
        if (actor.equipment == null)
        {
            actor.equipment = new ActorEquipment();
        }

        ActorEquipmentSlot slot = actor.equipment.getSlot(equipmentType);
        if (slot.data == null || GetItemValue(newEquipment) > GetItemValue(slot.data))
        {
            slot.setItem(newEquipment);
            actor.setStatsDirty();
            actor.dirty_sprite_item = true;
        }
    }

    private int GetItemValue(ItemData itemData)
    {
        ItemTools.calcItemValues(itemData);
        return ItemTools.s_value;
    }

    private void RevolveSprite(GameObject spriteObject)
    {
        float angleStep = 360f / spriteObjects.Count;
        float time = Time.time * speed;
        int index = spriteObjects.IndexOf(spriteObject);

        float angle = (time + index * angleStep) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle) * (radius + unitGap + index * gap);
        float y = Mathf.Sin(angle) * (radius + unitGap + index * gap) + verticalOffset; // Add vertical offset

        spriteObject.transform.localPosition = new Vector3(x, y, 0);
    }


    private void AnimateSprites()
    {
        animationTimer += Time.deltaTime;
        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;
            currentFrame = (currentFrame + 1) % sprites.Count;
            foreach (var spriteObject in spriteObjects)
            {
                SpriteRenderer renderer = spriteObject.GetComponent<SpriteRenderer>();
                renderer.sprite = sprites[currentFrame];
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var spriteObject in spriteObjects)
        {
            if (spriteObject != null)
            {
                Destroy(spriteObject);
            }
        }
    }
}
