using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] spawnPoints;
    public GameObject middleBoss;
    public GameObject lastBoss;

    public float spawnTime;

    private List<int> bag = new List<int>();

    private bool isBoss;

    void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(SpawnBoss());
    }

    void Update()
    {
        
    }

    int GetFromBag()
    {
        if (bag.Count == 0)
            bag = new List<int>() { 0, 1, 2, 3 };

        int rng = Random.Range(0, bag.Count);
        int result = bag[rng];
        bag.RemoveAt(rng);

        return result;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            if (!isBoss)
            {
                int randomEnemy = Random.Range(0, enemyPrefab.Length);
                Instantiate(enemyPrefab[randomEnemy], spawnPoints[GetFromBag()].transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("중간보스소환알림");
        yield return new WaitForSeconds(10);

        GameObject instance = Instantiate(middleBoss, spawnPoints[GetFromBag()].transform.position, Quaternion.identity);
        isBoss = true;
        while (instance)
        {
            yield return null;
        }
        isBoss = false;

        Debug.Log("보스소환알림");
        yield return new WaitForSeconds(20);

        lastBoss.SetActive(true);
        isBoss = true;
        while (lastBoss)
        {
            yield return null;
        }
        isBoss = false;


    }
}
