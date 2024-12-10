using UnityEngine;
using UnityEngine.UI;

public class MenuLevelController : MonoBehaviour
{
    public Text levelText;        // Текстовое поле для отображения текущего уровня
    public int currentLevel = 1;  // Текущий уровень
    public int bestLevel = 1;     // Лучший уровень

    private const string LevelKey = "Level";
    private const string BestLevelKey = "BestLevel";

    private void Start()
    {
        // Загружаем текущий уровень и лучший уровень из PlayerPrefs
        currentLevel = PlayerPrefs.GetInt(LevelKey, 1);
        bestLevel = PlayerPrefs.GetInt(BestLevelKey, 1);

        UpdateLevelText();
    }

    public void IncreaseLevel()
    {
        if (currentLevel < bestLevel) // Увеличиваем уровень только до лучшего уровня
        {
            currentLevel++;
            SaveLevel();
        }
    }

    public void DecreaseLevel()
    {
        if (currentLevel > 1) // Не уменьшаем ниже 1
        {
            currentLevel--;
            SaveLevel();
        }
    }

    private void SaveLevel()
    {
        PlayerPrefs.SetInt(LevelKey, currentLevel); // Сохраняем текущий уровень
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        levelText.text = $"{currentLevel}";
    }

    // Вызывается где-то в игре при достижении нового лучшего уровня
    public void SaveBestLevel(int newBestLevel)
    {
        if (newBestLevel > bestLevel)
        {
            bestLevel = newBestLevel;
            PlayerPrefs.SetInt(BestLevelKey, bestLevel); // Сохраняем лучший уровень
        }
    }
}
