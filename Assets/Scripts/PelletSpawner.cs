using UnityEngine;

public class PelletSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _pelletPrefab;
    [SerializeField] private GameObject _powerBulletPrefab;
    private int _gridWidth;
    private int _gridHeight;
    private float _spacing;
    private Vector2 _boxSize;
    private LayerMask _obstaclesLayer;
    private LayerMask _noSpawnsLayer;


    // Start is called before the first frame update
    void Start()
    {
        _gridWidth = 26;
        _gridHeight = 29;
        _spacing = 0.5f;
        _boxSize = new Vector2(0.5f, 0.5f);
        _obstaclesLayer = LayerMask.GetMask("Obstacles");
        _noSpawnsLayer = LayerMask.GetMask("No Spawns");
        GeneratePellets();
    }

    void GeneratePellets()
    {
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                Vector3 position = new Vector3(transform.position.x + x * _spacing, transform.position.y + y * _spacing, -1);
                GameObject prefab = ((x == 0 || x == _gridWidth - 1) && (y == 6 || y == _gridHeight - 3)) ? _powerBulletPrefab : _pelletPrefab;
                bool isValidSpawn = !Physics2D.OverlapBox(position, _boxSize, 0f, _obstaclesLayer) && !Physics2D.OverlapBox(position, _boxSize, 0f, _noSpawnsLayer);
                if (isValidSpawn) Instantiate(prefab, position, Quaternion.identity, transform);
            }
        }
    }
}