using System;
using UnityEngine;

public class PigCannon : EnemyBase
{
    public float attackDelay = 5f;
    public bool canAttack = true;
    public Action OnAttack;
    private float attackCooldownTimer;
    protected override void Awake()
    {
        base.Awake();
        ChangeState(new MatchOn());
    }
    protected override void Update()
    {
        base.Update();
        attackCooldownTimer += Time.deltaTime;
        if (attackCooldownTimer >= attackDelay)
        {
            canAttack = true;
            attackCooldownTimer = 0f;
        }
    }
    protected override void HandleHit()
    {
        ChangeState(new PigCannonHitState());
    }
    protected override void HandleDeath()
    {
        ChangeState(new PigCannonDeadState());
    }
    private void Attack()
    {
        OnAttack?.Invoke();
    }
    public Vector3 PlayerPosition()
    {
        return playerHealth.transform.position;
    }
}
