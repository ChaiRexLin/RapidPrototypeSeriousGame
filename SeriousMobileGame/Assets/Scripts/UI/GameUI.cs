using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Slider fearSlider;


    private void Start() {
        foodText.text = "FOOD: " + ScoreManager.Instance.currentFood + "/" + ScoreManager.Instance.maxFood;

        int minutes = (int)ScoreManager.Instance.Timer / 60;
        int seconds = (int)ScoreManager.Instance.Timer - (minutes * 60);
        timerText.text = minutes.ToString("D1") + ":" + seconds.ToString("D2"); ;

        fearSlider.value = 0f;

        ScoreManager.Instance.OnFoodChange += ScoreManager_OnFoodChange;
    }

    private void ScoreManager_OnFoodChange(object sender, System.EventArgs e) {
        foodText.text = "FOOD: " + ScoreManager.Instance.currentFood + "/" + ScoreManager.Instance.maxFood;
    }

    private void Update() {

        fearSlider.value = FearController.Instance.GetNormalizedFear();
    }

    private void FixedUpdate() {
        int minutes = (int)ScoreManager.Instance.Timer / 60;
        int seconds = (int)ScoreManager.Instance.Timer - (minutes * 60);
        timerText.text = minutes.ToString("D1") + ":" + seconds.ToString("D2"); ;
    }
}
