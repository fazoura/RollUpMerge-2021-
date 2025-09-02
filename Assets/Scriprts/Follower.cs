using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Follower : MonoBehaviour
{

    public float speed = 8;
    public float actualSpeed;



    public void Move()
    {

        actualSpeed = speed;
    }

    public void Stop()
    {

        actualSpeed = 0;
    }

    public void Update()
    {
        CalculateTrajectoryWidth();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * actualSpeed*0.01f);
    }


    public void CalculateTrajectoryWidth()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.right, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Trajectory"))
            {
                GameManager.instance.trajectoryWidth = Vector3.Distance(transform.position, hit.point) * 1.7f;
            }

        }
    }
}
