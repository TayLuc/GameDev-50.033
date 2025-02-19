using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<Collider2D> OnBulletDropped;
    public static event Action<bool> OnBulletLoaded;
    public static event Action<bool> OnSpinFinished;
    public static event Action<bool> OnFireGun;
    public static event Action<bool> OnSpinStart;

    public static void BulletDropped(Collider2D bulletCollider)
    {
        OnBulletDropped?.Invoke(bulletCollider); // Notify all listeners

    }

    public static void bulletLoaded(bool loaded)
    {
        OnBulletLoaded?.Invoke(loaded);
    }

    public static void spinFinished(bool spinFinished)
    {
        OnSpinFinished?.Invoke(spinFinished);
    }

    public static void spinStart(bool start)
    {
        OnSpinStart?.Invoke(start);
    }

    public static void fireGun(bool playerKilled)
    {
        OnFireGun?.Invoke(playerKilled);
    }
}
