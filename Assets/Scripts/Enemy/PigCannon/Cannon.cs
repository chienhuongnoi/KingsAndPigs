using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab của viên đạn
    public Transform firePoint; // Điểm bắn (có thể là một child transform)
    [SerializeField]
    private PigCannon pigCannon; // Tham chiếu đến script PigCannon
    [Header("Chỉ số súng")]
    public float shootForce = 25f;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        pigCannon.OnAttack += Fire; // Đăng ký sự kiện tấn công
    }
    private void OnDisable()
    {
        pigCannon.OnAttack -= Fire; // Hủy đăng ký sự kiện khi không cần thiết
    }

    private void Fire()
    {
        animator.SetTrigger("Shoot");
    }
    // Hàm này sẽ được gọi từ animation event ở khung hình bắn
    public void SpawnProjectile()
    {
        // Instantiate viên đạn tại vị trí firePoint và với rotation của firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (pigCannon.PlayerPosition() - firePoint.position).normalized;

        // Lấy script Projectile từ viên đạn mới tạo
        CannonBall projectileScript = projectile.GetComponent<CannonBall>();

        if (projectileScript != null)
        {
            // Gọi hàm Initialize để thiết lập vận tốc cho viên đạn
            projectileScript.Initialize(direction, shootForce);
        }
    }
}
