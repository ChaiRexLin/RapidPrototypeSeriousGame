using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapControl : MonoBehaviour
{
    private float eatRange = 2.5f;

    

    private void Start()
    {
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        // touch
        if (Input.touchCount > 0)
        {
            // draw ray
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch rayTouch = Input.GetTouch(i);
                if (rayTouch.position.x / Screen.width < .8f)
                {
                    Vector3 rayTouchPosition = rayTouch.position;
                    rayTouchPosition.z = 100f;
                    rayTouchPosition = Camera.main.ScreenToWorldPoint(rayTouchPosition);
                    Debug.DrawRay(Camera.main.transform.position, rayTouchPosition - Camera.main.transform.position, Color.blue);
                    if (rayTouch.phase == TouchPhase.Ended)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(rayTouch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, eatRange))
                        {
                            hit.transform.GetComponent<FoodBase>().isEating = true;
                        }
                    }
                }

            }
        }

    }
}


