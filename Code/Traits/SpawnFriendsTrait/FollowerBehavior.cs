using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class FriendFollowerBehaviour : MonoBehaviour
    {
        private Actor actor;
        private Actor target;
        private float followInterval = 0.2f; // Reduced interval for more frequent updates
        private float followTimer;
        private float minFollowDistance = 2f; // Minimum distance to keep from the main unit
        private float maxFollowDistance = 5f; // Maximum distance allowed from the main unit
        private WorldTile lastTile;

        public void SetTarget(Actor target)
        {
            this.target = target;
        }

        private void Start()
        {
            actor = GetComponent<Actor>();
            followTimer = followInterval; // Ensure immediate first update
            SyncPosition(); // Ensure position is synced at the start
        }

        private void Update()
        {
            followTimer += Time.deltaTime;
            if (followTimer >= followInterval)
            {
                followTimer = 0f;
                SyncPosition(); // Ensure the position is updated correctly
                FollowTarget(); // Follow the main unit
            }
        }

        private void SyncPosition()
        {
            if (actor.currentPosition != null)
            {
                // Get the current tile safely
                int x = Mathf.Clamp((int)actor.currentPosition.x, 0, MapBox.width - 1);
                int y = Mathf.Clamp((int)actor.currentPosition.y, 0, MapBox.height - 1);
                WorldTile currentTile = MapBox.instance.GetTileSimple(x, y);

                // Check if the tile has changed
                if (actor.currentTile != currentTile)
                {
                    // Update the tile and the position
                    actor.setCurrentTile(currentTile);
                    actor.updatePos();
                }
            }
        }

        private void FollowTarget()
        {
            if (target == null) return;

            // Calculate the distance to the target
            float distanceToTarget = Vector2.Distance(actor.currentPosition, target.currentPosition);

            // Only move towards the target if the distance is greater than the minimum follow distance
            if (distanceToTarget > minFollowDistance)
            {
                // Calculate the new target position within the allowed distance range
                Vector2 direction = (target.currentPosition - actor.currentPosition).normalized;
                Vector2 targetPosition = target.currentPosition - (Vector2)(direction * minFollowDistance);

                // Move the actor to the new target position
                MoveTowardsTarget(targetPosition);

                // If the actor is too far, force it to move closer
                if (distanceToTarget > maxFollowDistance)
                {
                    actor.setCurrentTile(target.currentTile);
                    actor.updatePos();
                }
            }
        }

        private void MoveTowardsTarget(Vector2 targetPosition)
        {
            WorldTile nearestTile = GetNearestAvailableTile(targetPosition);
            if (nearestTile != null && nearestTile != actor.currentTile)
            {
                actor.setCurrentTile(nearestTile);
                actor.updatePos();
            }
        }

        private WorldTile GetNearestAvailableTile(Vector2 targetPosition)
        {
            WorldTile targetTile = MapBox.instance.GetTileSimple((int)targetPosition.x, (int)targetPosition.y);
            List<WorldTile> neighbors = new List<WorldTile>();

            // Add all neighboring tiles to the list
            if (targetTile.tile_up != null) neighbors.Add(targetTile.tile_up);
            if (targetTile.tile_down != null) neighbors.Add(targetTile.tile_down);
            if (targetTile.tile_left != null) neighbors.Add(targetTile.tile_left);
            if (targetTile.tile_right != null) neighbors.Add(targetTile.tile_right);

            // Find the nearest walkable and empty tile
            WorldTile nearestTile = null;
            float nearestDistance = float.MaxValue;
            foreach (WorldTile tile in neighbors)
            {
                if (IsTileWalkable(tile) && tile != actor.currentTile)
                {
                    float distance = Vector2.Distance(tile.pos, targetTile.pos);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestTile = tile;
                    }
                }
            }

            return nearestTile;
        }

        private bool IsTileWalkable(WorldTile tile)
        {
            // Implement a walkability check based on your game's logic.
            return !tile.Type.liquid && !tile.Type.block && tile.building == null;
        }
    }
}
