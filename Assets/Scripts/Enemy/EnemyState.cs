public abstract class EnemyState
{
    // Gọi khi bắt đầu vào trạng thái
    public virtual void Enter(EnemyBase enemy) { }

    // Gọi liên tục mỗi frame 
    public virtual void LogicUpdate(EnemyBase enemy) { }

    // Gọi khi thoát khỏi trạng thái
    public virtual void Exit(EnemyBase enemy) { }
}
