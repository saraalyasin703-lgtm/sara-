using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public Transform player;          // اللاعب
    public GameObject trackPrefab;    // قطعة الطريق

    public float trackLength = 30f;   // طول القطعة
    public int startTracks = 5;       // عدد القطع بالبداية

    private float spawnZ = 0f;        // مكان إنشاء الطريق

    void Start()
    {
        for (int i = 0; i < startTracks; i++)
        {
            SpawnTrack();
        }
    }

    void Update()
    {
        if (player.position.z > spawnZ - (trackLength * 2))
        {
            SpawnTrack();
        }
    }

    void SpawnTrack()
    {
        Instantiate(trackPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += trackLength;
    }
}
