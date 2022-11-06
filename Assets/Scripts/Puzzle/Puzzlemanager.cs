using UnityEngine;

public class Puzzlemanager : MonoBehaviour
{
    public static Puzzlemanager Instance { get; private set; }

    [SerializeField]
    private int solvedPuzzlesToWin;

    private int solvedPuzzles;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PuzzleSolved()
    {
        solvedPuzzles++;
        if (solvedPuzzles >= solvedPuzzlesToWin)
        {
            GameWon();
        }
    }

    public void GameWon()
    {
        GameManager.Instance.UpdateGameState(GameState.Won);
    }

}
