using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    private float playerX;
    private float playerY;
    private float playerZ;  

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerX = player.transform.position.x + offset.x;
        playerY = player.transform.position.y + offset.y;
        playerZ = player.transform.position.z + offset.z; 

        transform.position = new Vector3(playerX, playerY, offset.z);
    }
}
