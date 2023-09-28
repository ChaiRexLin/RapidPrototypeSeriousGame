using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        Instance = this;

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        this.gameObject.SetActive(false);
    }
}
