using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private int minStaticObstacleSize;
    [SerializeField] private int maxStaticObstacleSize;
    [SerializeField] private Vector3 centerOfPlayerZone;
    [SerializeField] private float radius;
    [SerializeField] private Transform[] walls;
    [SerializeField] private Transform floor;

    [Space]

    [SerializeField] private GameObject staticObstaclePrefab;

    private WaveData currentWaveData;
    private int mapSize;

    private void Awake()
    {
        currentWaveData = waveConfig.GetWaveData(WaveProgress.Instance.CurrentWave);
        mapSize = waveConfig.mapSize;
        GenerateWalls();
        GenerateObstacles();
#if !UNITY_EDITOR
        YandexMetrica.EventStartWave(WaveProgress.Instance.CurrentWave);
#endif
    }

    private void GenerateWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].localScale = new Vector3(mapSize + 1, 2, 1);
        }
        walls[0].position = new Vector3(mapSize / 2, 1, 0);
        walls[1].position = new Vector3(-mapSize / 2, 1, 0);
        walls[2].position = new Vector3(0, 1, mapSize / 2);
        walls[3].position = new Vector3(0, 1, -mapSize / 2);
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < currentWaveData.staticObstacleQuantity; i++)
        {
            int scale = Random.Range(minStaticObstacleSize, maxStaticObstacleSize);
            Vector3 position;
            do
            {
                position = new Vector3(Random.Range(-mapSize / 2 + minStaticObstacleSize, mapSize / 2 - minStaticObstacleSize), 
                    scale / 2, Random.Range(-mapSize / 2 + minStaticObstacleSize, mapSize / 2 - minStaticObstacleSize));
            }
            while (Vector3.Distance(position, centerOfPlayerZone) < radius);
            GameObject staticObstacle = Instantiate(staticObstaclePrefab, position, Quaternion.identity);
            staticObstacle.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
