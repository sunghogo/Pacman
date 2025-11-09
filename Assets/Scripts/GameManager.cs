using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public uint Score { get; private set;}
    public uint HighScore { get; private set;}
    public bool IsStarted { get; private set; }
    public GameState gameState;

    public void StartGame()
    {
        IsStarted = true;
        Score = 0;
        HighScore = gameState.highScore;
    }

    public void IncrementScore(uint n) {
        Score += n;
        if (Score > HighScore)
        {
            HighScore = Score;
            gameState.highScore = HighScore;
        }
    }

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            IsStarted = false;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        } else {
            Destroy(gameObject); // Destroy any duplicate GameManager instances
        }
    }

    void Start() {
        Score = HighScore = 0;
    }

    void FixedUpdate() {
        if (Score > HighScore) HighScore = Score;
    }
}
