using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    [SerializeField]
    public Canvas titleMenu;
    [SerializeField]
    public Transform ghost;
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

    [SerializeField]
    Vector3 ofs;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            UpdateGameState(GameState.Paused);
        }
        if (IsGameRunning()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                ghost.position = hit.point + ofs;
                //ghost.position = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z + 0.5f);
            }
        }

    }



}

public enum GameState {
    TitleScreen,
    Playing,
    Paused,
    Won,
}