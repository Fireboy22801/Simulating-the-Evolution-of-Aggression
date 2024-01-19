using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlurbsSpawner : MonoBehaviour
{
    public static BlurbsSpawner Instance;

    [SerializeField] private GameObject blurbPrefab;
    [SerializeField] private float yBlurbSpawnOffSet = -0.5f;
    [SerializeField] private float spawnRadius = 28.25f;
    [SerializeField] private TMP_Text blurbsCounter;
    public float blurbsCount = 5;

    private List<Blurb> blurbs = new List<Blurb>();

    private int indexName = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBlurbs()
    {
        indexName = 0;

        blurbsCounter.text = blurbsCount.ToString();

        BlurbsClear();

        for (int i = 0; i < blurbsCount; i++)
            SpawnBlurb();
    }

    public void PlaceBlurbsInCircle()
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

    public void SpawnBlurb()
    {
        Blurb newBlurb = Instantiate(blurbPrefab, Vector3.zero, Quaternion.Euler(0, 180, 0)).GetComponent<Blurb>();
        newBlurb.name = "Blurb #" + indexName.ToString();
        indexName++;
        blurbs.Add(newBlurb);
    }

    public void ChooseFood()
    {
        foreach (Blurb blurb in blurbs)
        {
            blurb.ChooseFood();
        }
    }

    public void EatFood()
    {
        foreach (Blurb blurb in blurbs)
        {
            blurb.Eat();
        }
    }

    private void BlurbsClear()
    {
        foreach(Blurb blurb in blurbs)
            Destroy(blurb.gameObject);

        blurbs.Clear();
    }
}
