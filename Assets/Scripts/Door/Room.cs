using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private CameraIntro cameraIntro;
    [SerializeField] private DoorIn doorIn;
    private void OnEnable()
    {
        cameraIntro.OnCameraFollowEnabled += OnCameraFollowEnabled;
    }
    private void OnDisable()
    {
        cameraIntro.OnCameraFollowEnabled -= OnCameraFollowEnabled;
    }
    private void OnCameraFollowEnabled()
    {

        doorIn.Open();
    }
}