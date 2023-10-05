using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour {
    public static VictoryUI Instance { get; private set; }

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private TMP_Text foodText;


    private void Awake() {
        Instance = this;

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        this.gameObject.SetActive(false);
    }

    public void UpdateText() {
        //TimeSpan time = TimeSpan.FromSeconds(ScoreManager.Instance.Timer);
        //finalTimeText.text = string.Format("{0:00}:{1:00}", (int)time.TotalMinutes, (int)time.Seconds);
        int minutes = (int)ScoreManager.Instance.Timer / 60;
        int seconds = (int)ScoreManager.Instance.Timer - (minutes * 60);
        finalTimeText.text = minutes.ToString("D1") + ":" + seconds.ToString("D2"); ;
        foodText.text = "FOOD: " + ScoreManager.Instance.currentFood + "/" + ScoreManager.Instance.maxFood;
    }
}
