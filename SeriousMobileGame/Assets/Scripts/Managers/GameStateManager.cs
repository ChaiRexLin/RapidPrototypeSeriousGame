using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private enum State {
        WaitingToStart,
        Playing,
        GameOver,
    }
    private State state;

    private void Awake() {
        Instance = this;

        state = State.WaitingToStart;
    }

    public bool IsWaitingToStart() {
        return state == State.WaitingToStart;
    }
    public bool IsPlaying() {
        return state == State.WaitingToStart;
    }

    public void EndGame() {
        state = State.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
        //EndUI.Instance.gameObject.SetActive(true);
        Time.timeScale = 0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
