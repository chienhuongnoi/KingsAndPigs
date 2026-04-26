using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected float moveSpeed = 1.5f;
    [SerializeField] protected int damage = 1;
    [SerializeField] public float idleDuration = 1.5f;

    [Header("Detection")]
    [Tooltip("Chỉnh độ lệch trục X, Y của hộp kiểm tra so với tâm quái vật")]
    [SerializeField] protected Vector2 sightOffset = new Vector2(1f, 0f); // Thay thế range và colliderDistance cũ

    [Tooltip("Chỉnh chiều Rộng (X) và Cao (Y) của hộp kiểm tra")]
    [SerializeField] protected Vector2 sightSize = new Vector2(2f, 1.5f);

    [Header("Components")]
    [SerializeField] protected CapsuleCollider2D capsuleCollider2D;
    [SerializeField] protected LayerMask playerLayer;
    protected EnemyState currentState;
    protected EnemyHealth healthScript; // Tham chiếu đến script máu
    protected PlayerHealth playerHealth; // Tham chiếu đến script máu của Player
    protected Rigidbody2D rb;
    [HideInInspector] public Animator anim; // Thêm Animator vào đây

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthScript = GetComponent<EnemyHealth>();

        if (healthScript != null)
        {
            healthScript.OnTakeDamage += HandleHit;
            healthScript.OnDeath += HandleDeath;
        }
    }

    protected virtual void OnDestroy()
    {
        if (healthScript != null)
        {
            healthScript.OnTakeDamage -= HandleHit;
            healthScript.OnDeath -= HandleDeath;
        }
    }

    // Các hàm xử lý khi nhận được sự kiện
    protected virtual void HandleHit()
    {
        ChangeState(new PigHitState());
    }

    protected virtual void HandleDeath()
    {
        ChangeState(new PigDeadState());
    }

    // --- LOGIC STATE MACHINE ---

    protected void InitializeState(EnemyState startingState)
    {
        currentState = startingState;
        currentState.Enter(this);
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null) currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    protected virtual void Update()
    {
        if (currentState != null) currentState.LogicUpdate(this);
    }
    public virtual bool PlayerInSight()
    {
        Vector2 boxCenter = new Vector2(
            capsuleCollider2D.bounds.center.x + sightOffset.x * Mathf.Sign(transform.localScale.x),
            capsuleCollider2D.bounds.center.y + sightOffset.y
        );
        Collider2D hit = Physics2D.OverlapBox(boxCenter, sightSize, 0f, playerLayer);
        if (hit != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit != null;
    }

    public virtual void PerformAttack() { /* Logic gây sát thương */ }
    public void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
    void OnDrawGizmos()
    {
        if (capsuleCollider2D != null)
        {
            Gizmos.color = Color.red;

            // Tính toán lại vị trí vẽ Gizmo y hệt như trong hàm PlayerInSight
            Vector2 boxCenter = new Vector2(
                capsuleCollider2D.bounds.center.x + sightOffset.x * Mathf.Sign(transform.localScale.x),
                capsuleCollider2D.bounds.center.y + sightOffset.y
            );

            Gizmos.DrawWireCube(boxCenter, sightSize);
        }
    }
}