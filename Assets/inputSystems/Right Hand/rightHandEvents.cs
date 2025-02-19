using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class rightHandEvents : MonoBehaviour
{
    public GameObject wheelPrefab;
    public GameObject revolver;
    public Transform spawnPoint;
    private GameObject spawnedWheel;


    public void spawnWheel()
    {

        if (spawnedWheel == null)
        {
            Vector3 offset = new Vector3(0, 0.7f, 0);
            spawnedWheel = Instantiate(wheelPrefab, spawnPoint.position + offset, spawnPoint.rotation);
            spawnedWheel.transform.parent = revolver.transform;

            // Start coroutine to delay the offset
            StartCoroutine(AnimateWheelOffset());
        }
        else
        {
            Debug.Log("Wheel already exists.");
        }
    }


    private IEnumerator AnimateWheelOffset()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Initial and target positions
        Vector3 startPosition = spawnedWheel.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(-0.7f, 0, 0); // Offset to the left

        float duration = 1f; // Duration of the animation (1 second)
        float timeElapsed = 0f;

        // Animate the wheel's position over time
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime; // Increment elapsed time
            spawnedWheel.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            yield return null; // Wait for the next frame
        }

        // Ensure the final position is exactly the target
        spawnedWheel.transform.position = targetPosition;
    }

    public void spinAndLoadRevolver()
    {
        StartCoroutine(SpinAndMoveWheel());
    }

    private IEnumerator SpinAndMoveWheel()
    {
        float moveDuration = 1f;
        float spinDuration = 1f; // 10 full spins in 1 second
        float elapsedTime = 0f;

        Vector3 startPosition = spawnedWheel.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0.7f, 0, 0);

        // Move the wheel to the right first
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            spawnedWheel.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            yield return null;
        }

        spawnedWheel.transform.position = targetPosition;

        // Reset elapsed time for rotation
        elapsedTime = 0f;
        float totalRotation = 1800f; //SPIN MORE WITH HIGHER VALUE

        EventManager.spinStart(true); // trigger the spin audio listener
        // Spin the wheel after moving
        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;
            float rotationStep = (totalRotation / spinDuration) * Time.deltaTime; // Smooth rotation step
            spawnedWheel.transform.Rotate(0, 0, rotationStep);
            yield return null;
        }

        Destroy(spawnedWheel);
        EventManager.spinFinished(true); // notify listener in rightactionmanager

    }

    // Logic for firing the gun and triggering the 
    public void russianRoulette()
    {
        Debug.Log("rightHandEvent russian roulette");
        int checkInt = Random.Range(0, 5);
        int fireInt = Random.Range(0, 5);
        bool playerKilled = checkInt == fireInt;
        Debug.Log(playerKilled);
        EventManager.fireGun(playerKilled);
    }

}
