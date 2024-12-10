using System.Collections;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public GameObject[] descriptionPlanes;
    public GameObject[] gamePlanes;
    private float spawnInterval = 0.5f;
    [SerializeField] private GameObject _preGame;
    [SerializeField] private GameObject _count;
    public LevelTimer levelTimer;

    private int targetIndex;

    private void Start()
    {
        SetTargetPlane();
    }

    private void SetTargetPlane()
    {
        targetIndex = Random.Range(0, descriptionPlanes.Length);

        for (int i = 0; i < descriptionPlanes.Length; i++)
        {
            descriptionPlanes[i].SetActive(i == targetIndex);
        }

        Debug.Log($"Target plane for description is: {descriptionPlanes[targetIndex].name}");
    }

    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnPlane), 0, spawnInterval);

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

        Vector3 spawnPosition = new Vector3(
            plane.transform.position.x,
            Random.Range(6014.9f, 6015.1f),
            plane.transform.position.z
        );

        if (randomIndex == targetIndex)
        {
            levelTimer.IncrementTargetCount();
        }

        plane.transform.position = spawnPosition;

        plane.GetComponent<Rigidbody>().velocity = Vector3.left * 0.25f;
    }



    public int GetTargetIndex()
    {
        return targetIndex;
    }
}