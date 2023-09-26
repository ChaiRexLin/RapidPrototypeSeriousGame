using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CarHandler : MonoBehaviour {


    [SerializeField] private float timeBetweenCars = 2f;
    [SerializeField] private int maxCars = 5;
    [SerializeField] private float carSpeed = 25f;

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private GameObject carPrefab;


    private float carTimer;

    private GameObject[] carArray;

    private int currentAmountOfCars = 0;


    private void Start() {
        carArray = new GameObject[maxCars];
    }


    private void Update() {


        if (currentAmountOfCars > 0) {
            for (int i = 0; i < currentAmountOfCars; i++) {
                carArray[i].transform.position = Vector3.MoveTowards(carArray[i].transform.position, endTransform.position, carSpeed * Time.deltaTime);
                if (carArray[i].transform.position == endTransform.position) {
                    carArray[i].transform.position = startTransform.position;
                }
            }
        }

        if (currentAmountOfCars >= maxCars) { return; }

        if (carTimer < timeBetweenCars) {
            carTimer += Time.deltaTime;
            return;
        }

        carArray[currentAmountOfCars] = Instantiate(carPrefab, startTransform.position, Quaternion.Euler(0, 0, 90), this.transform);
        currentAmountOfCars++;
        carTimer = 0;

    }


}
