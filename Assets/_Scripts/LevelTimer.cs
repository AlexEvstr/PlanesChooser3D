using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Text timerText;              // Текстовое поле для отображения времени
    public ResultManager resultManager; // Ссылка на ResultManager
    public PlaneManager planeManager;   // Ссылка на PlaneManager

    public int currentLevel = 1;        // Текущий уровень
    private float timer;                // Текущее время обратного отсчета
    private bool isRunning = false;

    private int targetPlaneCount = 0;   // Количество целевых самолетов, подсчитанных за уровень

    private void Start()
    {
        // Загружаем текущий уровень или устанавливаем 1 по умолчанию
        currentLevel = PlayerPrefs.GetInt("Level", 1);

        // Рассчитываем длительность уровня
        timer = currentLevel * 5f;

        // Обновляем отображение таймера
        //UpdateTimerText();
    }

    private void Update()
    {
        if (!isRunning) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            isRunning = false;
            EndLevel();
        }

        UpdateTimerText();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void IncrementTargetCount()
    {
        targetPlaneCount++;
    }

    private void UpdateTimerText()
    {
        timerText.text = $"Time: {Mathf.Ceil(timer)}";
        if (timer == 0)
        {
            timerText.text = "";
        }
    }

    private void EndLevel()
    {
        // Останавливаем спавн самолетов
        planeManager.CancelInvoke();

        // Показываем окно с результатами
        resultManager.ShowResultPanel(targetPlaneCount);

        
    }
}
