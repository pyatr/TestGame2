using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWall : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BoxCollider2D wallCollider;

    [SerializeField]
    private Vector2 startSize, startPos;

    public void SetHeight(float height, bool isDown)
    {
        float newHeight = startSize.y * height;
        int directionMod = isDown ? -1 : 1;
        spriteRenderer.size = new Vector2(startSize.x, newHeight);
        wallCollider.size = spriteRenderer.size;
        transform.localPosition = new Vector3(startPos.x, startPos.y + newHeight / 2f * directionMod - startSize.y / 2f * directionMod, 0);
    }
}
