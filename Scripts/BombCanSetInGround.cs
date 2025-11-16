using UnityEngine;

public class BombCanSetInGround : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab; // Prefab for the explosion effect
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the bomb collides with the ground
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Instantiate explosion effect
            Destroy(gameObject); // Destroy the bomb
        }
    }
}
