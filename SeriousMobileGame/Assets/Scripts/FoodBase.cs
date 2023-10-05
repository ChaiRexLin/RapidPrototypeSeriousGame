using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBase : MonoBehaviour {

    [SerializeField] private int foodAmount = 1;
    [SerializeField] private int foodMax = 5;
    [SerializeField] private float foodCooldown = 1f;

    [SerializeField] private Sprite emptySprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float foodTimer = 0f;
    private int foodCount = 0;
    public bool isEating = false;
    


    private void Update() {
        foodTimer += Time.deltaTime;
        if (foodCount >= foodMax) { return; }

        if (!isEating) { return; }
        isEating = false;

        if (ScoreManager.Instance.IsFoodFull()) { return; }

        if (foodTimer > foodCooldown) {
            ScoreManager.Instance.AddToFood(foodAmount);
            SoundManager.Instance.SoundEating(transform.position);
            foodCount += foodAmount;
            if (foodCount >= foodMax) { 
                spriteRenderer.sprite = emptySprite; 
            }
            foodTimer = 0f;
            return;
        }
    }
}
