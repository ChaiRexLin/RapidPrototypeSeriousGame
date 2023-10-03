using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LightEffect : MonoBehaviour
{
    [SerializeField] private float r = 0.8867f;
    [SerializeField] private float g = 0.8758702f;
    [SerializeField] private float b = 0.6901922f;
    private Image LightImage;
    private float distance;
    private float opacity;
    private int LightState = 3;
    private GameObject Player;
    private Transform Car;
    private float min_opacity = 0.1f;
    private float max_opacity = 0.8f;
    private float opacity_multiplier = 0.01f;

    private void Start()
    {
        LightImage = GameObject.FindGameObjectWithTag("LightImage").GetComponent<Image>();
        Car = gameObject.transform.parent;
    }

    private void Update()
    {
        if (LightState == 1)
        {   
            distance = Vector3.Distance(Player.transform.position, Car.position);
            Debug.Log(distance.ToString());
            opacity = max_opacity - opacity_multiplier * distance;
            if (opacity < min_opacity)
            {
                opacity = min_opacity;
            }
            LightImage.color = new Vector4(r, g, b, opacity);
        } else if (LightState == 2)
        {   
            LightImage.color = new Vector4(r, g, b, 0);
            LightState = 3;
        } 

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            Player = other.gameObject;
            LightState = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LightState = 2;
        }
    }
}