using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isDieSkill = false;

    public int hp;
    public int maxHp;
    public bool ground;

    public float speed;
    public float originalSpeed;

    public bool isLive = true;
    bool isHit = false;

    private void Awake()
    {
        originalSpeed = speed;
    }

    public void Update()
    {
        if (isHit)
            return;

        if (hp <= 0)
            EnemyDie();

        transform.Translate((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

    private void EnemyDie()
    {
        if(isDieSkill)
            isLive = false;
        else
            Destroy(gameObject);
    }

    public void Attack(int damage)
    {
        StartCoroutine(Hit());
        hp -= damage;
    }

    IEnumerator Hit()
    {
        isHit = true;
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }
}
