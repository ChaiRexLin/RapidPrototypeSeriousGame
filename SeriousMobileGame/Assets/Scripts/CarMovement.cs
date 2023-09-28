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

    [SerializeField] private float carSpeed = 25f;
    [SerializeField] private float detectDistance = 50f;
    [SerializeField] private float slowRate = 20f;
    [SerializeField] private float acceleRate = 15f;

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
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectDistance, mask))
        {
            curSpeed -= slowRate * Time.deltaTime;
            if (curSpeed < 0f) { curSpeed = 0f; }
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
