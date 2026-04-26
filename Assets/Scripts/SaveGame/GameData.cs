using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public int levelID;
    public int highScore;
    public bool isUnlocked;

    // Constructor khởi tạo
    public LevelData(int id, bool unlocked = false)
    {
        levelID = id;
        highScore = 0; // Điểm mặc định là 0
        isUnlocked = unlocked;
    }
}

[System.Serializable]
public class GameData
{
    public List<LevelData> levels = new List<LevelData>();

    // Khởi tạo dữ liệu ban đầu khi người chơi mới tải game
    public GameData(int totalLevels)
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            // Chỉ màn 1 (i == 1) được gán giá trị isUnlocked = true
            levels.Add(new LevelData(i, i == 1));
        }
    }
}