using UnityEngine;

// --- TRẠNG THÁI ĐI TUẦN ---
public class PigPatrolState : EnemyState
{

    public override void Enter(EnemyBase enemy)
    {
        // Phát animation Run (giống hình bạn gửi)
        enemy.anim.Play("Run");
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        PigBasic pig = (PigBasic)enemy;

        // // Nếu thấy Player -> Chuyển sang rượt đuổi
        if (pig.PlayerInSight())
        {
            pig.ChangeState(new PigAttackState());
            return;
        }
        // Di chuyển tuần tra
        pig.DoPatrolMovement();
    }
}

// --- TRẠNG THÁI NGHỈ NGƠI (IDLE) ---
public class PigIdleState : EnemyState
{
    private float waitTimer; // Đứng nghỉ 1.5 giây

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play("Idle"); // Phát animation Idle
        enemy.StopMoving(); // Dừng di chuyển khi vào trạng thái nghỉ
        waitTimer = enemy.idleDuration;
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        PigBasic pig = (PigBasic)enemy;

        // Nếu thấy Player -> Chuyển sang tấn công
        if (pig.PlayerInSight())
        {
            pig.ChangeState(new PigAttackState());
            return;
        }

        // Đếm ngược thời gian nghỉ
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            // Hết thời gian nghỉ, quay lại tuần tra
            pig.ChangeState(new PigPatrolState());
        }
    }
}

// --- TRẠNG THÁI TẤN CÔNG ---
public class PigAttackState : EnemyState
{
    private float attackTimer = 1f; // Thời gian đợi animation đánh xong (chỉnh theo thực tế)

    public override void Enter(EnemyBase enemy)
    {
        enemy.anim.Play("Attack");
        enemy.PerformAttack();
        enemy.StopMoving(); // Dừng di chuyển khi tấn công
    }

    public override void LogicUpdate(EnemyBase enemy)
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            // Đánh xong, nếu Player vẫn ở trong tầm thì nó sẽ tự động chạy lại ChaseState và đánh tiếp
            enemy.ChangeState(new PigPatrolState());
        }
    }
}

// --- TRẠNG THÁI BỊ ĐÁNH TRÚNG ---
public class PigHitState : EnemyState
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
            enemy.ChangeState(new PigPatrolState());
        }
    }
}

// --- TRẠNG THÁI CHẾT ---
public class PigDeadState : EnemyState
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