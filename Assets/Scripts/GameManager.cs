using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public bool isStarted { get; private set; }
    
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

    public void StartGame()
    {
        isStarted = true;   
    }
}
