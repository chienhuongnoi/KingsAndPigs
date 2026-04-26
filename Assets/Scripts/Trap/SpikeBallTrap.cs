using UnityEngine;

public class SpikeBallTrap : MonoBehaviour
{
    [Header("Cài đặt chuyển động")]
    [Tooltip("Góc lắc tối đa tính từ điểm giữa")]
    [SerializeField] private float swingAngle = 60f;

    [Tooltip("Tốc độ lắc (Càng to lắc càng nhanh)")]
    [SerializeField] private float swingSpeed = 2f;

    [Tooltip("Độ trễ nhịp ban đầu (Giúp nhiều quả cầu cạnh nhau không bị vung cùng một lúc)")]
    [SerializeField] private float timeOffset = 0f;

    [Header("Hiển thị (Chỉ thấy trong Editor)")]
    [Tooltip("Độ dài sợi xích để vẽ đường viền quỹ đạo cho chuẩn")]
    [SerializeField] private float gizmoChainLength = 3f;

    void Update()
    {
        float currentAngle = Mathf.Sin(Time.time * swingSpeed + timeOffset) * swingAngle;

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.15f);

        Vector3 leftExtents = transform.position + Quaternion.Euler(0, 0, swingAngle) * Vector3.down * gizmoChainLength;
        Vector3 rightExtents = transform.position + Quaternion.Euler(0, 0, -swingAngle) * Vector3.down * gizmoChainLength;

        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawLine(transform.position, leftExtents);
        Gizmos.DrawLine(transform.position, rightExtents);

        Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * gizmoChainLength);

        Gizmos.color = Color.yellow;
        int segments = 20;
        Vector3 previousPoint = leftExtents;

        for (int i = 1; i <= segments; i++)
        {
            float t = (float)i / segments;

            float currentStepAngle = Mathf.Lerp(swingAngle, -swingAngle, t);

            Vector3 nextPoint = transform.position + Quaternion.Euler(0, 0, currentStepAngle) * Vector3.down * gizmoChainLength;

            Gizmos.DrawLine(previousPoint, nextPoint);

            previousPoint = nextPoint;
        }
    }
}