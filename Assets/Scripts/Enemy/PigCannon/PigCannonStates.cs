using UnityEngine;

public class LightingTheMatch : EnemyState //Pickup
{
    private int pickupAnimationHash = Animator.StringToHash("LightingTheMatch");
    private float pickupTimer = 0.375f;
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(pickupAnimationHash);
    }
    public override void LogicUpdate(EnemyBase enemy)
    {
        pickupTimer -= Time.deltaTime;
        if (pickupTimer <= 0)
        {
            enemy.ChangeState(new MatchOn());
        }
    }
}
public class LightingTheCannon : EnemyState //Attack
{
    private int attackAnimationHash = Animator.StringToHash("LightingTheCannon");
    private float attackTimer = 1f;
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(attackAnimationHash);
    }
    public override void LogicUpdate(EnemyBase enemy)
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            enemy.ChangeState(new LightingTheMatch());
        }
    }
}
public class MatchOn : EnemyState //Idle
{
    private int matchOnAnimationHash = Animator.StringToHash("MatchOn");
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(matchOnAnimationHash);
    }
    public override void LogicUpdate(EnemyBase enemy)
    {
        PigCannon pig = (PigCannon)enemy;

        if (pig.PlayerInSight() && pig.canAttack)
        {
            pig.canAttack = false;
            pig.ChangeState(new LightingTheCannon());
            return;
        }
    }
}
public class PigCannonHitState : EnemyState
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
            enemy.ChangeState(new MatchOn());
        }
    }
}
public class PigCannonDeadState : EnemyState
{
    private int deadAnimationHash = Animator.StringToHash("Dead");
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(deadAnimationHash);
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        // Quái chết rồi không làm gì cả
    }
}
