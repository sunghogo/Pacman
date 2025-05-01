using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector2 _direction;
    private Vector2 _nextDirection; 

    [SerializeField] private Animator _animator;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _rayLength;
    private LayerMask _obstaclesLayer;
    private bool _canTeleport;

    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = 5F;
        _direction = Vector2.zero;
        _nextDirection = Vector2.zero;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rayLength = 0.05F;
        _obstaclesLayer = LayerMask.GetMask("Obstacles");
        _canTeleport = true;
    }

    private RaycastHit2D boxCast(Vector2 direction) {
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position; // if pacman is offset

        return Physics2D.BoxCast(origin, size, 0f, direction, _rayLength, _obstaclesLayer);
    }

    private bool isColliding(Vector2 direction) {
        RaycastHit2D hit = boxCast(direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle")) return true;
        return false;
    }

    void OnDrawGizmos() {
        if (_rigidbody == null || _spriteRenderer == null) return;
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position; // if pacman is offset
        Vector2 endPoint = origin + (_direction * _rayLength);

        RaycastHit2D hit = boxCast(_direction);
        if (isColliding(_direction))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(origin + _direction * hit.distance, size);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(endPoint, size);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Mirror") && _canTeleport) {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            _canTeleport = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Mirror")) {
            _canTeleport = true;
        }
    }

    void FixedUpdate()
    {        
        if (!isColliding(_nextDirection)) _direction = _nextDirection;
        
        if (_direction == Vector2.up) {
            _animator.SetBool("Up", true);
            _animator.SetBool("Down", false);
            _animator.SetBool("Left", false);
            _animator.SetBool("Right", false);
        }
        if (_direction == Vector2.down) {
            _animator.SetBool("Up", false);
            _animator.SetBool("Down", true);
            _animator.SetBool("Left", false);
            _animator.SetBool("Right", false);
        }
        if (_direction == Vector2.left) {
            _animator.SetBool("Up", false);
            _animator.SetBool("Down", false);
            _animator.SetBool("Left", true);
            _animator.SetBool("Right", false);
        }
        if (_direction == Vector2.right) {
            _animator.SetBool("Up", false);
            _animator.SetBool("Down", false);
            _animator.SetBool("Left", false);
            _animator.SetBool("Right", true);
        }
        if (!isColliding(_direction)) transform.Translate(_direction * _moveSpeed * Time.deltaTime); // (0, 1)
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            _nextDirection = Vector2.up;
        }
        if (Input.GetKey(KeyCode.S ) || Input.GetKey(KeyCode.DownArrow)) {
            _nextDirection = Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _nextDirection = Vector2.right;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _nextDirection = Vector2.left;
        }   
    }
}
