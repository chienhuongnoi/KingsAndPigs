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
            // UnlockNewLevel();
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
    // public void UnlockNewLevel()
    // {
    //     if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex", 1))
    //     {
    //         PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
    //         PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
    //         PlayerPrefs.Save();
    //     }
    // }
}