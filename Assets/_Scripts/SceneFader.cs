using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float progress = 1f;
        while (progress > 0)
        {
            progress -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, progress);
            yield return null;
        }
    }

    public IEnumerator FadeOut(System.Action onComplete)
    {
        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, progress);
            yield return null;
        }
        onComplete?.Invoke();
    }

    public void LoadNewScene(string menuSceneName)
    {
        StartCoroutine(FadeOut(() =>
        {
            SceneManager.LoadScene(menuSceneName);
        }));
    }
}