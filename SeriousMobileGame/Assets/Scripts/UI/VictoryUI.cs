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
        finalTimeText.text = ScoreManager.Instance.Timer + " sec remaining";
        foodText.text = "FOOD: " + ScoreManager.Instance.currentFood + "/" + ScoreManager.Instance.maxFood;
    }
}
