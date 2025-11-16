using UnityEngine;

public class PigBase : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the pig movement
    public float maxHp =  100f; // Maximum health of the pig
    protected float currentHp; // Current health of the pig
    protected bool isDead = false; // Flag to check if the pig is dead
    public GameObject deathPrefab; // Prefab to instantiate on death
    public Animator animator; // Reference to the Animator component
    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    public Transform player; // Target to follow
    public float detectionRange = 1f; // Range within which the pig will follow the player
    public Transform groundCheck;
    public LayerMask groundLayer;
    protected virtual void Start()
    {
        currentHp = maxHp; // Initialize current health to maximum health
    }

    
    protected virtual void Update()
    {
        if(isDead) return; // If the pig is dead, skip the update logic
        DetectPlayer(); // Check if the player is detected
        PatrolLogic(); // Execute patrol logic
    }
    protected bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    protected virtual void PatrolLogic() {  }

    protected virtual void DetectPlayer() { }

    public virtual void TakeDamage(int dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        Destroy(gameObject); // Destroy the pig game object
        GameObject deatheffect = Instantiate(deathPrefab, transform.position + Vector3.down * 0.3f, Quaternion.identity); // Instantiate death effect
        Destroy(deatheffect, 1.5f);

    }
}
