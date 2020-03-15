using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [Range(0.0f, 1.0f)]
    public float followSpeed = 0.5f;
    float xDiff;
    float yDiff;
    float zDiff;
    void Start()
    {
        xDiff = transform.position.x - player.transform.position.x;
        yDiff = transform.position.y - player.transform.position.y;
        zDiff = transform.position.z - player.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + xDiff, player.transform.position.y + yDiff, player.transform.position.z + zDiff), followSpeed);
    }
}
