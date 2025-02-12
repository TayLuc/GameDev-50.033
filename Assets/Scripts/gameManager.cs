using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller theBS;

    public static gameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    public int currentMultiplier;
    public int multiplierTracker; // when to increase to the next multiplier
    public int[] multiplierThreshold;
    // for the UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public AudioSource powerUpSoundSource, breakBlockSoundSource;
    private AudioClip powerUpSoundClip, breakBlockSoundClip;

    // For the end screen
    public GameObject resultsScreen;
    public Text percentHitText, normalHitsText, goodHitsText, perfectHitsText, missedHitsText, rankText, finalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        // Make it so only have one instance of the game manager
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        if (breakBlockSoundClip != null) breakBlockSoundSource.clip = breakBlockSoundClip;
        if (powerUpSoundClip != null) powerUpSoundSource.clip = powerUpSoundClip;


        totalNotes = FindObjectsOfType<coinObject>().Length;

    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                music.Play();
                Invoke("StopAudio", 42f);
            }
        }
        else
        {
            if (!music.isPlaying && !resultsScreen.activeInHierarchy) // to trigger once only
            {
                resultsScreen.SetActive(true);
                normalHitsText.text = normalHits.ToString();
                goodHitsText.text = goodHits.ToString();
                perfectHitsText.text = perfectHits.ToString();
                missedHitsText.text = missedHits.ToString();

                float totalHits = normalHits + goodHits + perfectHits;
                float percentHits = totalHits / totalNotes * 100f;
                percentHitText.text = percentHits.ToString("F2") + "%"; // show as a float to 2 dp (shortcut yay)

                // Set the rank
                string rankValue = "F";
                if (percentHits > 40)
                {
                    rankValue = "D";
                    if (percentHits > 55)
                    {
                        rankValue = "C";
                        if (percentHits > 70)
                        {
                            rankValue = "B";
                            if (percentHits > 85)
                            {
                                rankValue = "A";
                                if (percentHits > 95)
                                {
                                    rankValue = "S";
                                }
                            }
                        }
                    }
                }
                rankText.text = rankValue;
                finalScoreText.text = currentScore.ToString();

            }
        }
    }

    void StopAudio()
    {
        music.Stop();
    }

    public void normalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        normalHits++;
        noteHit();
    }

    public void goodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        goodHits++;
        noteHit();
    }

    public void perfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        perfectHits++;
        noteHit();
    }

    public void noteHit()
    {
        // Debug.Log("Hit on time");

        // keep the multiplier within the threshold
        if (currentMultiplier - 1 < multiplierThreshold.Length)
        {
            multiplierTracker++;
            if (multiplierThreshold[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
                // play power up sound
                powerUpSoundSource.Play();
            }
            multiplierText.text = "Multiplier: x" + currentMultiplier;
            // currentScore += scorePerNote * currentMultiplier;
            scoreText.text = "Score: " + currentScore;
        }
    }

    public void noteMiss()
    {
        Debug.Log("Missed Note");
        // play the block break sound
        breakBlockSoundSource.Play();
        // reset the multiplier
        currentMultiplier = 1;
        multiplierTracker = 0;

        missedHits++;
        multiplierText.text = "Multiplier: x" + currentMultiplier;

    }
}
