using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMananger : MonoBehaviour
{
    Player playerScript;

    public GameObject suncreen;
    public GameObject seagull;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject timedOutScreen;
    public GameObject sunburntScreen;
    public GameObject startScreen;
    public GameObject finishLine;
    public GameObject player;
    public GameObject powerup;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerBonusText;
    public TextMeshProUGUI sunburnBonusText;
    public TextMeshProUGUI scoreText;

    Vector2 powerupSpawnPos;
    Vector2 birdSpawnPos;

    int timeLeft;
    int totalScore;
    int sunburnBonus;
    int powerupCount;
    int powerupLimit = 3;

    float powerupLeftBoundry = -4.5f;
    float powerupRightBoundry = 4.5f;
    float powerupSpawnX;
    float powerupSpawnY;
    float[] birdSpawnArray = new float[2];
    float birdSpawnY;


    public bool isActive;
    public bool isVictory;
    bool isNoTimeLeft;
    public bool isBurnt;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        // bird spawn locations
        birdSpawnArray[0] = -10;
        birdSpawnArray[1] = 10;


    }

    public void GameStart()
    {
        // starts the game and sets the timer
        startScreen.SetActive(false);
        isActive = true;
        timeLeft = 45;

        // starts spawning birds
        InvokeRepeating("SpawnBirds", 1, Random.Range(1.5f, 3.5f));
        // begins timer countdown 
        InvokeRepeating("Timer", 1, 1);
        // starts spawning powerups
        InvokeRepeating("SpawnPowerup", 5, Random.Range(10, 15));
        // starts sunburn damage
        playerScript.StartSunBurnRate();
    }

    void SpawnBirds()
    {
        // sets spawn position offscreen to left or right of player randomly
        birdSpawnY = player.transform.position.y + Random.Range(3f, 5.5f);
        birdSpawnPos = new Vector2(birdSpawnArray[Random.Range(0, birdSpawnArray.Length)], birdSpawnY);

        if (isActive)
        {
            Instantiate(seagull, birdSpawnPos, seagull.transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        // spawns powerups until the count reches the limit        
        powerupSpawnX = Random.Range(powerupLeftBoundry, powerupRightBoundry);
        powerupSpawnY = player.transform.position.y + 8;

        powerupSpawnPos = new Vector2(powerupSpawnX, powerupSpawnY);

        if (isActive && powerupCount < powerupLimit)
        {
            Instantiate(powerup, powerupSpawnPos, powerup.transform.rotation);
            powerupCount += 1;
        }


    }

    public void GameOver()
    {
        isActive = false;
        gameOverScreen.SetActive(true);

        //shows score if finish line crossed
        if (isVictory)
        {
            timedOutScreen.SetActive(false);
            sunburntScreen.SetActive(false);

            sunburnBonus = 100 - playerScript.sunburnLevel;
            totalScore = timeLeft + sunburnBonus;

            timerBonusText.text = "Timer Bonus: " + timeLeft;
            sunburnBonusText.text = "Sunburn Bonus: " + sunburnBonus;
            scoreText.text = "Total Score: " + totalScore;
            
        }

        // shows timed out text if timer reaches 0
        if (isNoTimeLeft)
        {
            victoryScreen.SetActive(false);
            sunburntScreen.SetActive(false);
        }

        //shows sun burnt text if sunburn level reaches 100
        if(isBurnt)
        {
            victoryScreen.SetActive(false);
            timedOutScreen.SetActive(false);
        }
    }

    void Timer()
    {
        // reduces timer by 1 every second
        if(isActive)
        {
            timeLeft -= 1;
            timerText.text = "Time Left: " + timeLeft;
        }
        else
        {
            timeLeft = timeLeft;
        }

        // end the game if the timer reaches 0
        if (timeLeft == 0)
        {
            isNoTimeLeft = true;
            GameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
