using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2DBehaviour : MonoBehaviour {

    private const float skinWidth = .2f;

    public BoxCollider2D c2d { get; private set; }
    private RaycastInfo raycastInfo;
    private CollisionInfo collisionInfo;

    [SerializeField]
    private int horizontalRayCount;
    private float horizontalRaySpace;

    [SerializeField]
    private int verticalRayCount;
    private float verticalRaySpace;

    [SerializeField]
    private LayerMask collisionMask;

    private void Start()
    {
        c2d = GetComponent<BoxCollider2D>();
        CalculateRaySpace();
    }

    public void Move(Vector2 velocity)
    {
        UpdateRaycastPosition();
        collisionInfo.Reset();

        HorizontalCollision(ref velocity);
        VerticalCollision(ref velocity);

        transform.Translate(velocity);
    }

    private void HorizontalCollision(ref Vector2 velocity)
    {
        int directionX = (int)Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = ((int)directionX == -1) ? raycastInfo.bottomLeft : raycastInfo.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpace * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisionInfo.left = directionX == -1;
                collisionInfo.right = directionX == 1;
            }
        }
    }

    private void VerticalCollision(ref Vector2 velocity)
    {
        int directionY = (int)Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastInfo.bottomLeft : raycastInfo.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpace * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisionInfo.below = directionY == -1;
                //collisionInfo.above = directionY == 1;
                collisionInfo.above = false;
            }
        }

    }

    private void UpdateRaycastPosition()
    {
        Bounds bounds = c2d.bounds;
        bounds.Expand(skinWidth * -2);

        raycastInfo.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastInfo.bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        raycastInfo.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastInfo.topRight = new Vector2(bounds.max.x, bounds.max.y);

    }

    private void CalculateRaySpace()
    {
        Bounds bounds = c2d.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpace = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpace = bounds.size.x / (verticalRayCount - 1);
    }

    public CollisionInfo GetCollisionInfo()
    {
        return collisionInfo;
    }

    private struct RaycastInfo
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}
