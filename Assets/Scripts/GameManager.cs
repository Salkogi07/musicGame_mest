using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public const float SPEED = 7.0f;

    public enum Judge
    {
        Miss,
        Good,
        Perfect
    }

    public static GameManager Instance;

    public int groundGauge = 0;
    public int maxGroundGauge = 100;
    public int airGauge = 0;
    public int maxAirGauge = 100;

    public float bossSkillCloudTimer = 0;
    public float bossSkillGhostTimer = 0;
    public float bossSkillReverseTimer = 0;

    public Player player;


    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Change_PlayerHp(int changeValue)
    {
        player.hp = Mathf.Clamp(player.hp + changeValue, 0, player.maxHp);
    }

    public void UseBossSkillCloud()
    {
        bossSkillCloudTimer = 5;
    }
    public bool IsCloud()
    {
        return bossSkillCloudTimer > 0;
    }
    public void UseBossSkillGhost()
    {
        bossSkillGhostTimer = 3;
    }
    public bool IsGhost()
    {
        return bossSkillGhostTimer > 0;
    }
    public void UseBossSkillReverse()
    {
        bossSkillReverseTimer = 5;
    }
    public bool IsReverse()
    {
        return bossSkillReverseTimer > 0;
    }

    public void HitNoteWithGround(Note note, Judge judge)
    {
        if (note.isIgnoreMiss)
        {

        }
        else if (note.isMultiNode && note.IsBothPressed())
        {
            if (judge == Judge.Perfect)
            {
                Change_GroundGauge(note.second_energy * 2);
            }
            else if (judge == Judge.Good)
            {
                Change_GroundGauge(note.second_energy * 1);
            }
        }
        else
        {
            if (judge == Judge.Perfect)
            {
                Change_GroundGauge(note.energy * 2);
            }
            else if (judge == Judge.Good)
            {
                Change_GroundGauge(groundGauge += note.energy * 1);
            }
        }
    }

    public void HitNoteWithAir(Note note, Judge judge)
    {
        if (note.isIgnoreMiss)
        {

        }
        else if (note.isMultiNode)
        {
            if (judge == Judge.Perfect)
            {
                Change_AirGauge(note.second_energy * 2);
            }
            else if (judge == Judge.Good)
            {
                Change_AirGauge(note.second_energy * 1);
            }
        }
        else
        {
            if (judge == Judge.Perfect)
            {
                Change_AirGauge(note.energy * 2);
            }
            else if (judge == Judge.Good)
            {
                Change_AirGauge(note.energy * 1);
            }
        }
    }

    public void Change_GroundGauge(int value)
    {
        groundGauge = Mathf.Clamp(groundGauge + value, 0, maxGroundGauge);
    }
    public void Change_AirGauge(int value)
    {
        airGauge = Mathf.Clamp(airGauge + value, 0, maxAirGauge);
    }


    public float GetRemainingSeconds(GameObject note)
    {
        if (IsReverse())
            return ((3.5f) - note.transform.position.y) / SPEED;
        else
            return (note.transform.position.y - (-3.5f)) / SPEED;
    }
}
