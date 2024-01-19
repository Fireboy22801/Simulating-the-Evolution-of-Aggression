using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurbsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blurbPrefab;
    [SerializeField] private int initialBlurbCount = 5;
    [SerializeField] private float yBlurbSpawnOffSet = -0.5f;
    [SerializeField] private float spawnRadius = 28.25f;

    private List<Blurb> blurbs = new List<Blurb>();

    private void Start()
    {
        for (int i = 0; i < initialBlurbCount; i++)
            AddBlurb();

        SpawnBlurbsInCircle();
    }

    private void SpawnBlurbsInCircle()
    {
        float angleStep = 360f / blurbs.Count;
        float randomStartAngle = Random.Range(0f, 360f);

        for (int i = 0; i < blurbs.Count; i++)
        {
            float angle = randomStartAngle + angleStep * i;
            Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * spawnRadius;
            position.y = yBlurbSpawnOffSet;
            blurbs[i].transform.position = position;
        }
    }

    public void AddBlurb()
    {
        Blurb newBlurb = Instantiate(blurbPrefab, Vector3.zero, Quaternion.identity).GetComponent<Blurb>();
        blurbs.Add(newBlurb);
    }
}
