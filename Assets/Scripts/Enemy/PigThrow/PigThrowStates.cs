using UnityEngine;
public class PigThrowPatrolState : EnemyState
{
    private int runAnimationHash = Animator.StringToHash("Run");

    public override void Enter(EnemyBase enemy)
    {
        // Phát animation Run (giống hình bạn gửi)
        enemy.anim.Play(runAnimationHash);
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        PigThrow pig = (PigThrow)enemy;

        // // Nếu thấy Player -> Chuyển sang rượt đuổi
        if (pig.PlayerInSight())
        {
            pig.ChangeState(new PigThrowAttackState());
            return;
        }
        // Di chuyển tuần tra
        pig.DoPatrolMovement();
    }
}
public class PigThrowIdleState : EnemyState
{
    private int idleAnimationHash = Animator.StringToHash("Idle");
    private float waitTimer; // Đứng nghỉ 1.5 giây

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(idleAnimationHash); // Phát animation Idle
        enemy.StopMoving(); // Dừng di chuyển khi vào trạng thái nghỉ
        waitTimer = enemy.idleDuration;
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        PigThrow pig = (PigThrow)enemy;

        // Nếu thấy Player -> Chuyển sang tấn công
        if (pig.PlayerInSight())
        {
            pig.ChangeState(new PigThrowAttackState());
            return;
        }

        // Đếm ngược thời gian nghỉ
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            // Hết thời gian nghỉ, quay lại tuần tra
            pig.ChangeState(new PigThrowPatrolState());
        }
    }
}
public class PigThrowAttackState : EnemyState
{
    private int throwAnimationHash = Animator.StringToHash("Throw");
    private float attackTimer = 1f;

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(throwAnimationHash);
        enemy.PerformAttack();
        enemy.StopMoving(); // Dừng di chuyển khi tấn công
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            enemy.ChangeState(new PigThrowPickupState());
        }
    }
}
public class PigThrowPickupState : EnemyState
{
    private int pickupAnimationHash = Animator.StringToHash("Pickup");
    private float pickupTimer = 0.75f; // Thời gian đợi animation nhặt xong (chỉnh theo thực tế)
    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(pickupAnimationHash);
        // Sau khi nhặt xong sẽ có thể ném, nên không cần làm gì thêm ở đây
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        // Chờ animation Pickup kết thúc, sau đó sẽ tự động chuyển sang trạng thái tấn công để ném
        pickupTimer -= Time.deltaTime;
        if (pickupTimer <= 0)
        {
            enemy.ChangeState(new PigThrowPatrolState());
        }
    }
}
public class PigThrowHitState : EnemyState
{
    private int hitAnimationHash = Animator.StringToHash("Hit");
    private float hitStunTimer = 0.5f; // Thời gian bị choáng (khớp với độ dài animation Hit)

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play(hitAnimationHash);
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        hitStunTimer -= Time.deltaTime;
        if (hitStunTimer <= 0)
        {
            // Hết choáng, kiểm tra quanh đó xem có ai không để đuổi, không thì đi tuần
            enemy.ChangeState(new PigThrowPatrolState());
        }
    }
}
public class PigThrowDeadState : EnemyState
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
