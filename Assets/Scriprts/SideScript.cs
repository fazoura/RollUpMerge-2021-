using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            if (!ball.isMerging & !ball.removed)
            {
                BallsController.instance.RemoveBall(ball);
                Destroy(ball.gameObject, 3f);
                print(other.name);
            }
        }
    }
}
