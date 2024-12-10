using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    public GameObject resultPanel;     // Панель с вариантами ответа
    public GameObject winPanel;       // Панель победы
    public GameObject losePanel;      // Панель поражения
    public Text[] answerButtons;      // Текстовые поля для кнопок ответов

    private int correctAnswer;        // Правильный ответ
    //private int targetCount;          // Количество целевых самолетов
    private System.Random random = new System.Random();

    public void ShowResultPanel(int counted)
    {
        StartCoroutine(WaitForAllPlanes(counted));
    }

    private IEnumerator WaitForAllPlanes(int counted)
    {
        yield return new WaitForSeconds(5.0f);
        correctAnswer = counted; // Правильный ответ — количество целевых самолетов

        // Генерация ответов
        int[] answers = GenerateAnswers(correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].text = answers[i].ToString(); // Отображаем ответы на кнопках
            int answer = answers[i]; // Копия для замыкания
            answerButtons[i].transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            answerButtons[i].transform.parent.GetComponent<Button>().onClick.AddListener(() => CheckAnswer(answer));
        }

        // Плавное открытие панели
        resultPanel.SetActive(true);
        StartCoroutine(ScaleResultPanel());
    }

    private IEnumerator ScaleResultPanel()
    {
        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;
        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime * 2; // Скорость увеличения
            resultPanel.transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }
    }

    private int[] GenerateAnswers(int correct)
    {
        Debug.Log(correctAnswer);
        int[] answers = new int[4];
        HashSet<int> uniqueAnswers = new HashSet<int>(); // Для проверки уникальности

        // Генерируем случайную позицию для правильного ответа
        int correctIndex = Random.Range(0, 4);
        answers[correctIndex] = correct; // Устанавливаем правильный ответ
        uniqueAnswers.Add(correct);

        for (int i = 0; i < answers.Length; i++)
        {
            if (i == correctIndex) continue; // Пропускаем индекс правильного ответа

            int randomAnswer;
            do
            {
                // Генерируем уникальные неправильные ответы
                randomAnswer = correct + Random.Range(-5, 6); // Диапазон значений
            }
            while (randomAnswer < 0 || uniqueAnswers.Contains(randomAnswer)); // Исключаем дубликаты и отрицательные значения

            answers[i] = randomAnswer;
            uniqueAnswers.Add(randomAnswer);
        }

        return answers;
    }



    private void CheckAnswer(int answer)
    {
        // Проверяем правильность ответа
        resultPanel.SetActive(false);

        if (answer == correctAnswer)
        {
            ShowWinPanel();
        }
        else
        {
            ShowLosePanel();
        }
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
        StartCoroutine(ScalePanel(winPanel.transform));
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        currentLevel++; // Увеличиваем уровень
        PlayerPrefs.SetInt("Level", currentLevel); // Сохраняем уровень
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true);
        StartCoroutine(ScalePanel(losePanel.transform));
    }

    private IEnumerator ScalePanel(Transform panel)
    {
        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = Vector3.one * 1.1f;
        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime * 2; // Скорость увеличения
            panel.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }
    }
}
