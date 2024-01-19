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

    private GameManager gameManager;

    private void Awake()
    {
        targetFoodTrasform = transform;

        gameManager = GameManager.Instance;
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
            StartCoroutine(MoveToFood());

            BlurbsSpawner.Instance.blurbsCount += myFood - 1;
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
            targetFoodPair.blurb1 = this;
            targetFoodPair.blurb2 = this;
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

    private IEnumerator MoveToFood()
    {
        if (targetFoodTrasform == null)
            yield return null;

        float elapsedTime = 0;

        Vector3 startPosition = transform.position; 

        while (elapsedTime < gameManager.simulationPeriod * 0.8f)
        {
            if (targetFoodTrasform == null)
                yield return null;

            Vector3 newPosition = Vector3.Lerp(startPosition, targetFoodTrasform.position, elapsedTime / (gameManager.simulationPeriod * 0.8f));

            transform.position = newPosition;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        if (targetFoodPair.blurb1 == this && targetFoodPair.blurb2 == this)
        {
            foodSpawner.foodPairs.Remove(targetFoodPair);
            Destroy(targetFoodPair.gameObject);
        }
        if (targetFoodPair.blurb1 == this && targetFoodPair.food1 != null)
            Destroy(targetFoodPair.food1.gameObject);
        if (targetFoodPair.blurb2 == this && targetFoodPair.food2 != null)
            Destroy(targetFoodPair.food2.gameObject);

        transform.position = targetFoodTrasform.position;
    }

}