using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public bool ground;

    public float speed;

    public void Update()
    {
        if (hp <= 0)
            gameObject.SetActive(false);

        transform.Translate((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

    public void Attack(int damage)
    {
        hp -= damage;
    }
}
