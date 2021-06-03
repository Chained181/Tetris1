using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    public Text ScoreText, HighScoreText;
    public static int score = 0;
    public int highscore;

    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("SaveScore"))
        {
            highscore = PlayerPrefs.GetInt("SaveScore");
            HighScoreText.text = "High Score:" + highscore.ToString();
        }


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Score()
    {
        score += 40;
        ScoreText.text = "Score:" + score.ToString();
        highScore();

    }
    public void highScore()
    {
        if (score > highscore)
        {
            
            highscore = score;
            HighScoreText.text = "High Score:" + highscore.ToString();

        }

        PlayerPrefs.SetInt("SaveScore", highscore);

    }
    
}
