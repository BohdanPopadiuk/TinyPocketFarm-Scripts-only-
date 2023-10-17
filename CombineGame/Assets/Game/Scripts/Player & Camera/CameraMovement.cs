using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private GameConfig gameConfig;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameConfig = Globals.instance.gameConfig;
        transform.localEulerAngles = gameConfig.cameraAngle;
    }
    
    void Update()
    {
        MoveToTarget();
        
        if (Globals.instance.devMode)
        {
            transform.localEulerAngles = gameConfig.cameraAngle;
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + gameConfig.cameraOffset,
            gameConfig.cameraSmooth * Time.deltaTime);
    }
}
