using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationZone : MonoBehaviour
{
    public int number;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Ball>().Multiply(number);
        }
    }
}
