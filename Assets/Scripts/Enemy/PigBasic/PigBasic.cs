using System;
using UnityEngine;

public class PigBasic : EnemyBase
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private bool movingLeft;
    private Vector3 initScale;

    protected override void Awake()
    {
        base.Awake();
        initScale = transform.localScale;
    }
    private void Start()
    {
        InitializeState(new PigPatrolState());
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
        ChangeState(new PigIdleState());
        movingLeft = !movingLeft;
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage, transform);
        }
    }
}