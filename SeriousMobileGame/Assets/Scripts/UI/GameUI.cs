using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour {

    [SerializeField] private TMP_Text foodText;

    private void Start() {
        foodText.text = "FOOD: " + ScoreManager.Instance.currentScore;

        ScoreManager.Instance.OnFoodChange += ScoreManager_OnFoodChange;
    }

    private void ScoreManager_OnFoodChange(object sender, System.EventArgs e) {
        foodText.text = "FOOD: " + ScoreManager.Instance.currentScore;
    }
}
