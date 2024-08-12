using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public bool ground;

    public float speed;
    public float originalSpeed;

    public bool isLive = true;

    private void Awake()
    {
        originalSpeed = speed;
    }

    public void Update()
    {
        if (hp <= 0)
            EnemyDie();

        transform.Translate((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

    private void EnemyDie()
    {
        isLive = false;
    }

    public void Attack(int damage)
    {
        hp -= damage;
    }
}
