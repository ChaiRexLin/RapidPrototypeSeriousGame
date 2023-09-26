using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBase : MonoBehaviour {

    [SerializeField] private int foodAmount = 1;
    [SerializeField] private float foodCooldown = 1f;

    private float foodTimer = 0f;

    private bool isEating = false;



    private void Update() {
        if (!isEating) { return; }

        if (foodTimer > foodCooldown) {
            ScoreManager.Instance.AddToFood(foodAmount);
            foodTimer = 0f;
            return;
        }

        foodTimer += Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            isEating = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            isEating = false;
        }
    }
}
