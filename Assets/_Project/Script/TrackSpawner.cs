using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public GameObject[] roadPrefabs; // Straight, Slope, Curve
    public Transform player;

    public float roadLength = 40f;
    public int startSegments = 3;

    float spawnZ = 0f;

    void Start()
    {
        for (int i = 0; i < startSegments; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        if (player.position.z > spawnZ - (startSegments * roadLength))
        {
            SpawnRoad();
        }
    }

    void SpawnRoad()
    {
        int index = Random.Range(0, roadPrefabs.Length);
        Instantiate(roadPrefabs[index], new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += roadLength;
    }
}
