using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public float spawnRate = 2f;
    public float spawnRateDecrement = 0.1f;
    public float minSpawnRate = 0.5f;

    private float _nextSpawnTime;

    private bool spawn = true;
    [SerializeField]private Transform player;
    [SerializeField]private SequenceManagerForSurvivors
        sequenceManagerForSurvivors;

    private void Start()
    {
        _nextSpawnTime = Time.time + spawnRate;
        sequenceManagerForSurvivors.OnErrorGlitch += () => { spawn = false; };
    }

    private void Update()
    {
        if(!spawn) return;
        if (Time.time > _nextSpawnTime)
        {
            SpawnZombie();
            _nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>().SetPlayer(player);

        if (spawnRate > minSpawnRate)
        {
            spawnRate -= spawnRateDecrement;
        }
    }
}
