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
    public Transform targetFoodTrasform;

    private void Awake()
    {
        targetFoodTrasform = transform;
    }

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
            GetFoodPosition();
            TeleportToFood();
        }

        foodCounter.text = myFood.ToString();
    }

    private void GetFoodPosition()
    {
        if (targetFoodTrasform == null)
        {
            Debug.LogError("Target food position is not set");
            return;
        }

        if (myFood == 2)
        {
            targetFoodTrasform = targetFoodPair.transform;
        }
        else if (targetFoodPair.blurb1 == null)
        {
            targetFoodPair.blurb1 = this;
            if (targetFoodPair.food1 != null)
            {
                targetFoodTrasform = targetFoodPair.food1.transform;
            }
            else
            {
                Debug.LogError("Food1 is not set");
            }
        }
        else
        {
            targetFoodPair.blurb2 = this;
            if (targetFoodPair.food2 != null)
            {
                targetFoodTrasform = targetFoodPair.food2.transform;
            }
            else
            {
                Debug.LogError("Food2 is not set");
            }
        }
    }

    private void TeleportToFood()
    {
        transform.position = targetFoodTrasform.position;
    }
}