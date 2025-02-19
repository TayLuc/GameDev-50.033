using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiManager : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject gameOverPanel;
    public GameObject EndScore;
    public Transform restartButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
        gameOverPanel.SetActive(false);
    }


    public void SetScore(int score)
    {
        // Debug.Log("UI manager score is" + score.ToString());
        scoreText.GetComponent<TextMeshProUGUI>().text = "Rounds Survived: " + score.ToString();
        EndScore.GetComponent<TextMeshProUGUI>().text = "YOU SURVIVED " + score.ToString() + " ROUNDS";
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true);

    }

    public void gameRestart()
    {
        gameOverPanel.SetActive(false);
    }
}
