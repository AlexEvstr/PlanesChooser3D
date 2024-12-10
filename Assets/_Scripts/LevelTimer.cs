using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Text timerText;
    public ResultManager resultManager;
    public PlaneManager planeManager;

    public int currentLevel = 1;
    private float timer;
    private bool isRunning = false;

    private int targetPlaneCount = 0;

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 1);
        timer = currentLevel * 5f;
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
        planeManager.CancelInvoke();

        resultManager.ShowResultPanel(targetPlaneCount);    
    }
}