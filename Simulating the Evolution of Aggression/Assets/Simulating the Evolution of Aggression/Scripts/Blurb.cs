using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blurb : MonoBehaviour
{
    [SerializeField] private TMP_Text foodCounter;
    private FoodSpawner foodSpawner;
    public FoodPair targetFoodPair;
    public float myFood = 0;

    public void ChooseFood()
    {
        foodSpawner = FoodSpawner.Instance;

        targetFoodPair = foodSpawner.GetRandomFoodPair();
    }

    public void Eat()
    {
        if (targetFoodPair != null)
        {
            myFood = 2 / targetFoodPair.timesChosen;
        }

        foodCounter.text = myFood.ToString();
    }
}
