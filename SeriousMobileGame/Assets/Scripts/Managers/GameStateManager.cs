using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    [SerializeField] private float waitingToStartTimer = 0.5f;

    private enum State {
        WaitingToStart,
        Playing,
        GameOver,
        Victory,
    }
    private State state;

    private void Awake() {
        Instance = this;

        state = State.WaitingToStart;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.Playing;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
        }
    }

    public bool IsWaitingToStart() {
        return state == State.WaitingToStart;
    }
    public bool IsPlaying() {
        return state == State.Playing;
    }
    public bool IsGameOver() {
        return state == State.GameOver;
    }
    public bool IsVictory() {
        return state == State.Victory;
    }


    public void GameOver() {
        state = State.GameOver;
        GameOverUI.Instance.gameObject.SetActive(true);
        SoundManager.Instance.SoundGameOver(Camera.main.transform.position);
        Time.timeScale = 0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Victory() {
        state = State.Victory;
        VictoryUI.Instance.UpdateText();
        VictoryUI.Instance.gameObject.SetActive(true);
        SoundManager.Instance.SoundVictory(Camera.main.transform.position);
        Time.timeScale = 0f;
    }

}
