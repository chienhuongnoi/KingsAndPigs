using System.Collections;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Player Attack Settings")]
    [SerializeField] private float attackCooldown = 1f;

    [Header("Players Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;

    [Header("Detection")]//
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;//
    [Header("Components")]
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;
    [SerializeField] private LayerMask enemyLayer;

    private const float GroundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerHealth playerHealth;
    private KnockBack knockBack;
    private EnemyHealth enemyHealth;

    //Cache các chuỗi Animator thành Hash (int) để truy xuất nhanh hơn rất nhiều so với dùng string.
    private static readonly int IsJumpingHash = Animator.StringToHash("isJumping");
    private static readonly int IsFallingHash = Animator.StringToHash("isFalling");
    private static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    private static readonly int IsHittingHash = Animator.StringToHash("isHitting");
    private static readonly int AttackHash = Animator.StringToHash("attack");

    private WaitForSeconds attackWait;

    private float horizontalInput;
    private bool canAttack = true;
    public bool LockControl = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        knockBack = GetComponent<KnockBack>();
        playerHealth = GetComponent<PlayerHealth>();

        // Khởi tạo Cache
        attackWait = new WaitForSeconds(attackCooldown);
    }

    private void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, groundLayer);
        bool isFalling = rb.linearVelocity.y < -0.1f;
        bool isJumping = rb.linearVelocity.y > 0.1f;

        UpdateAnimator(isJumping, isFalling);

        if (knockBack.GettingKnockedBack || playerHealth.IsDead || LockControl) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        Flip();
        Jump(isGrounded);
        Attack();
        Move();
    }

    private void UpdateAnimator(bool isJumping, bool isFalling)
    {
        animator.SetBool(IsJumpingHash, isJumping);
        animator.SetBool(IsFallingHash, isFalling);
        animator.SetBool(IsRunningHash, horizontalInput != 0);
        animator.SetBool(IsHittingHash, knockBack.GettingKnockedBack);
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump(bool isGrounded)
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Attack()
    {
        if (canAttack && Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger(AttackHash);
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;
        yield return attackWait;
        canAttack = true;
    }

    private void Flip()
    {
        if (horizontalInput > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public bool EnemyInSight()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(capsuleCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(capsuleCollider2D.bounds.size.x * range, capsuleCollider2D.bounds.size.y, capsuleCollider2D.bounds.size.z),
         0, Vector2.left, 0, enemyLayer);
        bool hasDetected = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                    hasDetected = true;
                }


                Box box = hit.collider.GetComponent<Box>();
                if (box != null)
                {
                    box.TakeDamage(1, gameObject.transform);
                    hasDetected = true;
                }

            }
        }

        return hasDetected;
    }
    private void EnemyDamage()
    {
        if (EnemyInSight())
        {
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(capsuleCollider2D.bounds.size.x * range, capsuleCollider2D.bounds.size.y, capsuleCollider2D.bounds.size.z));
    }
}