using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public int maxHp;

    public float attackTime;
    public float attackRange;
    public int attackDamage;

    public bool isAttack;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        if (isAttack)
            PlayAnimation("Attack");
        else
            PlayAnimation("Idle");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            GameManager.Instance.Change_PlayerHp(-other.GetComponent<Enemy>().hp);
            Destroy(other.gameObject);
        }
        if (other.tag == "AirEnemy")
        {
            GameManager.Instance.Change_PlayerHp(-other.GetComponent<Enemy>().hp);
            Destroy(other.gameObject);
        }
    }

    private GameObject[] GetEnemies()
    {
        GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        System.Array.Sort(enemyGameObjects, (GameObject x, GameObject y) => Vector3.Distance(x.transform.position, transform.position) < Vector3.Distance(y.transform.position, transform.position) ? -1 : 1);
        return enemyGameObjects;
    }

    private GameObject[] GetAirEnemies()
    {
        GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("AirEnemy");
        System.Array.Sort(enemyGameObjects, (GameObject x, GameObject y) => Vector3.Distance(x.transform.position, transform.position) < Vector3.Distance(y.transform.position, transform.position) ? -1 : 1);
        return enemyGameObjects;
    }

    private void AttackNearEnemies(GameObject[] enemyGameObjects, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (enemyGameObjects.Length <= i)
                break;
            if (Vector3.Distance(enemyGameObjects[i].transform.position, transform.position) > attackRange)
                break;
  
            enemyGameObjects[i].GetComponent<Enemy>().Attack(attackDamage);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            GameObject[] enemies = GetEnemies();
            GameObject[] airEnemies = GetAirEnemies();

            if (enemies.Length > 0)
            {
                if (GameManager.Instance.groundGauge > 80)
                {
                    AttackNearEnemies(enemies, 8);
                    GameManager.Instance.Change_GroundGauge(-1);
                }
                else if (GameManager.Instance.groundGauge > 40)
                {
                    AttackNearEnemies(enemies, 4);
                    GameManager.Instance.Change_GroundGauge(-1);
                }
                else if (GameManager.Instance.groundGauge > 20)
                {
                    AttackNearEnemies(enemies, 2);
                    GameManager.Instance.Change_GroundGauge(-1);
                }
                else if (GameManager.Instance.groundGauge >= 1)
                {
                    AttackNearEnemies(enemies, 1);
                    GameManager.Instance.Change_GroundGauge(-1);
                }
                else
                {
                    // nth
                }
            }
            else
            {
                if (GameManager.Instance.airGauge > 80)
                {
                    AttackNearEnemies(airEnemies, 8);
                    GameManager.Instance.Change_AirGauge(-1);
                }
                else if (GameManager.Instance.airGauge > 40)
                {
                    AttackNearEnemies(airEnemies, 4);
                    GameManager.Instance.Change_GroundGauge(-1);
                }
                else if (GameManager.Instance.airGauge > 20)
                {
                    AttackNearEnemies(airEnemies, 2);
                    GameManager.Instance.Change_GroundGauge(-1);
                }
                else if (GameManager.Instance.airGauge >= 1)
                {
                    AttackNearEnemies(airEnemies, 1);
                    GameManager.Instance.Change_GroundGauge(-1);
                }
                else
                {
                    // nth
                }
            }

            yield return new WaitForSeconds(attackTime);
        }
    }

    private string lastAnimation = null;

    public void PlayAnimation(string animName)
    {
        if (lastAnimation != animName)
        {
            anim.Play(animName);
            lastAnimation = animName;
        }
    }
}
