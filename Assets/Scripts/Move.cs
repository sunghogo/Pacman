using UnityEngine;

public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public class Move : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _moveVector;
    [SerializeField] private Vector2 _nextMoveVector;
    private Direction _playerDirection;
    private Direction _nextPlayerDirection;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    private LayerMask _obstaclesLayer;  
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _rayLength;
    private bool _isMirroring;
    
    // Box casts a box the size of sprite, shifted rayLength units towards the front
    private RaycastHit2D boxCast(Vector2 direction) {
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2;

        return Physics2D.BoxCast(origin, size, 0f, direction, _rayLength, _obstaclesLayer);
    }
    
    // Checks if there is obstacle rayLength ahead of given direction vector
    private bool isBlocked(Vector2 direction)
    {
        RaycastHit2D hit = boxCast(direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle")) {
            return true;  
        }
        return false;
    }

    void Start() {
        _moveSpeed = 5f;
        _moveVector = _nextMoveVector = Vector2.zero;
        _playerDirection = _nextPlayerDirection = Direction.Right;
        _rayLength = 0.05f;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _obstaclesLayer = LayerMask.GetMask("Obstacles");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isMirroring = false;
    }

    void OnDrawGizmos() {
        if (_rigidbody == null || _spriteRenderer == null) return;
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2;
        Vector2 direction = _nextMoveVector;
        Vector2 endPoint = origin + (direction * _rayLength);

        RaycastHit2D hit = boxCast(direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(origin + direction * hit.distance, size);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(endPoint, size);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Mirror") {
            if (!_isMirroring) transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            _isMirroring = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Mirror") {
            _isMirroring = false;
        }
    }
    
    void FixedUpdate()
    {
        if (GameManager.Instance.isStarted) {
            if (_moveVector == Vector2.zero) {
                _animator.enabled = false;
            } else {
                _animator.enabled = true;
            }

            // Move the player based on the moveDirection and moveSpeed
            if (!isBlocked(_nextMoveVector)) {
                _moveVector = _nextMoveVector;
                _playerDirection = _nextPlayerDirection;
            }
            if (!isBlocked(_moveVector)) transform.Translate(_moveVector * _moveSpeed * Time.deltaTime, 0);
            else _moveVector = Vector2.zero;

            _animator.SetInteger("Direction", (int)_playerDirection);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )
        {
            _nextPlayerDirection = Direction.Up;
            _nextMoveVector = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _nextPlayerDirection = Direction.Down;
            _nextMoveVector = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _nextPlayerDirection = Direction.Left;            
            _nextMoveVector = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _nextPlayerDirection = Direction.Right;            
            _nextMoveVector = Vector2.right;
        }
    }
}
