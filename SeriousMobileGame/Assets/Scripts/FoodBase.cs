using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBase : MonoBehaviour {

    [SerializeField] private int foodAmount = 1;
    [SerializeField] private int foodMax = 5;
    [SerializeField] private float foodCooldown = 1f;

    //[SerializeField] private Sprite depleatedSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float foodTimer = 0f;
    private int foodCount = 0;
    private bool isEating = false;
    


    private void Update() {
        if (foodCount >= foodMax) { return; }

        if (!isEating) { return; }

        if (foodTimer > foodCooldown) {
            ScoreManager.Instance.AddToFood(foodAmount);
            foodCount += foodAmount;
            if (foodCount >= foodMax) { 
                spriteRenderer.sprite = emptySprite; 
            }
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
