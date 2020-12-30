using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    public GameObject player;

    float leftBoundry = -11;
    float rightBoundry = 11;
    float birdPosX;
    float playerPosX;
    float speed = 11;

    public bool moveLeft;

    void Start()
    {
        birdPosX = transform.position.x;
        playerPosX = GameObject.Find("Player").transform.position.x;

        // determines bird movement direction based on spawn position
        if ((birdPosX - playerPosX) < 0)
            moveLeft = false;
        if ((birdPosX - playerPosX) > 0)
            moveLeft = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (!moveLeft)
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        BorderLimit();
    }

    void BorderLimit()
    {
        // destroys bird once they reach the boundry
        if (transform.position.x < leftBoundry || transform.position.x > rightBoundry)
            Destroy(this.gameObject);
        
    }
}
