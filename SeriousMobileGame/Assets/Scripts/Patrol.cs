using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int targetPoint = 0;
    [SerializeField] private float speed = 4;

    public bool isPatroling = true;

    private void Update() {
        if (!isPatroling) { return; }

        if (transform.position == patrolPoints[targetPoint].position) {
            if (targetPoint + 1 >= patrolPoints.Length) {
                targetPoint = 0;
            } else {
                targetPoint++;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }


}
