using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject segmentPrefab;

    public float segmentLength = 30f;
    public int startSegments = 8;
    public float spawnAheadDistance = 90f;
    public int maxSegmentsAlive = 12;

    private float nextSpawnZ = 0f;
    private Queue<GameObject> spawned = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < startSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if (player != null && player.position.z + spawnAheadDistance > nextSpawnZ)
        {
            SpawnSegment();
        }
    }

    void SpawnSegment()
    {
        if (segmentPrefab == null) return;

        Vector3 pos = new Vector3(0f, 0f, nextSpawnZ);
        GameObject seg = Instantiate(segmentPrefab, pos, Quaternion.identity);

        spawned.Enqueue(seg);
        nextSpawnZ += segmentLength;

        if (spawned.Count > maxSegmentsAlive)
        {
            Destroy(spawned.Dequeue());
        }
    }
}
