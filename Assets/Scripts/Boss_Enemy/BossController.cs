using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        GameManager.Instance.bossSkillCloudTimer -= Time.deltaTime;
        GameManager.Instance.bossSkillGhostTimer -= Time.deltaTime;
        GameManager.Instance.bossSkillReverseTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.Instance.UseBossSkillGhost();
        }
    }


}
