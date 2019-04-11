using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBMoveForward : MonoBehaviour
{
    public float moveSpeed = 1;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = Vector2.right * moveSpeed * Mathf.Sign(transform.localScale.x);
    }
}
