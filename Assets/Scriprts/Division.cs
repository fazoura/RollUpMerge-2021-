using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Division : MonoBehaviour
{
    public int number;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Ball>().Division(number);
           
        }
    }
}
