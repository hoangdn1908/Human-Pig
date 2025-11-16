using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float dmg = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the explosion collides with the player
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(dmg); // Deal damage to the player
            
            }
        }
        Destroy(gameObject, 0.3f); // Destroy the explosion object after it collides


    }
}
