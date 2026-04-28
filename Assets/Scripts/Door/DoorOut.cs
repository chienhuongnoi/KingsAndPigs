using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoorOut : MonoBehaviour
{
    [SerializeField] private PlayerController1 player;
    public string nextSceneName;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        GameManager.Instance.nextSceneName = nextSceneName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController1>())
        {
            player.LockControl = true;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            animator.Play("DoorOut");
        }
    }

    public void PlayerDoorInEvent()
    {
        player.GetComponent<Animator>().Play("DoorIn");
    }

    public void TriggerNextRoom()
    {
        player.gameObject.SetActive(false);
        GameManager.Instance.LevelComplete(GameManager.Instance.currentLevelID, GameManager.Instance.playerScore);
    }
}