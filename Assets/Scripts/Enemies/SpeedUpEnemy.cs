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
        // ���� ���� ���� �ִ� ����
        Collider2D[] enemyObj = Physics2D.OverlapCircleAll(transform.position, speedUpRadius);
        List<Enemy> currentEnemiesInRange = new List<Enemy>();

        foreach (Collider2D collider in enemyObj)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("AirEnemy"))
            {
                Enemy enemyInRange = collider.GetComponent<Enemy>();
                if (!enemiesInRange.Contains(enemyInRange))
                {
                    enemyInRange.speed = speedUpValue; // �ӵ� ����
                }
                currentEnemiesInRange.Add(enemyInRange);
            }
        }

        // ���� ������ ���� ���� �ӵ� ������� ����
        foreach (Enemy enemyInRange in enemiesInRange)
        {
            if (!currentEnemiesInRange.Contains(enemyInRange))
            {
                enemyInRange.speed = enemyInRange.originalSpeed; // �ӵ� �������
            }
        }

        // ����Ʈ ������Ʈ
        enemiesInRange = currentEnemiesInRange;
    }
}
