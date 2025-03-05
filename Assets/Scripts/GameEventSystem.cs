// GameEventSystem.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Simple GameEvent Scriptable Object
[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

    public GameEvent gameRestartEvent;

    public void RestartGame()
    {
        if (gameRestartEvent != null)
        {
            gameRestartEvent.Raise();
        }

        // Your existing restart code...
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}