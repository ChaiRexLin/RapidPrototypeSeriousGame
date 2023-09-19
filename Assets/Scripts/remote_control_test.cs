using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remote_control_test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        Debug.Log(Input.gyro.attitude);
        transform.rotation = Input.gyro.attitude;
        
    }
}