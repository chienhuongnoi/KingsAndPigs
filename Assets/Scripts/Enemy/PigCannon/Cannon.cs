using UnityEngine;

public class Cannon : MonoBehaviour
{
    // public GameObject projectilePrefab; // Prefab của viên đạn
    public Transform firePoint; // Điểm bắn 
    [SerializeField]
    private PigCannon pigCannon;
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
    public void SpawnProjectile()
    {
        // GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        GameObject projectile = ObjectPool.Instance.GetObjectFromPool("ball");
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        Vector2 direction = (pigCannon.PlayerPosition() - firePoint.position).normalized;

        CannonBall projectileScript = projectile.GetComponent<CannonBall>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(direction, shootForce);
        }
    }
}
