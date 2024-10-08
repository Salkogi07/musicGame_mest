using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Enemy : MonoBehaviour
{
    Enemy enemy;
    public GameObject BoomEffect;
    public float explosionRadius;
    public int explosionDamage;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (!enemy.isLive)
            Explosion();
    }

    void Explosion()
    {
        Instantiate(BoomEffect,transform.position, Quaternion.identity);
        Collider2D[] enemyObj = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in enemyObj)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().hp -= explosionDamage;
            }
        }
        Destroy(gameObject);
    }
}
