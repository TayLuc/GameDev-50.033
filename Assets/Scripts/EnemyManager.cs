using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public ShipHandler enemyBase;
    public ShipSpawner shipSpawner;
    public Slider EnemyHealthSlider;
    public float destroyerCooldownStart;
    public float tankerCooldownStart;
    public float bsCooldownStart;
    public float destroyerCooldown;
    public float tankerCooldown;
    public float bsCooldown;



    public void Update()
    {
        EnemyHealthSlider.value = enemyBase.currentHealth / enemyBase.stats.maxHealth;
        destroyerCooldown -= Time.deltaTime;
        tankerCooldown -= Time.deltaTime;
        bsCooldown -= Time.deltaTime;
        SpawnEnemyShip();

    }

    public void SpawnEnemyShip()
    {
        // check if got money
        // ship id 0: destroyer, 1: tanker, 2: battleship

        if (destroyerCooldown <= 0)
        {
            shipSpawner.SpawnDestroyer();
            destroyerCooldown = destroyerCooldownStart;
        }

        if (tankerCooldown <= 0)
        {
            shipSpawner.SpawnTanker();
            tankerCooldown = tankerCooldownStart;
        }

        if (bsCooldown <= 0)
        {
            shipSpawner.SpawnBattleShip();
            bsCooldown = bsCooldownStart;
        }
    }


}
