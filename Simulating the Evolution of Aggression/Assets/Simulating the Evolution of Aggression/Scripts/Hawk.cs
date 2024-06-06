using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : Mob
{
    public override void Eat()
    {
        if (targetFoodPair != null)
        {
            if (targetFoodPair.timesChosen == 1)
            {
                targetFoodPair.owner1 = this;
                targetFoodPair.owner2 = this;

                myFoodCount = 2;
            }
            else if (targetFoodPair.owner1 == null)
                targetFoodPair.owner1 = this;
            else if (targetFoodPair.owner2 == null)
                targetFoodPair.owner2 = this;


            if (targetFoodPair.owner1 is Hawk && targetFoodPair.owner2 is Hawk)
            {
                myFoodCount = 0;
            }
            else if (targetFoodPair.owner1 is Hawk && targetFoodPair.owner2 is Blurb)
            {
                myFoodCount = 1.5f;
                targetFoodPair.owner2.myFoodCount = 0.5f;
            }
            else if (targetFoodPair.owner1 is Blurb && targetFoodPair.owner2 is Hawk)
            {
                myFoodCount = 1.5f;
                targetFoodPair.owner1.myFoodCount = 0.5f;
            }
            else
            {
                myFoodCount = 2;
            }


            GetFoodPosition();
            StartCoroutine(MoveToFood());

            MobSpawner.Instance.hawksCount += myFoodCount - 1;
        }
        else
            Debug.Log("I can't eat");
    }
}
