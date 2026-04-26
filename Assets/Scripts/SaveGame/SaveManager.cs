using System.IO;
using UnityEngine;

public static class SaveManager
{
    // Đường dẫn lưu file an toàn, không bị mất khi update game
    private static string saveFile => Application.persistentDataPath + "/savedata.json";

    public static void Save(GameData data)
    {
        // Chuyển đổi object thành chuỗi JSON (tham số true giúp file JSON format đẹp, dễ đọc lỗi)
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFile, json);
    }

    public static GameData Load(int totalLevels)
    {
        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            return JsonUtility.FromJson<GameData>(json);
        }

        // Nếu file chưa tồn tại (chơi lần đầu), tạo dữ liệu mới
        GameData newData = new GameData(totalLevels);
        Save(newData); // Tạo sẵn file gốc
        return newData;
    }
}