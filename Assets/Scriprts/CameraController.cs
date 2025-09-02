using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    float offset;
    float newZ;

    void Start()
    {
        offset = transform.position.z - target.position.z;
        Application.targetFrameRate = 60;
    }

    private void LateUpdate()
    {
        newZ = target.transform.position.z + offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
        offset= transform.position.z - target.position.z;
    }
}
