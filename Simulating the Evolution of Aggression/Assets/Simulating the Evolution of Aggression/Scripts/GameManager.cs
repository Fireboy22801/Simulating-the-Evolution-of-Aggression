using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float simulationPeriod = 3f;

    private BlurbsSpawner blurbsSpawner;
    private FoodSpawner foodSpawner;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 100;
    }

    private void Start()
    {
        blurbsSpawner = BlurbsSpawner.Instance;
        foodSpawner = FoodSpawner.Instance;

        StartCoroutine(Simulation());
    }

    private IEnumerator Simulation()
    {
        while (true)
        {
            foodSpawner.SpawnFood();

            blurbsSpawner.SpawnBlurbs();
            blurbsSpawner.PlaceBlurbsInCircle();

            blurbsSpawner.ChooseFood();
            blurbsSpawner.EatFood();

            yield return new WaitForSeconds(simulationPeriod);
        }
    }
}