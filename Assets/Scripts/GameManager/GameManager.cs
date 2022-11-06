using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    [SerializeField]
    public Canvas titleMenu;
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        UpdateGameState(GameState.TitleScreen);
    }
    public GameState State { get; set; }

    public static event Action<GameState> OnGameStateChanged;
    public void UpdateGameState(GameState newState) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        State = newState;
        switch (newState) {
            case GameState.TitleScreen:
                titleMenu.enabled = true;
                break;
            case GameState.Playing:
                titleMenu.enabled = false;
                break;
            case GameState.Paused:
                titleMenu.enabled = true;
                break;
            case GameState.Won:
                titleMenu.enabled = true;
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
    public bool IsGameRunning() {
        return State == GameState.Playing;
    }

    public void onStartGame() {
        UpdateGameState(GameState.Playing);
    }

    public void onContinueGame() {
        UpdateGameState(GameState.Playing);
    }

    public void onExitGame() {
        Console.WriteLine("EXIT");
        Application.Quit();
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            UpdateGameState(GameState.Paused);
        }
    }
}

public enum GameState {
    TitleScreen,
    Playing,
    Paused,
    Won,
}