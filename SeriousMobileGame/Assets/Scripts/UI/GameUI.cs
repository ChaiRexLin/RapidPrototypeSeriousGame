using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField] private TMP_Text foodText;
    [SerializeField] private Slider fearSlider;


    private void Start() {
        foodText.text = "FOOD: " + ScoreManager.Instance.currentFood + "/" + ScoreManager.Instance.maxFood;
        fearSlider.value = FearController.Instance.GetNormalizedFear();

        ScoreManager.Instance.OnFoodChange += ScoreManager_OnFoodChange;
    }

    private void ScoreManager_OnFoodChange(object sender, System.EventArgs e) {
        foodText.text = "FOOD: " + ScoreManager.Instance.currentFood + "/" + ScoreManager.Instance.maxFood;
    }

    private void Update() {
        fearSlider.value = FearController.Instance.GetNormalizedFear();

    }
}
