using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public ShipHandler destroyerPrefab;
    public ShipHandler tankerPrefab;
    public ShipHandler BattleshipPrefab;

    public Transform destroyerSpawn;
    public Transform tankerSpawn;
    public Transform battleshipSpawn;
    public AudioSource audioSource;
    public AudioClip destroyerSpawnSound;
    public AudioClip tankerSpawnSound;
    public AudioClip bsSpawnSound;


    public void SpawnDestroyer()
    {

        ShipHandler spawnedShip = Instantiate(destroyerPrefab);
        spawnedShip.transform.position = destroyerSpawn.position;
        audioSource.clip = destroyerSpawnSound;
        audioSource.Play();


    }

    public void SpawnTanker()
    {
        ShipHandler spawnedShip = Instantiate(tankerPrefab);
        spawnedShip.transform.position = tankerSpawn.position;
        audioSource.clip = tankerSpawnSound;
        audioSource.Play();

    }

    public void SpawnBattleShip()
    {
        ShipHandler spawnedShip = Instantiate(BattleshipPrefab);
        spawnedShip.transform.position = battleshipSpawn.position;
        audioSource.clip = bsSpawnSound;
        audioSource.Play();

    }
}