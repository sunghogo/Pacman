
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float eatDistance;
    void Start()
    {
        eatDistance = 0.25f;   
    }
    private void EatPellet() {
       Destroy(gameObject);
    }
    
    void OnTriggerStay2D(Collider2D other) {
        bool isCollidingWithPacman = other.gameObject.CompareTag("Pacman");
        if (isCollidingWithPacman) {
            Vector2 pacmanPosition = other.gameObject.GetComponent<Collider2D>().bounds.center;
            Vector2 pelletPosition = transform.position;
            bool isWithinDistance = Vector2.Distance(pacmanPosition, pelletPosition) <= eatDistance;
            if (isWithinDistance) EatPellet();
        }
    }
    
}