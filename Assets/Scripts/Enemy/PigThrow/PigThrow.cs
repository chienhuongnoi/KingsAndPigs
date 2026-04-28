using UnityEngine;

public class PigThrow : EnemyBase
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Projectile")]
    [SerializeField] private string projectileTag;
    [SerializeField] private Transform spawnPoint;
    private bool movingLeft;
    private Vector3 initScale;
    protected override void Awake()
    {
        base.Awake();
        initScale = transform.localScale;
    }
    private void Start()
    {
        InitializeState(new PigThrowPatrolState());
    }
    public void DoPatrolMovement()
    {
        if (movingLeft)
        {
            if (gameObject.transform.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (gameObject.transform.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }


    public void MoveInDirection(int _direction)
    {
        gameObject.transform.localScale = new Vector3(-Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        rb.linearVelocity = new Vector2(_direction * moveSpeed, rb.linearVelocity.y);
    }
    private void DirectionChange()
    {
        ChangeState(new PigThrowIdleState());
        movingLeft = !movingLeft;
    }
    protected override void HandleHit()
    {
        ChangeState(new PigThrowHitState());
    }
    protected override void HandleDeath()
    {
        ChangeState(new PigThrowDeadState());
    }

    [System.Obsolete]
    private void InstantiateObject()
    {
        // Projectile projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        Projectile projectile = ObjectPool.Instance.GetObjectFromPool(projectileTag).GetComponent<Projectile>();
        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = spawnPoint.rotation;
        projectile.Initialize(spawnPoint.position, playerHealth.transform.position);
    }
}
