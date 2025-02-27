using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;
    public AudioSource audioSource;

    public AudioClip transitionSound;
    public void LoadScene(string sceneName)
    {
        audioSource.clip = transitionSound;
        audioSource.Play();
        SceneManager.LoadScene(sceneName);
    }
}