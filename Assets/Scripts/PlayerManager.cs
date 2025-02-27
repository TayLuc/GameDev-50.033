using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Use this script to keep track of the player's state, e.g. hp, currency, level
public class PlayerManager : MonoBehaviour
{
    public float money;
    public float moneyMultiplier = 1; // this will increase with the player level
    public float levelUpCost = 100;
    public float levelUpCostMultiplier;
    public EntityStats destroyerStats;
    public EntityStats tankerStats;
    public EntityStats battleshipStats;
    public ShipHandler playerBase;
    public ShipHandler enemyBase;
    public ShipSpawner shipSpawner;

    public TextMeshProUGUI levelUpCostText;
    public TextMeshProUGUI currentMoneyText;
    public Slider PlayerHealthSlider;
    public float gameTimer = 0;
    public bool gameOver = false;
    public AudioSource audioSource;
    public AudioClip moneySound;

    void Update()
    {
        if (gameOver)
            return;
        // money making wewewewe
        money += 4 * moneyMultiplier * Time.deltaTime;
        levelUpCostText.text = $"${levelUpCost}";
        currentMoneyText.text = $"${money.ToString("0.00")}";
        PlayerHealthSlider.value = playerBase.currentHealth / playerBase.stats.maxHealth;

        gameTimer += Time.deltaTime;
        if (enemyBase.currentHealth <= 0)
        {
            // trigger win
            GameManager.Instance.gameHighScore.Add(gameTimer);

            GameManager.Instance.currentGameWin = true;
            StartCoroutine(TriggerGameEnd());
            gameOver = true;

        }
        if (playerBase.currentHealth <= 0)
        {
            //trigger lose
            GameManager.Instance.currentGameWin = false;
            StartCoroutine(TriggerGameEnd());

            gameOver = true;
        }
    }

    public void SpawnShip(int shipId)
    {
        // check if got money
        // ship id 0: destroyer, 1: tanker, 2: battleship
        float destroyerCost = destroyerStats.cost;
        float tankerCost = tankerStats.cost;
        float battleshipCost = battleshipStats.cost;

        if (shipId == 0 && money >= destroyerCost)
        {
            money -= destroyerCost;
            shipSpawner.SpawnDestroyer();
        }

        if (shipId == 1 && money >= tankerCost)
        {
            money -= tankerCost;
            shipSpawner.SpawnTanker();
        }

        if (shipId == 2 && money >= battleshipCost)
        {
            money -= battleshipCost;
            shipSpawner.SpawnBattleShip();
        }
    }

    public void LevelUp()
    {
        // increase the money multiplier
        if (money < levelUpCost)
            return;

        // play audio clip
        audioSource.clip = moneySound;
        audioSource.Play();
        money -= levelUpCost;
        moneyMultiplier *= 1.5f;
        levelUpCost *= levelUpCostMultiplier;
    }



    public IEnumerator TriggerGameEnd()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("GameOver");
    }


}
