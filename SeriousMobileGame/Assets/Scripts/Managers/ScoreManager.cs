using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }

    [SerializeField] public int maxFood = 10;
    [SerializeField] public float maxTimeSeconds = 300;

    public int currentFood { get; private set; }

    public event EventHandler OnFoodChange;

    public float Timer = 0;

    private void Awake() {
        Instance = this;
        Timer = maxTimeSeconds;
    }

    public void AddToFood(int foodGained) {
        if (currentFood + foodGained >= maxFood) {
            currentFood = maxFood;
        } else {
            currentFood += foodGained;
        }
        OnFoodChange?.Invoke(this, EventArgs.Empty);
    }

    public bool IsFoodFull() {
        return currentFood == maxFood;
    }

    private void Update() {
        if (!GameStateManager.Instance.IsPlaying()) { return; }
        Timer -= Time.deltaTime;
        if (Timer <= 0) { GameStateManager.Instance.GameOver(); }
    }

}
