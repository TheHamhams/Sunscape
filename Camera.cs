using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        transform.position = new Vector3(0, player.position.y, -10);
    }
}
