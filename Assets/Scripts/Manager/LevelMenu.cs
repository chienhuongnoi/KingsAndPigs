using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class LevelMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform levelButtonsContainer;

    [SerializeField] private Button[] buttons;

    private void Start()
    {
        InitializeButtons();
        UpdateLevelStatus();
    }

    private void InitializeButtons()
    {
        int childCount = levelButtonsContainer.childCount;
        buttons = new Button[childCount];

        for (int i = 0; i < childCount; i++)
        {
            int levelID = i + 1;
            buttons[i] = levelButtonsContainer.GetChild(i).GetComponent<Button>();

            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OpenLevel(levelID));
        }
    }

    public void UpdateLevelStatus()
    {
        // Lấy dữ liệu game hiện tại từ GameManager (Hệ thống JSON ta đã viết trước đó)
        GameData currentData = GameManager.Instance.currentData;

        for (int i = 0; i < buttons.Length; i++)
        {
            int levelID = i + 1;

            // Tìm data của level này trong file Save
            LevelData levelData = currentData.levels.FirstOrDefault(l => l.levelID == levelID);

            if (levelData != null)
            {
                // Mở khóa hay không dựa vào file JSON
                buttons[i].interactable = levelData.isUnlocked;

                /* [GỢI Ý THÊM] 
                Nếu các nút Level của bạn có text để hiện điểm HighScore, bạn có thể gọi ở đây:
                Text scoreText = buttons[i].transform.GetChild(0).GetComponent<Text>();
                scoreText.text = "Score: " + levelData.highScore;
                */
            }
            else
            {
                // Nếu chưa có data thì khóa lại phòng lỗi
                buttons[i].interactable = false;
            }
        }
    }

    public void OpenLevel(int levelIndex)
    {
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    }
}