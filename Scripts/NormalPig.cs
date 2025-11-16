using UnityEngine;

public class NormalPig : PigBase
{
    private float idleTime = 1.5f;
    private float idleTimer = 0f;
    private bool isIdle = false;
    private bool isChasing = false;   
    private bool movingLeft = true;
    public float patrolDistance = 4f;
    private Vector2 startPos;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Transform attackZoneLeft;
    [SerializeField] private Transform attackZoneRight;
   
    private float attackCooldown = 2f;
    private float attackTimer = 0f;
    private bool canAttack = true;

    protected override void Start()
    {
        base.Start();
        startPos = transform.position; // Ghi nhớ vị trí bắt đầu
       

        attackCollider.enabled = false;
        
    }
    protected override void Update()
    {

        if (isDead) return;

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            canAttack = true;
        }

        DetectPlayer();
        PatrolLogic();

    }

    protected override void PatrolLogic()
    {   if(isChasing) return; // Nếu đang đuổi theo người chơi, không thực hiện logic tuần tra
        if (isIdle)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                isIdle = false;
                movingLeft = !movingLeft; // Đổi hướng sau khi idle
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isWalking", false);
                return;
            }
        }

        // Tính hướng đi
        float dir = movingLeft ? -1f : 1f;
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        animator.SetBool("isWalking", true);

        // Flip sprite
        //transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);
        if (dir < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Hướng trái
            if (attackCollider != null && attackZoneLeft != null)
                attackCollider.transform.position = attackZoneLeft.position;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Hướng phải
            if (attackCollider != null && attackZoneRight != null)
                attackCollider.transform.position = attackZoneRight.position;
        }

        // Kiểm tra đã đi đủ xa chưa
        float distanceFromStart = transform.position.x - startPos.x;
        if ((movingLeft && distanceFromStart <= -patrolDistance) || (!movingLeft && distanceFromStart >= patrolDistance))
        {
            isIdle = true;
            idleTimer = idleTime;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
    }
    protected override void DetectPlayer()
    {
        
        if(player != null) 
        {
            float distToPlayer = Vector2.Distance(player.position, transform.position);

            if (distToPlayer < detectionRange)
            {
                isChasing = true;

                float dir = player.position.x < transform.position.x ? -1f : 1f;
                rb.linearVelocity = new Vector2(dir * moveSpeed * 1.5f, rb.linearVelocity.y);
                animator.SetBool("isWalking", true);

                // Flip theo hướng player
                //transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);
                if (dir < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1); // Hướng trái
                    if (attackCollider != null && attackZoneLeft != null)
                        attackCollider.transform.position = attackZoneLeft.position;
                    //Debug.Log("Zoneattack on the leffside");
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1); // Hướng phải
                    if (attackCollider != null && attackZoneRight != null)
                        attackCollider.transform.position = attackZoneRight.position;
                    //Debug.Log("Zoneattack on the rightside");
                }
                if (distToPlayer < 1.3f && canAttack)
                {
                    Attack();
                }
            }

            else
            {
                isChasing = false; // Người chơi đã ra khỏi vùng phát hiện
            }
        }
        else 
        {
        isChasing = false; // Nếu không có người chơi, không đuổi theo
        }
        

    }
    private void Attack()
    {
        canAttack = false;
        attackTimer = attackCooldown;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Attack");
    }
    public void EnableAttackZone()
    {
        
            attackCollider.enabled = true;
    }

    public void DisableAttackZone()
    {
        
            attackCollider.enabled = false;
    }
    public override void TakeDamage(int dmg) 
    {
        currentHp -= dmg;
        animator.SetTrigger("Hurt"); // Gọi animation bị đánh
        if (currentHp <= 0)
        {
            Die();
        }
    }
}
