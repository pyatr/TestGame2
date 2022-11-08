using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float GravityScale
    {
        set => rigidbody2d.gravityScale = value;
        get => rigidbody2d.gravityScale;
    }

    public GameController GameController;

    [SerializeField]
    private Rigidbody2D rigidbody2d;
    [SerializeField]
    private Vector2 riseForce = Vector2.up;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody2d.AddForce(riseForce, ForceMode2D.Impulse);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        GameController?.StopGame();
    }
}
