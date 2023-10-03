using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

    [SerializeField] private float range = 5f;
    [SerializeField] private Patrol patrol;

    private Collider[] colliderArray;

    private void FixedUpdate() {

        colliderArray = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in colliderArray) {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
                patrol.isPatroling = false; 
                return;
            }

        }
        patrol.isPatroling = true;
    }
}
