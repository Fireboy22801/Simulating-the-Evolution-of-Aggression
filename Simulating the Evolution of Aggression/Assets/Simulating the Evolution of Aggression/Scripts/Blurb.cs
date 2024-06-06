using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blurb : Mob
{
    public override void Eat()
    {
        if (targetFoodPair != null)
        {
            myFoodCount = 2 / targetFoodPair.timesChosen;

            GetFoodPosition();
            StartCoroutine(MoveToFood());

            MobSpawner.Instance.blurbsCount += myFoodCount - 1;
        }
        else
            Debug.Log("I can't eat");
    }
}
