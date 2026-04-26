using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CameraIntro : MonoBehaviour
{
    public Transform startPoint; // Vị trí xuất phát của Camera
    public Transform endPoint;   // Vị trí kết thúc intro
    public float duration = 3f;  // Thời gian di chuyển

    [Header("Scripts cần bật sau khi Intro xong")]
    public GameObject cameraFollow;
    public UnityAction OnCameraFollowEnabled;

    void Start()
    {
        if (cameraFollow != null)
        {
            cameraFollow.SetActive(false);
        }

        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
        StartCoroutine(MoveCameraToTarget());
    }

    IEnumerator MoveCameraToTarget()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.SmoothStep(0f, 1f, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPoint.position;

        if (cameraFollow != null)
        {
            cameraFollow.SetActive(true);
            OnCameraFollowEnabled?.Invoke();
        }

    }
}