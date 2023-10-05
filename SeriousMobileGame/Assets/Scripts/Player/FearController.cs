using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using System;

public class FearController : MonoBehaviour {

    public static FearController Instance { get; private set; }

    //public event EventHandler OnFearChanged;

    [SerializeField] private float range = 5f;
    [SerializeField] private float fearMax = 50f;
    [SerializeField] private float fearMultiplier = 0.2f;
    [SerializeField] private float fearLoss = 1f;
    [SerializeField] private Rigidbody rb;


    private Collider[] colliderArray;
    private float currentFear;
    private int amountOfHumans = 0;

    private void Start() {
        Instance = this;
        currentFear = 0f;
    }

    private void FixedUpdate() {
        
        colliderArray = Physics.OverlapSphere(transform.position, range);
        amountOfHumans = 0;
        foreach (Collider collider in colliderArray) {
            if(collider.gameObject.layer == LayerMask.NameToLayer("Human")) {
                amountOfHumans++;
            }
        }
        if (amountOfHumans == 0) { return; }
        float amountOfFear = amountOfHumans * fearMultiplier * Time.fixedDeltaTime;
        if (currentFear + amountOfFear >= fearMax) {
            currentFear = fearMax;
        } else {
            currentFear += amountOfFear;

        }
        if (currentFear >= fearMax) {
            //Debug.Log("SPOOKED");
            rb.AddForce(Vector3.up * 5);
        }
    }

    private void Update() {
        if (amountOfHumans != 0) { return; }
        currentFear -= fearLoss * Time.deltaTime;
        if (currentFear < 0f)
        {
            currentFear = 0f;
        }
    }

    public float GetNormalizedFear() {
        return currentFear / fearMax;
    }

}
