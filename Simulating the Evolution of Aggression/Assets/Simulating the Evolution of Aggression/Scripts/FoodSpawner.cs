using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private GameObject foodPairPrefab;
    [SerializeField] private GameObject foodParent;
    [SerializeField] private int initialPairCount = 6;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float distanceBetweenPairs = 1f;
    [SerializeField] private int numberOfCircles = 4;
    [SerializeField] private float distanceBetweenCircles = 7f;

    public List<FoodPair> foodPairs = new List<FoodPair>();

    private void Awake()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        for (int j = 0; j < numberOfCircles; j++)
        {
            int pairCount = initialPairCount + (j * 6);
            float angleStep = 360f / pairCount;
            float currentRadius = radius + j * distanceBetweenCircles;

            for (int i = 0; i < pairCount; i++)
            {
                float angle = angleStep * i;
                Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * currentRadius;

                FoodPair foodPair = Instantiate(foodPairPrefab, position, Quaternion.identity, foodParent.transform).GetComponent<FoodPair>();

                Food food1 = Instantiate(foodPrefab, position, Quaternion.identity, foodPair.transform).GetComponent<Food>();

                Vector3 pairOffset = Quaternion.Euler(0, 90, 0) * (position.normalized * distanceBetweenPairs);
                Vector3 pairPosition = position + pairOffset;
                Food food2 = Instantiate(foodPrefab, pairPosition, Quaternion.identity, foodPair.transform).GetComponent<Food>();

               foodPair.food1 = food1;
                foodPair.food2 = food2;

                foodPairs.Add(foodPair);
            }
        }
    }
}
