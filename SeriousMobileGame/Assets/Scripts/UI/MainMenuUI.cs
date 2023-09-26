using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button startButton;

    private void Awake() {
        startButton.Select();
        //lambda
        startButton.onClick.AddListener(() => {
            //click
            Loader.Load(Loader.Scene.GameScene);
        });


        Time.timeScale = 1f;
    }



}
