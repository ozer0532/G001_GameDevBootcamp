using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour {

    [Header("Movement Parameters")]
    public float moveSpeed = 10;
    public float jumpStrength = 30;
    float xVelocity;
    public bool facingRight;

    // References to others
    public Transform groundChecker;

    [Header("Animator Parameters")]
    public string runAnimation;
    public string fallAnimation;

    // References to components
    Rigidbody2D rb2d;
    Animator anim;

    [Header("Scoring Parameters")]
    public float addPointsDuration;
    private float addPointsTimer;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Edits the player rotation
        transform.localScale = new Vector3((facingRight) ? 1f : -1f, 1f, 1f);
    }

    private void FixedUpdate() {
        // Horizontal Movement
        Vector2 velocity = rb2d.velocity;
        float xInput = Input.GetAxisRaw("Horizontal");
        velocity.x = Mathf.SmoothDamp(velocity.x, xInput * moveSpeed, ref xVelocity, 0.1f);         // Apply horizontal smoothing
        if (xInput > 0) {
            facingRight = true;
            anim.SetBool(runAnimation, true);
            addPointsTimer = 0;
        } else if (xInput < 0) {
            facingRight = false;
            anim.SetBool(runAnimation, true);
            addPointsTimer = 0;
        } else {
            anim.SetBool(runAnimation, false);
            addPointsTimer += Time.fixedDeltaTime;
        }


        // Jumping
        if (Physics2D.OverlapPoint(groundChecker.position)) {
            if (Input.GetButton("Jump")) {
                velocity.y = jumpStrength;
            }
            anim.SetBool(fallAnimation, false);
        } else {
            anim.SetBool(fallAnimation, true);
        }

        // Modify player velocity
        rb2d.velocity = velocity;


        // Scoring
        while (addPointsTimer >= addPointsDuration) {
            GameMaster.score++;
            addPointsTimer -= addPointsDuration;
        }
    }
}
