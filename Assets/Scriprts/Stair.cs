using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public int index;
    public Transform target;

    private void Awake()
    {
        target = transform.GetChild(1);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball.number == index)
            {
                ball.Replaced(target);
                print("Stair");
            }
            else
            {

            }
        }
    }
}
