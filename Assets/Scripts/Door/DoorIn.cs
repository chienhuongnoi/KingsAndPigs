using System;
using UnityEngine;
using UnityEngine.Events;

public class DoorIn : MonoBehaviour
{
    [SerializeField] private PlayerController1 player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.Play("DoorIn");
    }
    public void PlayerDoorOutEvent()
    {
        player.GetComponent<Animator>().Play("DoorOut");
    }

    public void PlaceKing()
    {
        player.transform.position = gameObject.transform.GetChild(0).position; // Đặt King tại spawn point của DoorIn
        player.gameObject.SetActive(true); // Kích hoạt King nếu đang bị vô hiệu hóa
        player.LockControl = false;

    }
}