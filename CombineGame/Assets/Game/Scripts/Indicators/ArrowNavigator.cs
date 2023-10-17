using UnityEngine;

public class ArrowNavigator : MonoBehaviour
{
    private Vector3 targetPos;
    private void Start()
    {
        targetPos = GameObject.FindWithTag("StackingPlace").transform.position;
        targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
    }

    void Update()
    {
        transform.LookAt(targetPos);
    }
}
