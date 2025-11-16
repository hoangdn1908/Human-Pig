using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player movement
    [SerializeField] private float jumpForce = 5f; // Force applied when jumping
    [SerializeField] private LayerMask groundLayer; // Layer to check for ground collision
    [SerializeField] private Transform groundCheck; // Transform to check if the player is grounded
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground check
    [SerializeField] private GameObject humanDeathPrefab;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float maxHp = 100f; // Maximum health of the player
    private float currentHp; // Current health of the player
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isGrounded; // Flag to check if the player is on the ground
    private Animator animator; // Reference to the Animator component
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = maxHp; // Initialize current health to maximum health
        attackCollider.enabled = false; // Disable attack collider initially
    }


    void Update()
    {
        Movement(); // Handle player movement
        Jump(); // Handle jumping
        Attack(); // Handle attacking
    }
    private void Movement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
        animator.SetBool("isWalking", Mathf.Abs(moveInput) > 0.1f); // Set animator speed parameter
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage; // Reduce current health by damage amount
        if (currentHp <= 0)
        {
            Die(); // Call Die method if health is zero or below
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isJumping", !isGrounded); // Update animator jumping state

    }
    private void Attack() 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
        animator.SetTrigger("Attack"); // Trigger attack animation
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        Instantiate(humanDeathPrefab, transform.position + Vector3.down * 0.3f, Quaternion.identity);
    }
   
    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }
}