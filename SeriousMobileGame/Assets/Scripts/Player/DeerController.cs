using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour {


    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Rigidbody deerRigidBody;

    private float tilt;

    private void Start() {
        Input.gyro.enabled = true;
    }

    private void Update() {
        //Debug.Log(Input.gyro.attitude);
        //transform.rotation = Input.gyro.attitude;

        tilt += Input.acceleration.x * rotationSpeed * Time.deltaTime;
        //Vector3 movement = transform.forward * movementSpeed * -Input.acceleration.z * Time.deltaTime;
        //deerRigidBody.MovePosition(deerRigidBody.position + movement);



        Quaternion targetRotation = Quaternion.Euler(0, tilt, 0);
        deerRigidBody.rotation = Quaternion.Lerp(deerRigidBody.rotation, targetRotation, Time.deltaTime);
        //deerRigidBody.MoveRotation(Quaternion.Lerp(deerRigidBody.rotation, targetRotation, Time.deltaTime));


        // touch

        if (Input.touchCount > 0 ) {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPositionNormalized = touch.position.normalized;
            Debug.Log(touchPositionNormalized.y - 0.15f);

            if (touchPositionNormalized.x < .8f) { return; }

            Vector3 movement = transform.forward * movementSpeed * (touchPositionNormalized.y - 0.15f) * 2 * Time.deltaTime;

            deerRigidBody.MovePosition(deerRigidBody.position + movement);
        }

    }
}
