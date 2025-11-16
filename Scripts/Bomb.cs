using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab; // Prefab for the explosion effect
    public float speed = 5f;
    public float height = 2f;
    public float lifeTime = 3f;

    private Vector2 startPoint;
    private Vector2 targetPoint;
    private float timer = 0f;

    public void Launch(Vector2 target)
    {
        startPoint = transform.position;
        targetPoint = target;
    }

    private void Update()
    {
        timer += Time.deltaTime / lifeTime;
        if (timer >= 1f)
        {
            Destroy(gameObject);
            GameObject explosion1 = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Instantiate explosion effect
            Destroy(explosion1, 0.3f); 
            return;
        }

        // Parabola formula
        Vector2 pos = Vector2.Lerp(startPoint, targetPoint, timer);
        pos.y += height * Mathf.Sin(Mathf.PI * timer);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground")) // Check if the bomb collides with the player
        {

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Instantiate explosion effect
            Destroy(gameObject); // Destroy the bomb
        }
        

    }
}
