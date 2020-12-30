using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunk : MonoBehaviour
{
    
    float speed = 4;
    float leftBoundry = -5;
    float rightBoundry = 5;    

    bool moveLeft;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HunkSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        Boundries();
        HunkMove();
    }

    void Boundries()
    {
        // reverse movement direction when a hunk reaches a boundry
        if (transform.position.x < leftBoundry)
            speed *= -1;
        if (transform.position.x > rightBoundry)
            speed *= -1;

    }

    void HunkMove()
    {
        if (moveLeft)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (!moveLeft)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    IEnumerator HunkSpeed()
    {
        // switches movement direction after a random interval
        if (moveLeft)
            moveLeft = false;
        if (!moveLeft)
            moveLeft = true;
        speed = 4;
        yield return new WaitForSeconds(Random.Range(0.25f, 2));
        StartCoroutine(Delay());
    }

    
    IEnumerator Delay()
    {
        // pauses movement temporarily in between movement changes
        speed = 0;
        yield return new WaitForSeconds(Random.Range(0.25f, 1));
        StartCoroutine(HunkSpeed());
    }
}
