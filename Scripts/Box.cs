using UnityEngine;

public class Box : MonoBehaviour
{
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
            return;
        }

        // Parabola formula
        Vector2 pos = Vector2.Lerp(startPoint, targetPoint, timer);
        pos.y += height * Mathf.Sin(Mathf.PI * timer);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Box hit ");
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(20); 
            }
                Destroy(gameObject); // Vỡ hộp
        }
        if (other.CompareTag("Ground")) 
        {
            Destroy(gameObject); // Vỡ hộp khi chạm đất
        }
    }
}
