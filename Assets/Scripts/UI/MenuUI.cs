using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuUI : MonoBehaviour
{
    public Text text;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        text.text =
            $"{SaveNaver.instance.Rankname[0]}     {SaveNaver.instance.Score[0]}\n\n" +
            $"{SaveNaver.instance.Rankname[1]}     {SaveNaver.instance.Score[1]}\n\n" +
            $"{SaveNaver.instance.Rankname[2]}     {SaveNaver.instance.Score[2]}\n\n" +
            $"{SaveNaver.instance.Rankname[3]}     {SaveNaver.instance.Score[3]}\n" +
            $"{SaveNaver.instance.Rankname[4]}     {SaveNaver.instance.Score[4]}\n" +
            $"{SaveNaver.instance.Rankname[5]}     {SaveNaver.instance.Score[5]}\n" +
            $"{SaveNaver.instance.Rankname[6]}     {SaveNaver.instance.Score[6]}\n" +
            $"{SaveNaver.instance.Rankname[7]}     {SaveNaver.instance.Score[7]}\n" +
            $"{SaveNaver.instance.Rankname[8]}     {SaveNaver.instance.Score[8]}\n" +
            $"{SaveNaver.instance.Rankname[9]}     {SaveNaver.instance.Score[9]}\n";
    }
}
