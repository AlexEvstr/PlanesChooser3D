using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    public GameObject resultPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public Text[] answerButtons;

    [SerializeField] private AudioController _audioController;

    private int correctAnswer;
    private System.Random random = new System.Random();

    public void ShowResultPanel(int counted)
    {
        StartCoroutine(WaitForAllPlanes(counted));
    }

    private IEnumerator WaitForAllPlanes(int counted)
    {
        yield return new WaitForSeconds(5.0f);
        correctAnswer = counted;

        int[] answers = GenerateAnswers(correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].text = answers[i].ToString();
            int answer = answers[i];
            answerButtons[i].transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            answerButtons[i].transform.parent.GetComponent<Button>().onClick.AddListener(() => CheckAnswer(answer));
        }

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
            progress += Time.deltaTime * 2;
            resultPanel.transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }
    }

    private int[] GenerateAnswers(int correct)
    {
        Debug.Log(correctAnswer);
        int[] answers = new int[4];
        HashSet<int> uniqueAnswers = new HashSet<int>();

        int correctIndex = Random.Range(0, 4);
        answers[correctIndex] = correct;
        uniqueAnswers.Add(correct);

        for (int i = 0; i < answers.Length; i++)
        {
            if (i == correctIndex) continue;

            int randomAnswer;
            do
            {
                randomAnswer = correct + Random.Range(-5, 6);
            }
            while (randomAnswer < 0 || uniqueAnswers.Contains(randomAnswer));

            answers[i] = randomAnswer;
            uniqueAnswers.Add(randomAnswer);
        }

        return answers;
    }

    private void CheckAnswer(int answer)
    {
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
        _audioController.StopMusic();
        _audioController.WinSound();
        winPanel.SetActive(true);
        StartCoroutine(ScalePanel(winPanel.transform));

        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);

        int bestLevel = PlayerPrefs.GetInt("BestLevel", 1);
        if (currentLevel > bestLevel)
        {
            PlayerPrefs.SetInt("BestLevel", currentLevel);
        }

        _audioController.Win2Sound();
    }


    private void ShowLosePanel()
    {
        _audioController.StopMusic();
        _audioController.LoseSound();
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
            progress += Time.deltaTime * 2;
            panel.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }
    }
}