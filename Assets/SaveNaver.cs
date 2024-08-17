using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNaver : MonoBehaviour
{
    public static SaveNaver instance;

    public string[] Rankname = new string[10];
    public int[] Score = new int[10];

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void InsertRank(string new_name, int new_score)
    {
        for (int i = Rankname.Length; i >= 1; i--)
        {
            if (Score[i - 1] > new_score)
            {
                if (i == Rankname.Length)
                {
                    // ·©Å· ¹Û
                }
                else
                {
                    for (int j = Rankname.Length - 2; j >= i; j--)
                    {
                        Rankname[j + 1] = Rankname[j];
                        Score[j + 1] = Score[j];
                    }
                    Rankname[i] = new_name;
                    Score[i] = new_score;
                }

                break;
            }
        }
        for (int j = Rankname.Length - 2; j >= 0; j--)
        {
            Rankname[j + 1] = Rankname[j];
            Score[j + 1] = Score[j];
        }
        Rankname[0] = new_name;
        Score[0] = new_score;
    }

    void Update()
    {
        
    }
}
