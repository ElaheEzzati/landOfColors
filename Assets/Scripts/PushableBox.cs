using UnityEngine;

public class PushableBox : MonoBehaviour
{
    public float pushForce = 5f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Rigidbody2D boxRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Move box in the direction of player movement
            float horizontalInput = Input.GetAxis("Horizontal");
            boxRb.velocity = new Vector2(horizontalInput * pushForce, boxRb.velocity.y);
        }
    }
}
