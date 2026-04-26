using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public string nextSceneName;
    public int currentLevelID;
    public int playerScore;
    public GameData currentData;
    public int totalLevelsInGame = 6;
    protected override void Awake()
    {
        base.Awake();
        currentData = SaveManager.Load(totalLevelsInGame);
    }
    void Start()
    {
        Debug.Log("Đường dẫn file save: " + Application.persistentDataPath);
    }
    public void PauseGame()
    {
        UIManager.Instance.uiPauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        UIManager.Instance.uiPauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        UIManager.Instance.uiGameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(this.nextSceneName);
    }
    public void LevelComplete(int levelID, int score)
    {
        LevelData currentLevel = currentData.levels.FirstOrDefault(l => l.levelID == levelID);
        if (currentLevel != null)
        {
            // So sánh và cập nhật điểm cao nhất
            if (score > currentLevel.highScore)
            {
                currentLevel.highScore = score;
                Debug.Log($"Kỷ lục mới ở màn {levelID}: {score} điểm!");
            }
        }
        LevelData nextLevel = currentData.levels.FirstOrDefault(l => l.levelID == levelID + 1);
        if (nextLevel != null && !nextLevel.isUnlocked)
        {
            nextLevel.isUnlocked = true;
            Debug.Log($"Đã mở khóa màn {levelID + 1}!");
        }
        SaveManager.Save(currentData);
        UIManager.Instance.uiLevelCompleteMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void AddScore(int amount)
    {
        playerScore += amount;
        UIManager.Instance.UpdateScore(playerScore);
    }
    public void ResetScore()
    {
        playerScore = 0;
        UIManager.Instance.UpdateScore(playerScore);
    }
}
