using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject reverseObj;

    void Update()
    {
        BossSkill_CoolTime();

        BossSkill_Reverse();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameManager.Instance.UseBossSkillGhost();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.Instance.UseBossSkillReverse();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.Instance.UseBossSkillCloud();
        }
    }

    private void BossSkill_Reverse()
    {
        if (GameManager.Instance.IsReverse())
            reverseObj.transform.localScale = new Vector3(1, -1, 1);
        else
            reverseObj.transform.localScale = new Vector3(1, 1, 1);
    }

    private static void BossSkill_CoolTime()
    {
        GameManager.Instance.bossSkillCloudTimer -= Time.deltaTime;
        GameManager.Instance.bossSkillGhostTimer -= Time.deltaTime;
        GameManager.Instance.bossSkillReverseTimer -= Time.deltaTime;
    }
}
