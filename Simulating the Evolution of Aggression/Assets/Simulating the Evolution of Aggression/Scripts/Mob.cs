using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    protected FoodSpawner foodSpawner;
    public FoodPair targetFoodPair;
    public float myFoodCount = 0;
    public Transform targetFoodTrasform;

    protected GameManager gameManager;

    protected void Awake()
    {
        targetFoodTrasform = transform;

        gameManager = GameManager.Instance;
    }

    public void ChooseFood()
    {
        foodSpawner = FoodSpawner.Instance;

        targetFoodPair = foodSpawner.GetRandomFoodPair();
    }

    public virtual void Eat()
    {
    }

    protected void GetFoodPosition()
    {
        if (targetFoodTrasform == null)
        {
            Debug.LogError("Target food position is not set");
            return;
        }

        if (myFoodCount == 2)
        {
            targetFoodPair.owner1 = this;
            targetFoodPair.owner2 = this;

            targetFoodTrasform = targetFoodPair.transform;
        }
        else if (targetFoodPair.owner1 == null)
        {
            targetFoodPair.owner1 = this;
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
            targetFoodPair.owner2 = this;
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

    protected IEnumerator MoveToFood()
    {
        if (targetFoodTrasform == null)
            yield return null;

        this.transform.LookAt(targetFoodTrasform);

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

        if (targetFoodPair.owner1 == this && targetFoodPair.owner2 == this)
        {
            foodSpawner.foodPairs.Remove(targetFoodPair);
            Destroy(targetFoodPair.gameObject);
        }
        if (targetFoodPair.owner1 == this && targetFoodPair.food1 != null)
            Destroy(targetFoodPair.food1.gameObject);
        if (targetFoodPair.owner2 == this && targetFoodPair.food2 != null)
            Destroy(targetFoodPair.food2.gameObject);

        transform.position = targetFoodTrasform.position;
    }

}