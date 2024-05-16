using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleHighscore : MonoBehaviour
{
    public TMP_Text scoreText;
    public int currentScore; // hvad end vore score hedder??

    public TMP_Text finalScoreText;
    public TMP_Text highscoreText;


    public void HighscoreUpdate()
    {
        if (PlayerPrefs.HasKey("SavedHighscore"))
        {
            if (currentScore > PlayerPrefs.GetInt("SavedHighscore"))
            {
                PlayerPrefs.SetInt("SavedHighscore", currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("SavedHighscore", currentScore);
        }

        finalScoreText.text = currentScore.ToString();
        highscoreText.text = PlayerPrefs.GetInt("SavedHighscore").ToString();
    }
}
