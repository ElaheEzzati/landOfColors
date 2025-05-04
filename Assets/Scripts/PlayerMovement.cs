using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {   
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Check if the player is near the gap and if box is covering it
        if (NearGap() && !FindObjectOfType<GapChecker>().IsBoxCoveringGap())
        {
            Debug.Log("Box is missing! Player cannot cross.");
            horizontalInput = 0; // Stops movement near the gap if box isn't placed
        }

        // Apply movement
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(8, 8, 8);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-8, 8, 8);

        // Jumping logic
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed + 1);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }

    private bool NearGap()
    {
        // Define the area near the gap where the check should occur
        return transform.position.x > 10 && transform.position.x < 12;
    }

    public float pushForce = 5f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Rigidbody2D boxRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Push only if the player is moving left or right
            float horizontalInput = Input.GetAxis("Horizontal");

            if (Mathf.Abs(horizontalInput) > 0)
            {
                boxRb.AddForce(new Vector2(horizontalInput * pushForce, 0), ForceMode2D.Impulse);
            }
        }
    }
    
}
