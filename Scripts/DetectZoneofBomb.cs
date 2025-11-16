using UnityEngine;

public class DetectZoneofBomb : MonoBehaviour
{
    private PigThrowTheBomb pigThrowTheBomb;
    private void Awake()
    {
        pigThrowTheBomb = GetComponentInParent<PigThrowTheBomb>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pigThrowTheBomb != null) // Check if the collided object is tagged as "Player"
        {
            pigThrowTheBomb.SetPlayerInRange(true); // Call method to set player in range
            
            Debug.Log("Player entered detection zone of bomb");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pigThrowTheBomb != null) // Check if the object exiting the detection zone is tagged as "Player"
        {
            pigThrowTheBomb.SetPlayerInRange(false); // Call method to set player not in range
            
            Debug.Log("Player exited detection zone of bomb");
        }
    }
}
