using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 1;
    public int damage = 1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Damage this enemy
    public void Damage(int damage) {
        health -= damage;

        // Kills this enemy when health reaches zero
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    // Check collision against player
    private void OnCollisionEnter2D(Collision2D collision) {
        FighterController player = collision.gameObject.GetComponent<FighterController>();
        // If the collided gameobject is a player (has the fighter component)
        if (player) {
            // Damage it
            player.Damage(damage);
        }
    }
}
