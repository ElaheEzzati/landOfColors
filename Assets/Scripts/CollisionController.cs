using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject player;
    private bool hasCollectedMagic = false; // Tracks if magic is collected
    private bool hasGreenMagic = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
    // If player touches the magic, set hasCollectedMagic to true and remove magic
    if (collision.gameObject.CompareTag("magic"))
    {
        hasCollectedMagic = true;
        Debug.Log("Magic Collected!");
        Destroy(collision.gameObject); // Magic disappears
    }

    // If player touches water without collecting magic, reset position
    if (collision.gameObject.CompareTag("water"))
    {
        if (!hasCollectedMagic)
        {
            Debug.Log("No magic collected! Restarting...");
            player.transform.position = startPoint.transform.position;
            hasCollectedMagic = false;
        }
        else
        {
            Debug.Log("Magic collected! Player can pass through water!");
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), collision.collider, true);

            // Reset gravity so player doesn't float permanently
            player.GetComponent<Rigidbody2D>().gravityScale = 0; // Prevents falling
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0); // Stops upward movement

        }
    }

    // Move scene change logic outside of water check
    if (collision.gameObject.CompareTag("f"))
    {
        Debug.Log("level2");
        SceneManager.LoadScene("level2");
    }

        

        // If player collects green magic
        if (collision.gameObject.CompareTag("green"))
        {
            hasGreenMagic = true;
            Debug.Log("Green Magic Collected!");
            Destroy(collision.gameObject); // Magic disappears
        }

        // If player reaches finish, check for green magic
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (!hasGreenMagic)
            {
                Debug.Log("No Green Magic collected! Restarting...");
                player.transform.position = startPoint.transform.position;
                hasGreenMagic = false;
            }
            else
            {
                SceneManager.LoadScene("level3");
            }
        }
    
        if(collision.gameObject.CompareTag("gameOver"))
            {
                Debug.Log("Game Finished! Thanks for playing.");

                // OPTIONAL: Quit the game
                Application.Quit();
            }
}
}
