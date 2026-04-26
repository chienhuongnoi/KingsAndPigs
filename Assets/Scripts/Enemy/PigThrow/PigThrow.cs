using UnityEngine;

public class PigThrow : EnemyBase
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
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

        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + Time.deltaTime * _direction * moveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
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
        Projectile projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Initialize(spawnPoint.position, playerHealth.transform.position);
    }
}
