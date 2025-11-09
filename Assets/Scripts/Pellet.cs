using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] float _eatDistance;
    Collider2D _collider2D;
    public GameState gameState;

    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _eatDistance = 0.25f;
    }

    void EatPellet() {
        GameManager.Instance.IncrementScore(10);
        Destroy(gameObject);
    }
    
    void OnTriggerStay2D(Collider2D other) {
        bool isCollidingWithPacman = other.gameObject.CompareTag("Pacman");
        if (isCollidingWithPacman) {
            Vector2 pacmanPosition = other.bounds.center;
            Vector2 pelletPosition = _collider2D.bounds.center;
            bool isWithinDistance = Vector2.Distance(pacmanPosition, pelletPosition) <= _eatDistance;
            if (isWithinDistance) EatPellet();
        }
    }
}
