using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public GameObject smallEnemy;
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (!enemy.isLive)
            ObstacleSpanw();
    }


    void ObstacleSpanw()
    {
        Instantiate(smallEnemy, transform.position + new Vector3(-1, -1, 0), Quaternion.identity);
        Instantiate(smallEnemy, transform.position + new Vector3(-1, 1, 0), Quaternion.identity);
        Instantiate(smallEnemy, transform.position + new Vector3(1, -1, 0), Quaternion.identity);
        Instantiate(smallEnemy, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
