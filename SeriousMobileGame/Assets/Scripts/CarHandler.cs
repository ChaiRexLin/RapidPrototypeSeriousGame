using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CarHandler : MonoBehaviour {

    [SerializeField] private float minTimeInterval = 2f;
    [SerializeField] private float maxTimeInterval = 3f;
    private float timeBetweenCars;
    [SerializeField] private int maxCars = 5;

    [SerializeField] private GameObject[] startTransforms;
    [SerializeField] private GameObject[] endTransforms;
    [SerializeField] private GameObject carPrefab;

    private int num_of_lane;

    private float carTimer;

    private GameObject[] carArray;

    private int currentAmountOfCars = 0;


    private void Start() {
        carArray = new GameObject[maxCars];
        timeBetweenCars = Random.Range(minTimeInterval, maxTimeInterval);
        num_of_lane = startTransforms.Length;
    }


    private void Update() {
        if (currentAmountOfCars >= maxCars) { return; }

        if (carTimer < timeBetweenCars) {
            carTimer += Time.deltaTime;
            return;
        }
        int lane = currentAmountOfCars % num_of_lane;
        carArray[currentAmountOfCars] = Instantiate(carPrefab, startTransforms[lane].transform.position, startTransforms[lane].transform.rotation);
        carArray[currentAmountOfCars].GetComponent<CarMovement>().startTransform = startTransforms[lane].transform;
        carArray[currentAmountOfCars].GetComponent<CarMovement>().endTransform = endTransforms[lane].transform;
        currentAmountOfCars++;
        carTimer = 0;
        timeBetweenCars = Random.Range(minTimeInterval, maxTimeInterval);
    }


}
