using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class tap_raycast : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float tap_threshold = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 inputVector = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(inputVector.x) < tap_threshold && Mathf.Abs(inputVector.y) < tap_threshold)
            {
                tap();
            }
        }
    }

    void tap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "box")
            {
                Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                hit.collider.GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
    }
}
