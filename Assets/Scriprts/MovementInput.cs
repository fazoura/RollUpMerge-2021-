using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public float speed;

    float prevPosition;
    bool pressed;
    float movementOffset;

    float targetX;
    float width;

    private void Start()
    {
        movementOffset = GameManager.instance.movementOffset;
    }

    private void Update()
    {

        if (!GameManager.instance.isStoped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                prevPosition = Input.mousePosition.x;
                pressed = true;

            }

            if (Input.GetMouseButton(0))
            {
                if (pressed)
                {
                    float deltaX = (Input.mousePosition.x - prevPosition) * Time.deltaTime * speed * (1242 / Screen.width);
                    float newPosition = transform.localPosition.x + deltaX;
                    if (newPosition < -movementOffset)
                        newPosition = -movementOffset;
                    else if (newPosition > movementOffset)
                    {
                        newPosition = movementOffset;

                    }

                    targetX = newPosition - transform.localPosition.x;
                    transform.Translate(new Vector3(targetX, 0, 0), Space.Self);
                   

                    prevPosition = Input.mousePosition.x;
                }
                else
                {
                    prevPosition = Input.mousePosition.x;
                    pressed = true;
                }

            }

            else
            {
                pressed = false;
            }
        }
        CorrectPosition();
    }

   public void CorrectPosition()
    {
        width = GameManager.instance.trajectoryWidth;
        targetX = transform.localPosition.x;
        targetX = Mathf.Clamp(targetX, -width, width);
        transform.localPosition = new Vector3(targetX, transform.localPosition.y, transform.localPosition.z);

    }
}
