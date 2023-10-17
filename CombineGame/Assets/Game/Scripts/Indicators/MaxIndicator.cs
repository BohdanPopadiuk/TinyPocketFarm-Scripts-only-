using UnityEngine;

public class MaxIndicator : MonoBehaviour
{
    private Transform targetPos;
    private void Start()
    {
        targetPos = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(new Vector3(targetPos.position.x, transform.position.y, targetPos.position.z));
    }
}
