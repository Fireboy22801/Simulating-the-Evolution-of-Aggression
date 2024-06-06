using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public static MobSpawner Instance;

    [SerializeField] private GameObject blurbPrefab;
    [SerializeField] private GameObject hawkPrefab;
    [SerializeField] private float yMobSpawnOffSet = -0.5f;
    [SerializeField] private float spawnRadius = 28.25f;
    [SerializeField] private TMP_Text mobsCounter;
    public float blurbsCount;
    public float hawksCount;

    private List<Mob> mobs = new List<Mob>();

    private int indexName = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnMobs()
    {
        indexName = 0;

        MobsClear();

        for (int i = 0; i < blurbsCount; i++)
            SpawnMob(blurbPrefab);
        for (int i = 0; i < hawksCount; i++)
            SpawnMob(hawkPrefab);

        mobsCounter.text = (blurbsCount + hawksCount).ToString();
    }

    public void PlaceMobsInCircle()
    {
        float angleStep = 360f / mobs.Count;
        float randomStartAngle = Random.Range(0f, 360f);

        for (int i = 0; i < mobs.Count; i++)
        {
            float angle = randomStartAngle + angleStep * i;
            Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * spawnRadius;
            position.y = yMobSpawnOffSet;
            mobs[i].transform.position = position;
        }
    }

    public void SpawnMob(GameObject prefab)
    {
        Mob newMob = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Mob>();
        newMob.name = prefab.name + " #" + indexName.ToString();
        indexName++;
        mobs.Add(newMob);
    }

    public void ChooseFood()
    {
        foreach (Mob mob in mobs)
        {
            mob.ChooseFood();
        }
    }

    public void EatFood()
    {
        foreach (Mob mob in mobs)
        {
            mob.Eat();
        }
    }

    private void MobsClear()
    {
        foreach (Mob mob in mobs)
            Destroy(mob.gameObject);

        mobs.Clear();
    }
}
