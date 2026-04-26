using UnityEngine;

public class LightingTheMatch : EnemyState //Pickup
{
    private float pickupTimer = 0.375f;
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play("LightingTheMatch");
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
    private float attackTimer = 1f;
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play("LightingTheCannon");
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

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play("MatchOn");
    }
    public override void LogicUpdate(EnemyBase enemy)
    {
        PigCannon pig = (PigCannon)enemy;

        // Nếu thấy Player -> Chuyển sang tấn công
        if (pig.PlayerInSight() && pig.canAttack)
        {
            pig.ChangeState(new LightingTheCannon());
            pig.canAttack = false;
            return;
        }
    }
}
public class PigCannonHitState : EnemyState
{
    private float hitStunTimer = 0.5f; // Thời gian bị choáng (khớp với độ dài animation Hit)

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play("Hit");
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        hitStunTimer -= Time.deltaTime;
        if (hitStunTimer <= 0)
        {
            // Hết choáng, kiểm tra quanh đó xem có ai không để đuổi, không thì đi tuần
            enemy.ChangeState(new MatchOn());
        }
    }
}
public class PigCannonDeadState : EnemyState
{
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play("Dead");
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        // Quái chết rồi không làm gì cả
    }
}
