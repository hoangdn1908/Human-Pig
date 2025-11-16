using UnityEngine;

public class PigThrowTheBox : PigBase
{
    private float idleTime = 2f;
    private float idleTimer = 0f;
    private bool isIdle = false;
    private bool movingLeft = true;
    public float patrolDistance = 4f;
    private Vector2 startPos;
    private bool playerInRange = false;
    private float throwCooldown = 2f;
    private float throwTimer = 0f;
    public GameObject boxPrefab;
    public Transform throwPoint;

    protected override void Start()
    {
        base.Start();
        startPos = transform.position; // Remember the starting position
    }
    protected override void Update()
    {
        if (isDead) return; // If the pig is dead, skip the update logic
           
        PatrolLogic(); // Execute patrol logic
        ThrowBox();

    }
    protected override void PatrolLogic()
    {
        if (isIdle)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                isIdle = false;
                movingLeft = !movingLeft;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isWalking", false);
                return;
            }
        }

        float dir = movingLeft ? -1f : 1f;
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        animator.SetBool("isWalking", true);

        // Flip hướng
        transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);

        // Check khoảng cách tuần tra
        float distanceFromStart = transform.position.x - startPos.x;
        if ((movingLeft && distanceFromStart <= -patrolDistance) || (!movingLeft && distanceFromStart >= patrolDistance))
        {
            isIdle = true;
            idleTimer = idleTime;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
    }
    private void ThrowBox()
    {
        throwTimer -= Time.deltaTime;

        if (playerInRange)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            // Flip sprite đúng hướng người chơi
            float dir = player.position.x < transform.position.x ? -1f : 1f;
            transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);

            if (throwTimer <= 0f)
            {
                animator.SetTrigger("Throw"); // Phát animation ném
                throwTimer = throwCooldown;
            }
        }
        else
        {
            PatrolLogic(); // Nếu không có người chơi trong phạm vi, thực hiện logic tuần tra
        }
    }

    public void SetPlayerInRange(bool value) 
    {
        playerInRange = value;
    }
    public void SpawnBox()
    {
        if (boxPrefab != null && throwPoint != null && player != null)
        {
            GameObject box = Instantiate(boxPrefab, throwPoint.position, Quaternion.identity);
            Box projectile = box.GetComponent<Box>();
            if (projectile != null)
            {
                projectile.Launch(player.position); // Bay về phía người chơi
            }
        }
    }
}
