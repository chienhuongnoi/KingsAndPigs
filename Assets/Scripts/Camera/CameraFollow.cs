using UnityEngine;
using UnityEngine.Events;

public class CameraFollow : MonoBehaviour
{
    [Header("Mục tiêu bám theo (Player)")]
    public Transform target;

    [Header("Cài đặt Camera")]
    public Vector3 offset;

    public float smoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;
    public UnityAction OnCameraFollowEnabled;

    void Start()
    {

        if (target != null && offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
        OnCameraFollowEnabled?.Invoke();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}