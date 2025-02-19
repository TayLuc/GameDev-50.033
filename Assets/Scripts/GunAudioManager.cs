using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GunAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private Dictionary<string, AudioClip> soundClips = new Dictionary<string, AudioClip>();

    public AudioClip loadBulletSound;
    public AudioClip gunCockSound;
    public AudioClip gunShotSound;
    public AudioClip emptyClickSound;
    public AudioClip barrelRollSound;
    public AudioClip bulletDropSound;
    public AudioClip splatEffect;

    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot gunToHeadSnapshot;
    public AudioMixerSnapshot gameOverSnapshot;


    private void OnEnable()
    {
        EventManager.OnBulletLoaded += PlayBulletDropSound;
        EventManager.OnSpinStart += PlayBarrelRollSound;
        EventManager.OnFireGun += PlayGunFiringSound;
    }

    private void OnDisable()
    {
        EventManager.OnBulletLoaded -= PlayBulletDropSound;
        EventManager.OnSpinStart -= PlayBarrelRollSound;
        EventManager.OnFireGun -= PlayGunFiringSound;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        // Store sounds in a dictionary for easy retrieval
        soundClips.Add("loadBullet", loadBulletSound);
        soundClips.Add("gunCock", gunCockSound);
        soundClips.Add("gunShot", gunShotSound);
        soundClips.Add("emptyClick", emptyClickSound);
        soundClips.Add("barrelRoll", barrelRollSound);
        soundClips.Add("bulletDrop", bulletDropSound);
        soundClips.Add("splatEffect", splatEffect);

    }

    private void PlayBulletDropSound(bool loaded)
    {
        if (loaded)
            PlaySound("loadBullet");
    }

    private void PlayBarrelRollSound(bool start)
    {
        Debug.Log("Barrel spiinning");

        if (start)
        {
            PlaySound("barrelRoll");
            StartCoroutine(PlayDelayedSound("gunCock", 2f)); // 0.5 seconds delay
            gunToHeadSnapshot.TransitionTo(2f);
        }
    }
    private void PlayGunFiringSound(bool playerKilled)
    {
        if (playerKilled)
        {
            PlaySound("gunShot");
            //PlaySound("splatEffect");
            gameOverSnapshot.TransitionTo(2f);

        }
        else
        {
            PlaySound("emptyClick");
            StartCoroutine(PlayDelayedSound("bulletDrop", 1f)); // 0.5 seconds delay
            defaultSnapshot.TransitionTo(1f);
        }


    }

    private IEnumerator PlayDelayedSound(string soundKey, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound(soundKey);
    }

    private IEnumerator SwitchBackToDefaultSnapshot(float delay)
    {
        yield return new WaitForSeconds(delay);
        defaultSnapshot.TransitionTo(0.5f); // Switch back smoothly
    }

    public void PlaySound(string soundKey)
    {
        if (soundClips.TryGetValue(soundKey, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound '{soundKey}' not found in GunAudioManager.");
        }
    }
}
