using UnityEngine;

public class PowerBullet : MonoBehaviour
{
    [SerializeField] private float _eatDistance;
    private Collider2D _collider2D;

    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _eatDistance = 0.25f;   
    }

    private void EatBullet() {
        GameManager.IncrementScore(50);
        Destroy(gameObject);
    }
    
    void OnTriggerStay2D(Collider2D other) {
        bool isCollidingWithPacman = other.gameObject.CompareTag("Pacman");
        if (isCollidingWithPacman) {
            Vector2 pacmanPosition = other.bounds.center;
            Vector2 pelletPosition = _collider2D.bounds.center;
            bool isWithinDistance = Vector2.Distance(pacmanPosition, pelletPosition) <= _eatDistance;
            if (isWithinDistance) EatBullet();
        }
    }
}
