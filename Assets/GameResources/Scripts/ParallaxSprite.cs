using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxSprite : MonoBehaviour
{
    [SerializeField]
    private Vector2 parallaxDirection = Vector2.right;
    [SerializeField]
    private float parallaxDistanceMod = 2f;

    private SpriteRenderer spriteRenderer;
    private GameController gameController;

    private Vector2 originalSize = Vector2.zero;
    private Vector2 maxSize = Vector2.zero;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.drawMode == SpriteDrawMode.Simple)
            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        originalSize = spriteRenderer.size;
        maxSize = originalSize * parallaxDistanceMod;
        gameController = FindObjectOfType<GameController>();
    }

    public void Restore()
    {
        spriteRenderer.size = originalSize;
    }

    private void Update()
    {
        spriteRenderer.size += gameController.GameSpeed * Time.deltaTime * parallaxDirection;
        if (spriteRenderer.size.x > maxSize.x || spriteRenderer.size.y > maxSize.y)
        {
            Restore();
        }
    }
}
