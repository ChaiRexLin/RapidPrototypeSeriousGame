using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeerController : MonoBehaviour
    {


        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float rotationSpeed = 180f;
        [SerializeField] private float jumpPower = 250f;
        [SerializeField] private float foliageMovementMultiplier = 0.8f;
        [SerializeField] private float jumpCooldown = 1f;
        [SerializeField] private Rigidbody deerRigidBody;

        private float tilt;
        private float jumpTimer = 0f;
        private float movementMultiplier = 1f;

        private Vector2 startTouchPosition;
        private Vector2 endTouchPosition;

        private void Start()
        {
            Input.gyro.enabled = true;
        }

        private void Update()
        {
            //Debug.Log(Input.gyro.attitude);
            //transform.rotation = Input.gyro.attitude;

            tilt += Input.acceleration.x * rotationSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0, tilt, 0);
            deerRigidBody.rotation = Quaternion.Lerp(deerRigidBody.rotation, targetRotation, Time.deltaTime);

            if (jumpTimer < jumpCooldown)
            {
                jumpTimer += Time.deltaTime;
                return;
            }

            // touch
            if (Input.touchCount > 0)
            {

                Touch forwardTouch = Input.GetTouch(0);
                Vector2 touchPositionNormalized = new Vector2(forwardTouch.position.x / Screen.width, forwardTouch.position.y / Screen.height);
                //Debug.Log(touchPositionNormalized.y - 0.15f);

                if (touchPositionNormalized.x > .8f)
                {
                    Vector3 movement = transform.forward * movementSpeed * movementMultiplier * (touchPositionNormalized.y - 0.15f) * 2 * Time.deltaTime;

                    deerRigidBody.MovePosition(deerRigidBody.position + movement);
                }

                // draw ray
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch rayTouch = Input.GetTouch(i);
                    if (rayTouch.position.x/ Screen.width < .8f)
                    {
                        Vector3 rayTouchPosition = rayTouch.position;
                        rayTouchPosition.z = 100f;
                        rayTouchPosition = Camera.main.ScreenToWorldPoint(rayTouchPosition);
                        Debug.DrawRay(Camera.main.transform.position, rayTouchPosition - Camera.main.transform.position, Color.blue);

                        if (rayTouch.phase == TouchPhase.Ended)
                        {
                            Ray ray = Camera.main.ScreenPointToRay(rayTouch.position);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit, 10))
                            {
                                Debug.Log(hit.transform.name);
                            }
                        }
                    }

                }

                //swipe

                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch jumpTouch = Input.GetTouch(i);
                    if (jumpTouch.position.x / Screen.width < .8f)
                    {
                        if (jumpTouch.phase == TouchPhase.Began)
                        {
                            startTouchPosition = jumpTouch.position;
                        }
                        if (jumpTouch.phase == TouchPhase.Ended)
                        {
                            endTouchPosition = jumpTouch.position;
                            if (endTouchPosition.y - startTouchPosition.y > 300f)
                            {
                                deerRigidBody.AddForce(Vector3.up * jumpPower);
                                jumpTimer = 0f;
                            }
                        }
                    }
                }

            }

        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Foliage"))
            {
                movementMultiplier = foliageMovementMultiplier;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Foliage"))
            {
                movementMultiplier = 1f;
            }
        }
    }


       