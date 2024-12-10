using System.Collections;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject tutorial;       // Объект туториала
    public RectTransform tutorialChild; // Дочерний объект, который будет масштабироваться
    public float animationDuration = 0.5f; // Длительность анимации

    // Публичный метод для включения туториала
    public void ShowTutorial()
    {
        tutorial.SetActive(true); // Включаем объект туториал
        StartCoroutine(ScaleObject(tutorialChild, Vector3.zero, Vector3.one)); // Увеличиваем дочерний объект
    }

    // Публичный метод для уменьшения и выключения туториала
    public void HideTutorial()
    {
        StartCoroutine(ScaleObject(tutorialChild, Vector3.one, Vector3.zero, () =>
        {
            tutorial.SetActive(false); // Выключаем объект туториал после анимации
        }));
    }

    // Анимация изменения масштаба
    private IEnumerator ScaleObject(RectTransform target, Vector3 startScale, Vector3 endScale, System.Action onComplete = null)
    {
        float progress = 0;

        // Начальное состояние
        target.localScale = startScale;

        while (progress < 1)
        {
            progress += Time.deltaTime / animationDuration;
            target.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        // Финальное состояние
        target.localScale = endScale;
        onComplete?.Invoke(); // Вызываем действие после завершения анимации
    }
}
