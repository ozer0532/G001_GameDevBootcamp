using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public int health = 1;
    public int damage = 1;
    public float cooldown = 0.2f;
    public Rect attackBox;
    public LayerMask enemyLayer;

    bool attackInput;
    Animator anim;
    float cooldownTimer;
    PhysicsController phys;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        phys = GetComponent<PhysicsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            attackInput = true;
        }
    }

    private void FixedUpdate()
    {
        // When player clicks and the attack is not on cooldown
        if (attackInput && (cooldownTimer >= cooldown)) {
            // Get all gameobjects in hitbox
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(new Vector2((phys.facingRight)?attackBox.center.x:-attackBox.center.x, attackBox.center.y) + (Vector2)transform.position, attackBox.size, 0, enemyLayer);
            foreach (Collider2D hit in hitEnemies) {
                EnemyController enemy = hit.GetComponent<EnemyController>();
                // If the hit gameobject is an enemy, damage it
                if (enemy) {
                    enemy.Damage(damage);
                }
            }

            // Play attack animation, reset input, and starts the cooldown
            anim.SetTrigger("Attack");
            attackInput = false;
            cooldownTimer = 0;
        }

        // Increment the cooldown
        cooldownTimer += Time.deltaTime;
    }

    // Damages the player
    public void Damage(int damage) {
        health -= damage;
    }

    // Draws the sword hitbox
    private void OnDrawGizmos()
    {
        Color color = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(attackBox.center.x, attackBox.center.y, 0) + transform.position, (Vector3)attackBox.size + Vector3.forward);
        Gizmos.color = color;
    }
}
