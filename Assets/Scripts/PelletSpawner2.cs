using UnityEngine;

public class PelletSpawner2 : MonoBehaviour
{
    public GameObject pelletPrefab;
    private float spacing;
    private Vector2 boxSize;
    private LayerMask obstaclesLayer;
    private LayerMask noSpawnsLayer;


    void GeneratePellets()
    {
        for (int x = 0; x < 28; x++)
        {
            for (int y = 0; y < 29; y++)
            {
                Vector3 position = new Vector3(transform.position.x + x * spacing, transform.position.y + y * spacing, -1);
                bool isValidSpawn = !Physics2D.OverlapBox(position, boxSize, 0f, obstaclesLayer) && !Physics2D.OverlapBox(position, boxSize, 0f, noSpawnsLayer);
                if (isValidSpawn) Instantiate(pelletPrefab, position, Quaternion.identity, transform);
            }
        }
    }

    void Start()
    {
        spacing = 0.5f;
        boxSize = new Vector2(0.5f, 0.5f);
        obstaclesLayer = LayerMask.GetMask("Obstacles");
        noSpawnsLayer = LayerMask.GetMask("No Spawns");
        GeneratePellets();
    }
}
