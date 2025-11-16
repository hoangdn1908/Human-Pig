using UnityEngine;

public class NormalPigAttackZone : MonoBehaviour
{   public float damage = 10f; // Damage dealt by the pig
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the collided object is tagged as "Player"
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
               player.TakeDamage(damage); // Deal 10 damage to the player
                Debug.Log("Player hit by Pig");
            }
        }
    }
}
