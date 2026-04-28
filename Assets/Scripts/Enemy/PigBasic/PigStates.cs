using UnityEngine;

public class PigPatrolState : EnemyState
{
    private int runAnimationHash = Animator.StringToHash("Run");
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(runAnimationHash);
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        PigBasic pig = (PigBasic)enemy;

        if (pig.PlayerInSight())
        {
            pig.ChangeState(new PigAttackState());
            return;
        }
        pig.DoPatrolMovement();
    }
}

public class PigIdleState : EnemyState
{
    private int idleAnimationHash = Animator.StringToHash("Idle");
    private float waitTimer;

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(idleAnimationHash);
        enemy.StopMoving();
        waitTimer = enemy.idleDuration;
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        PigBasic pig = (PigBasic)enemy;

        if (pig.PlayerInSight())
        {
            pig.ChangeState(new PigAttackState());
            return;
        }

        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            pig.ChangeState(new PigPatrolState());
        }
    }
}

public class PigAttackState : EnemyState
{
    private int attackAnimationHash = Animator.StringToHash("Attack");
    private float attackTimer = 1f;

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(attackAnimationHash);
        enemy.PerformAttack();
        enemy.StopMoving();
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            enemy.ChangeState(new PigPatrolState());
        }
    }
}

public class PigHitState : EnemyState
{
    private int hitAnimationHash = Animator.StringToHash("Hit");
    private float hitStunTimer = 0.5f;

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(hitAnimationHash);
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        hitStunTimer -= Time.deltaTime;
        if (hitStunTimer <= 0)
        {
            enemy.ChangeState(new PigPatrolState());
        }
    }
}

public class PigDeadState : EnemyState
{
    private int deadAnimationHash = Animator.StringToHash("Dead");
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(deadAnimationHash);
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
    }
}