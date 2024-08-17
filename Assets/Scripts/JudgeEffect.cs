using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JudgeEffect : MonoBehaviour
{
    public Text text;

    public void SetText(bool early, float percent)
    {
        string earlylate = early ? "EARLY" : "LATE";
        text.text = $"{text.text}\n({percent}% {earlylate})";
    }
}
