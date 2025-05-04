using UnityEngine;

public class GapChecker : MonoBehaviour
{
    private bool boxInPlace = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            boxInPlace = true;
            Debug.Log("Box is covering the gap! Player can pass.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            boxInPlace = false;
            Debug.Log("Box moved away! Gap is open again.");
        }
    }

    public bool IsBoxCoveringGap()
    {
        return boxInPlace;
    }
}
