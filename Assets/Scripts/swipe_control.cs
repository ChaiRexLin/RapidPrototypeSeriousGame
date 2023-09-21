using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class swipe_control : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float jump_threshold = 200f;
    private float jump_force = 300f;
    private Rigidbody character_RB;

    private void Start()
    {
        character_RB = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {   
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 inputVector = endTouchPosition - startTouchPosition;
            //Debug.Log(inputVector.x);
            //Debug.Log(inputVector.y);
            if (Mathf.Abs(inputVector.y) > Mathf.Abs(inputVector.x))
            {
                if (inputVector.y > jump_threshold)
                {
                    jump();
                }
            }
        }

    }

    private void jump()
    {
        character_RB.AddForce(Vector3.up * jump_force);
        Debug.Log("jump");
    }
}