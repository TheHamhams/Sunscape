using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    GameMananger gameManangerScript;

    public GameObject finishLine;

    public TextMeshProUGUI sunburnText;

    Rigidbody2D rigidbody2d;

    public int sunburnLevel;
    int sunburnRate = 1;
    int powerupStrength = 10;

    float horizontal;
    float vertical;
    float speed = 7;
    float hitTime = 0.5f;
    

    public bool isShade;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        gameManangerScript = GameObject.Find("Game Manager").GetComponent<GameMananger>();
        
        // starting sunburn level
        sunburnLevel = 00;

    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerMove();
     
        // shows sunburn damage on game UI
        sunburnText.text = "Sunburn Level: " + sunburnLevel + "%";
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        
        if (gameManangerScript.isActive)
        {
            // determines player speed and position while game is active
            position.x = position.x + (speed * Time.fixedDeltaTime * horizontal);
            position.y = position.y + (speed * Time.fixedDeltaTime * vertical);
        }
        else
        {
            //stops player movement while game is not active
            position.x = transform.position.x;
            position.y = transform.position.y;
        }
        rigidbody2d.MovePosition(position);
    }

    void PlayerMove()
    {
        // moves player using Unity Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // stop moving for a short period after hitting an enemy
        if (collision.tag == "Enemy")
        {
            StartCoroutine(EnemyHit());
        }

        // if player comes in contact with a shade object, stop sunburn damage
        if (collision.tag == "Shade")
        {
            isShade = true;
        }

        // lowers sunburn level when player collides with powerup
        if (collision.tag == "Powerup")
        {
            if (sunburnLevel < powerupStrength)
            {
                sunburnLevel = 0;
            }
            else
            {
                sunburnLevel -= powerupStrength;
            }

            Destroy(collision.gameObject);
        }

        // ends game when the player reaches the finish line
        if (collision.tag == "Finish Line")
        {
            gameManangerScript.isVictory = true;
            gameManangerScript.GameOver();
        }
    }

    IEnumerator EnemyHit()
    {
        // temporarily sets player speed to 0 when an enemy is hit
        speed = 0;
        yield return new WaitForSeconds(hitTime);
        speed = 7;
    }

    void SunBurnDamage()
    {
        // player takes damage while not in shade
        if (!isShade && gameManangerScript.isActive)
        {
            if (sunburnLevel < 100)
            {
                sunburnLevel += sunburnRate;
            }
            else
            {
                // end the game if sunburn level reaches 100
                sunburnLevel = 100;
                gameManangerScript.isBurnt = true;
                gameManangerScript.GameOver();
            }
        }
    }

    public void StartSunBurnRate()
    {
        // sets the rate at which player takes sunburn damage
        InvokeRepeating("SunBurnDamage", 0.25f, 0.25f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // restarts sun damage once player leaves shadded area
        if (collision.tag == "Shade")
            isShade = false;
    }

    
}
