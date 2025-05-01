using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static uint score { get; private set;}
    public static uint highScore { get; private set;}
    public static bool isStarted { get; private set; }

    public static void StartGame()
    {
        isStarted = true;   
    }

    public static void IncrementScore(uint n) {
        score += n;
    }

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            isStarted = false;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        } else {
            Destroy(gameObject); // Destroy any duplicate GameManager instances
        }
    }

    void Start() {
        score = highScore = 0;
    }

    void FixedUpdate() {
        if (score > highScore) highScore = score;
    }
}
