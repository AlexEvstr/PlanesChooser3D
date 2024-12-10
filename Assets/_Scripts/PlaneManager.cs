using System.Collections;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public GameObject[] descriptionPlanes; // Самолеты для окна описания
    public GameObject[] gamePlanes;       // Самолеты для игрового процесса
    private float spawnInterval = 0.5f;      // Интервал спавна самолетов
    [SerializeField] private GameObject _preGame;
    [SerializeField] private GameObject _count;
    public LevelTimer levelTimer;

    private int targetIndex; // Индекс самолета, который нужно считать

    private void Start()
    {
        // Выбираем цель и включаем соответствующий самолет в окне описания
        SetTargetPlane();
    }

    private void SetTargetPlane()
    {
        // Случайно выбираем индекс самолета
        targetIndex = Random.Range(0, descriptionPlanes.Length);

        // Включаем самолет с этим индексом в массиве descriptionPlanes
        for (int i = 0; i < descriptionPlanes.Length; i++)
        {
            descriptionPlanes[i].SetActive(i == targetIndex);
        }

        Debug.Log($"Target plane for description is: {descriptionPlanes[targetIndex].name}");
    }

    public void StartSpawning()
    {
        // Запускаем спавн самолетов
        InvokeRepeating(nameof(SpawnPlane), 0, spawnInterval);

        // Запускаем таймер
        levelTimer.StartTimer();
    }

    public void StartGameButton()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        _preGame.SetActive(false);
        _count.SetActive(true);
        yield return new WaitForSeconds(3.33f);
        _count.SetActive(false);
        StartSpawning();
    }

    private void SpawnPlane()
    {
        int randomIndex = Random.Range(0, gamePlanes.Length);
        GameObject plane = Instantiate(gamePlanes[randomIndex]);

        // Задаем случайную позицию по оси Y
        Vector3 spawnPosition = new Vector3(
            plane.transform.position.x, // X остается неизменным
            Random.Range(6014.9f, 6015.1f), // Случайная координата Y
            plane.transform.position.z  // Z остается неизменным
        );

        if (randomIndex == targetIndex)
        {
            levelTimer.IncrementTargetCount(); // Увеличиваем счетчик целевых самолетов
        }

        // Применяем позицию к самолету
        plane.transform.position = spawnPosition;

        // Устанавливаем движение
        plane.GetComponent<Rigidbody>().velocity = Vector3.left * 0.25f; // скорость движения
    }



    public int GetTargetIndex()
    {
        return targetIndex;
    }
}
