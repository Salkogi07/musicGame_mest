using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpEnemy : MonoBehaviour
{
    Enemy enemy;
    public float speedUpRadius;
    public int speedUpValue;

    private List<Enemy> enemiesInRange = new List<Enemy>();

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.isLive)
            Explosion();
    }

    void Explosion()
    {
        // 현재 범위 내에 있는 적들
        Collider2D[] enemyObj = Physics2D.OverlapCircleAll(transform.position, speedUpRadius);
        List<Enemy> currentEnemiesInRange = new List<Enemy>();

        foreach (Collider2D collider in enemyObj)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("AirEnemy"))
            {
                Enemy enemyInRange = collider.GetComponent<Enemy>();
                if (!enemiesInRange.Contains(enemyInRange))
                {
                    enemyInRange.speed = speedUpValue; // 속도 증가
                }
                currentEnemiesInRange.Add(enemyInRange);
            }
        }

        // 범위 밖으로 나간 적들 속도 원래대로 복구
        foreach (Enemy enemyInRange in enemiesInRange)
        {
            if (!currentEnemiesInRange.Contains(enemyInRange))
            {
                enemyInRange.speed = enemyInRange.originalSpeed; // 속도 원래대로
            }
        }

        // 리스트 업데이트
        enemiesInRange = currentEnemiesInRange;
    }
}
