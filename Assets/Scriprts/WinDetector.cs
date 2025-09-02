using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetector : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") & !GameManager.instance.isStoped)
        {
            GameManager.instance.Stop();
            BallsController.instance.FindLeader();
           
        }
    }
}
