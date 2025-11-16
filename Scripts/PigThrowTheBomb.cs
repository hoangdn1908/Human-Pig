using UnityEngine;

public class PigThrowTheBomb : PigBase
{
    private float idleTime = 2f;
    private float idleTimer = 0f;
    private bool isIdle = false;
    private bool movingLeft = true;
    public float patrolDistance = 4f;
    private Vector2 startPos;
    private bool playerInRange = false;
    private float throwCooldown = 1f; // Thời gian hồi chiêu ném bom
    private float throwTimer = 0f; // Bộ đếm thời gian cho việc ném bom
    [SerializeField] private GameObject bombPrefabs; // Prefab của bom
    [SerializeField] private Transform throwPoint; // Vị trí ném bom
    protected override void Start()
    {
        base.Start();
        startPos = transform.position; // Remember the starting position
    }
    protected override void Update()
    {
        if (isDead) return; // If the pig is dead, skip the update logic
            PatrolLogic(); // Execute patrol logic
            ThrowBomb(); // Thực hiện logic ném bom 



    }
    protected override void PatrolLogic() 
    {
        if (isIdle) 
        {
            idleTimer -= Time.deltaTime;
            if(idleTimer <= 0f) 
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
        transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);
        float distanceFromStart = transform.position.x - startPos.x;
        if ((movingLeft && distanceFromStart <= -patrolDistance) || (!movingLeft && distanceFromStart >= patrolDistance)) 
        {
            isIdle = true;
            idleTimer = idleTime;
            rb.linearVelocity = Vector2.zero; // Dừng lại khi đến cuối khoảng cách tuần tra
            animator.SetBool("isWalking", false);
        }
    }
    private void ThrowBomb() 
    {
        throwTimer -= Time.deltaTime;
        if (playerInRange && player!= null) 
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            // Flip sprite đúng hướng người chơi
            float dir = player.position.x < transform.position.x ? -1f : 1f;
            transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);
            if (throwTimer <= 0f) 
            {
                throwTimer = throwCooldown; // Reset bộ đếm thời gian ném bom
                animator.SetTrigger("Throw"); // Phát animation ném
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
    private void SpawnBomb() 
    {
        if (bombPrefabs != null && throwPoint != null && player != null) 
        {
            GameObject bomb = Instantiate(bombPrefabs, throwPoint.position, Quaternion.identity);
            Bomb bombScript = bomb.GetComponent<Bomb>();
            if (bombScript != null) 
            {
               
                bombScript.Launch(player.position); // Gọi phương thức Launch để ném bom
            }
        }
    }
}
