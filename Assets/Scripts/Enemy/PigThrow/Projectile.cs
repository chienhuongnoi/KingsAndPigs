using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    [Header("Cài đặt vật lý")]
    [Tooltip("Độ cao tối đa chiếc hộp sẽ nảy lên so với điểm cao nhất")]
    public float arcHeight = 2.5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        // Lấy Rigidbody2D của Item Box
        rb = GetComponent<Rigidbody2D>();
    }

    // Gọi hàm này ngay khi Instantiate chiếc hộp
    public void Initialize(Vector2 startPos, Vector2 targetPos)
    {

        // Tính toán vận tốc (Velocity) cần thiết
        Vector2 launchVelocity = CalculateLaunchVelocity(startPos, targetPos, arcHeight);

        // Áp dụng thẳng vào Rigidbody
        // Dùng rb.linearVelocity thay vì AddForce để không bị ảnh hưởng bởi Mass (khối lượng) của hộp
        rb.linearVelocity = launchVelocity;
    }

    private Vector2 CalculateLaunchVelocity(Vector2 start, Vector2 target, float height)
    {
        // 1. Lấy gia tốc trọng trường thực tế đang tác động lên hộp (Luôn lấy số dương để dễ tính toán)
        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);

        // 2. Tính khoảng cách trục Y và trục X
        float displacementY = target.y - start.y;
        float displacementX = target.x - start.x;

        // Đảm bảo đỉnh parabol (h) luôn phải cao hơn vị trí đích để tránh lỗi toán học
        float h = Mathf.Max(height, displacementY + 0.5f);

        // 3. CÔNG THỨC VẬT LÝ KINEMATIC

        // Tính vận tốc ném lên trục Y (Vy = căn bậc 2 của 2 * g * h)
        float velocityY = Mathf.Sqrt(2 * gravity * h);

        // Tính thời gian bay từ lúc ném đến khi đạt đỉnh parabol
        float timeToApex = Mathf.Sqrt(2 * h / gravity);

        // Tính thời gian rơi từ đỉnh parabol xuống mục tiêu
        float timeToTarget = Mathf.Sqrt(2 * (h - displacementY) / gravity);

        // Tổng thời gian bay
        float totalTime = timeToApex + timeToTarget;

        // Tính vận tốc ném ngang trục X (Vx = Quãng đường / Tổng thời gian)
        float velocityX = displacementX / totalTime;

        // Trả về Vector vận tốc cuối cùng
        return new Vector2(velocityX, velocityY);
    }
}
