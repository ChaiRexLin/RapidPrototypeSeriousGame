using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class CarMovement : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;
    private float curSpeed;
    private int mask;
    private float slowTimer;

    [SerializeField] private float carSpeed = 25f;
    [SerializeField] private float detectDistance = 50f;
    [SerializeField] private float slowRate = 20f;
    [SerializeField] private float acceleRate = 15f;
    [SerializeField] private float slowDuration = 1f;
    [SerializeField] private float detectRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        int player = 1 << LayerMask.NameToLayer("Player");
        int car = 1 << LayerMask.NameToLayer("Car");
        mask = player | car;
        curSpeed = carSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * detectDistance, Color.yellow);
        if (Physics.SphereCast(transform.position, detectRadius, transform.forward, out hit, detectDistance, mask))
        {
            slowTimer = slowDuration;
            curSpeed -= slowRate * Time.deltaTime;
            if (curSpeed < 0f) { curSpeed = 0f; }
        }
        else if (slowTimer > 0f)
        {
            curSpeed -= slowRate * Time.deltaTime;
            if (curSpeed < 0f) { curSpeed = 0f; }
            slowTimer -= Time.deltaTime;
        }
        else
        {
            curSpeed += acceleRate * Time.deltaTime;
            if (curSpeed > carSpeed) { curSpeed = carSpeed; }
        }
        transform.position = Vector3.MoveTowards(transform.position, endTransform.position, curSpeed * Time.deltaTime);
        if (transform.position == endTransform.position)
        {
            transform.position = startTransform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameStateManager.Instance.GameOver();
        }
    }
}
