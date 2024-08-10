using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject testEnemy;
    public GameObject[] spawnPoints;

    public float spawnTime;

    private List<int> bag = new List<int>();

    void Start()
    {
        StartCoroutine(Spawn());
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

            Instantiate(testEnemy, spawnPoints[GetFromBag()].transform.position, Quaternion.identity);
        }
    }
}
