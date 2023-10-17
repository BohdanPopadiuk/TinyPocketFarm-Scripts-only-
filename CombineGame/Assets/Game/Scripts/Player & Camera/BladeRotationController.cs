using UnityEngine;

public class BladeRotationController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    void Update()
    {
        if (playerController.rotateBlade)
            transform.Rotate(0, 0, -Globals.instance.gameConfig.bladeSpeedRotation * Time.deltaTime);
    }
}
