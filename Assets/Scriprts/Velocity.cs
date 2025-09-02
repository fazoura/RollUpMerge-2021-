using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{

    public float velocityX;
    public float velocityZ;

    Vector3 oldPosition;

    private void Start()
    {
        oldPosition = transform.position;
    }

    private void FixedUpdate()
    {
        velocityX = transform.position.x - oldPosition.x;
        velocityZ = transform.position.z - oldPosition.z;

        oldPosition = transform.position;
    }
}
