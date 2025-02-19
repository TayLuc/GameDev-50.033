using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class gameManager : MonoBehaviour
{
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public GameObject rightArm;
    // public Vector3 rightArmSpawn;



    private int score = 0;

    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getScore()
    {
        return score;
    }

    public void GameRestart()
    {
        ResetRightArm();
        // reset score
        score = 0;
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        // Debug.Log("current score is" + score.ToString());
        SetScore(score);
    }
    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void ResetRightArm()
    {
        // rightArm.transform.position = rightArmSpawn;
        rightArm.GetComponent<Animator>().SetTrigger("ReturnToIdle");
    }

}
