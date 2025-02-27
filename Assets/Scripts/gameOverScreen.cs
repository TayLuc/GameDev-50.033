using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highScoreText;

    public void Start()
    {
        gameOverText.SetText(GameManager.Instance.currentGameWin ? "YOU ARE WIN!!!" : "YOU ARE KILL :(");
        SetHighScore();
    }

    public void SetHighScore()
    {
        List<float> highscores = GameManager.Instance.gameHighScore;
        string combinedString = string.Join("\n", highscores.ToArray());
        highScoreText.SetText(combinedString);
    }

}
