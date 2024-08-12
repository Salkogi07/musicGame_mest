using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealEnemy : MonoBehaviour
{
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (!enemy.isLive)
            Heal();
    }


    void Heal()
    {
        GameManager.Instance.Change_PlayerHp(50);
    }
}
