using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject uiLevelCompleteMenu;
    public GameObject uiPauseMenu;
    public GameObject uiGameOverMenu;
    public TextMeshProUGUI scoreText;
    [SerializeField] private GameObject uiInGame;
    [SerializeField] private List<GameObject> hpBarList = new List<GameObject>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            if (uiInGame != null) uiInGame.SetActive(false);
        }
        else if (scene.name.StartsWith("Level"))
        {
            if (uiInGame != null) uiInGame.SetActive(true);
        }
        uiPauseMenu.SetActive(false);
        uiGameOverMenu.SetActive(false);
        uiLevelCompleteMenu.SetActive(false);
        GameManager.Instance.currentLevelID = scene.buildIndex;
        GameManager.Instance.ResetScore();
    }

    public void UpdateHpBar(int currentHealth)
    {
        for (int i = 0; i < hpBarList.Count; i++)
        {
            if (hpBarList[i] == null)
            {
                continue;
            }

            if (i < currentHealth)
            {
                hpBarList[i].SetActive(true);
            }
            else
            {
                hpBarList[i].SetActive(false);
            }
        }
    }
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}