using UnityEngine;

public class PelletSpawner2 : MonoBehaviour
{
    public GameObject pelletPrefab;

    void GeneratePellet() {
        for (int i = 0; i < 10; i++) {
            Vector3 position = new Vector3(transform.position.x + i, transform.position.y , -1);
            Instantiate(pelletPrefab, position, Quaternion.identity);
        }
    }

    void Start()
    {
        GeneratePellet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
