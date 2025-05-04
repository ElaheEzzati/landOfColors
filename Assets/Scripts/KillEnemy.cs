using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public GameObject enemy; // Reference to enemy object

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();

            // Check if the player is falling downward
            if (playerRb.velocity.y < 0)
            {
                Debug.Log("Enemy defeated!");
                Destroy(enemy); // Remove the enemy

                // Apply bounce effect after enemy defeat
                playerRb.velocity = new Vector2(playerRb.velocity.x, 8);
            }
        }
    }
}
