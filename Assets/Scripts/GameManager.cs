using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public const float SPEED = 3.5f;

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

    public float itemMonsterPauseTimer = 0;
    public float itemEnergyTimer = 0;
    public float itemTimerTimer = 0;
    public float itemPerfectTimer = 0;
    public int itemMissDefenseCount = 0;

    public float playTime = 0;
    public int comboCount = 0;
    public float itemCount = 0;
    public float judgePerfectCount = 0;
    public float judgeGoodCount = 0;
    public float judgeMissCount = 0;
    public float noteCount = 0;
    public float finalScore = 0;
    public float monsterKillCount = 0;

    public Player player;

    public GameObject winPanel;
    public GameObject losePanel;
    public Text resultText;
    public InputField nameInputField;
    public Text comboText;


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
        playTime += Time.deltaTime;

        itemMonsterPauseTimer -= Time.deltaTime;
        itemEnergyTimer -= Time.deltaTime;
        itemTimerTimer -= Time.deltaTime;
        itemPerfectTimer -= Time.deltaTime;

        comboText.text = comboCount.ToString();
    }

    public void Change_PlayerHp(int changeValue)
    {
        player.hp = Mathf.Clamp(player.hp + changeValue, 0, player.maxHp);
        if (player.hp <= 0)
            GameLose();
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
                GroundGaugeCombo(note);
            }
            else if (judge == Judge.Good)
            {
                Change_GroundGauge(note.second_energy * 1);
                GroundGaugeCombo(note);
            }
        }
        else
        {
            if (judge == Judge.Perfect)
            {
                Change_GroundGauge(note.energy * 2);
                GroundGaugeCombo(note);
            }
            else if (judge == Judge.Good)
            {
                Change_GroundGauge(groundGauge += note.energy * 1);
                GroundGaugeCombo(note);
            }
        }
    }

    private void GroundGaugeCombo(Note note)
    {
        if(comboCount % 2 == 0 && comboCount != 0)
        {
            Change_GroundGauge(note.second_energy * 2);
            Change_AirGauge(note.second_energy);
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
                AirGaugeCombo(note);
            }
            else if (judge == Judge.Good)
            {
                Change_AirGauge(note.second_energy * 1);
                AirGaugeCombo(note);
            }
        }
        else
        {
            if (judge == Judge.Perfect)
            {
                Change_AirGauge(note.energy * 2);
                AirGaugeCombo(note);
            }
            else if (judge == Judge.Good)
            {
                Change_AirGauge(note.energy * 1);
                AirGaugeCombo(note);
            }
        }
    }

    private void AirGaugeCombo(Note note)
    {
        if (comboCount % 2 == 0 && comboCount != 0)
        {
            Change_AirGauge(note.second_energy * 2);
            Change_GroundGauge(note.second_energy);
        }
    }

    public void Change_GroundGauge(int value)
    {
        groundGauge = Mathf.Clamp(groundGauge + value * (itemEnergyTimer > 0 ? 2 : 1), 0, maxGroundGauge);
    }
    public void Change_AirGauge(int value)
    {
        airGauge = Mathf.Clamp(airGauge + value * (itemEnergyTimer > 0 ? 2 : 1), 0, maxAirGauge);
    }


    public float GetRemainingSeconds(GameObject note)
    {
        if (IsReverse())
            return ((3.5f) - note.transform.position.y) / SPEED;
        else
            return (note.transform.position.y - (-3.5f)) / SPEED;
    }

    public void GameWin()
    {
        int score = Mathf.FloorToInt(judgePerfectCount * 100 + judgeGoodCount * 50 + monsterKillCount * 100);

        float totalJudge = (judgePerfectCount + judgeGoodCount * 0.5f) / noteCount * 100;
        resultText.text = $"Play Time: {string.Format("{0:####.00}", playTime)}s\n" +
            $"Item Count: {itemCount}\n" +
            $"Notes: {noteCount}\n" +
            $"Total Perfect Rate: {string.Format("{0:###.00}", totalJudge)}%\n" +
            $"Average Perfect Rate: {string.Format("{0:###.00}", totalJudge)}%\n" +
            $"Total Score: {score}\n" +
            $"Monster Kill Count: {monsterKillCount}\n";

        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RankName()
    {
        Debug.Log(nameInputField.text);
        int score = Mathf.FloorToInt(judgePerfectCount * 100 + judgeGoodCount * 50 + monsterKillCount * 100);
        SaveNaver.instance.InsertRank(nameInputField.text, score);
        nameInputField.gameObject.SetActive(false);
    }

    public void GameLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Stage1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Stage2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void Ending()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
}
