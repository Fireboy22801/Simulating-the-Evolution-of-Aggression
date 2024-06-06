using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float simulationPeriod = 3f;

    private MobSpawner mobsSpawner;
    private FoodSpawner foodSpawner;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 100;
    }

    private void Start()
    {
        mobsSpawner = MobSpawner.Instance;
        foodSpawner = FoodSpawner.Instance;

        StartCoroutine(Simulation());
    }

    private IEnumerator Simulation()
    {
        while (true)
        {
            foodSpawner.SpawnFood();

            mobsSpawner.SpawnMobs();
            mobsSpawner.PlaceMobsInCircle();

            mobsSpawner.ChooseFood();
            mobsSpawner.EatFood();

            yield return new WaitForSeconds(simulationPeriod);
        }
    }
}