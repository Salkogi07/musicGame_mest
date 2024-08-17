using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject reverseObj;

    private void Start()
    {
        StartCoroutine(BossRoutine());
    }

    void Update()
    {
        BossSkill_CoolTime();

        BossSkill_Reverse();
    }

    private void BossSkill_Reverse()
    {
        if (GameManager.Instance.IsReverse())
            reverseObj.transform.localScale = new Vector3(1, -1, 1);
        else
            reverseObj.transform.localScale = new Vector3(1, 1, 1);
    }

    private void BossSkill_CoolTime()
    {
        GameManager.Instance.bossSkillCloudTimer -= Time.deltaTime;
        GameManager.Instance.bossSkillGhostTimer -= Time.deltaTime;
        GameManager.Instance.bossSkillReverseTimer -= Time.deltaTime;
    }

    private IEnumerator BossRoutine()
    {
        while (true)
        {
            GameManager.Instance.UseBossSkillCloud();
            yield return new WaitForSeconds(8);
            GameManager.Instance.UseBossSkillGhost();
            yield return new WaitForSeconds(8);
            GameManager.Instance.UseBossSkillReverse();
            yield return new WaitForSeconds(8);
        }
    }
}
