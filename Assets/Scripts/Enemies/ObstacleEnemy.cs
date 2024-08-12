using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnemy : MonoBehaviour
{
    public GameObject obstacleObj;
    public GameObject obstacleEffectObj;
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
        Instantiate(obstacleEffectObj, transform.position, Quaternion.identity);
        Instantiate(obstacleObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
