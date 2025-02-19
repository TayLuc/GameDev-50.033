using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rouletteEvents : MonoBehaviour
{
    private gameManager gameManagerInstance;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerInstance = FindObjectOfType<gameManager>(); // Find the gameManager in the scene
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnEnable()
    {
        EventManager.OnFireGun += firingPointLogic;
    }

    void OnDisable()
    {
        EventManager.OnFireGun -= firingPointLogic;
    }
    public void firingPointLogic(bool playerKilled)
    {
        Debug.Log("Triggering roulette logic");
        if (playerKilled)
        {
            playerDied();
        }
        else
        {
            playerSurvived();
        }

    }

    private void playerSurvived()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.IncreaseScore(1);
            // bring the gun back to the middle
            gameManagerInstance.ResetRightArm();

        }
    }

    private void playerDied()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.GameOver();
        }
    }
}
