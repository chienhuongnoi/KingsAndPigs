using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Cài đặt Di chuyển")]
    [Tooltip("Vị trí điểm A")]
    [SerializeField] private Transform pointA;

    [Tooltip("Vị trí điểm B")]
    [SerializeField] private Transform pointB;

    [Tooltip("Lưỡi cưa")]
    [SerializeField] private GameObject sawBlade;

    [Tooltip("Tốc độ di chuyển của lưỡi cưa")]
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 currentTarget;

    private void Start()
    {
        if (pointB != null)
        {
            currentTarget = pointB.position;
        }
    }

    private void Update()
    {
        if (pointA == null || pointB == null) return;


        sawBlade.transform.position = Vector3.MoveTowards(sawBlade.transform.position, currentTarget, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(sawBlade.transform.position, currentTarget) < 0.01f)
        {
            if (currentTarget == pointB.position)
            {
                currentTarget = pointA.position;
            }
            else
            {
                currentTarget = pointB.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            // Vẽ 2 điểm tròn nhỏ ở A và B
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);

            // Vẽ 1 đường thẳng màu đỏ nối giữa 2 điểm
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}