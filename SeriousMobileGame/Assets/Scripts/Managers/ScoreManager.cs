using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }
    public int currentScore { get; private set; }

    public event EventHandler OnFoodChange;

    private void Awake() {
        Instance = this;
    }

    public void AddToFood(int foodGained) {
        currentScore += foodGained;
        OnFoodChange?.Invoke(this, EventArgs.Empty);
    }

}
