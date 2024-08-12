using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Hp, GroundGauge, AirGauge}
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Hp:
                float curHealth = GameManager.Instance.player.hp;
                float maxHealth = GameManager.Instance.player.maxHp;
                mySlider.value = curHealth / maxHealth;
                break;
            case InfoType.GroundGauge:
                float gauge_G = GameManager.Instance.groundGauge;
                float maxGauge_G = GameManager.Instance.maxGroundGauge;
                mySlider.value = gauge_G / maxGauge_G;
                break;
            case InfoType.AirGauge:
                float gauge_A = GameManager.Instance.airGauge;
                float maxGauge_A = GameManager.Instance.maxAirGauge;
                mySlider.value = gauge_A / maxGauge_A;
                break;
        }
    }
}
