using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private int mapSize;
    [SerializeField] private int minStaticObstacleSize;
    [SerializeField] private int maxStaticObstacleSize;

    [Space]

    [SerializeField] private GameObject staticObstaclePrefab;
    [SerializeField] private GameObject dynamicObstaclePrefab;

    private void Start()
    {
        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < waveConfig.waveDatas[WaveProgress.instance.CurrentWave].staticObstacleQuantity; i++)
        {
            int scale = Random.Range(minStaticObstacleSize, maxStaticObstacleSize);
            Vector3 position = new Vector3(Random.Range(-mapSize, mapSize), scale / 2, Random.Range(-mapSize, mapSize));
            GameObject staticObstacle = Instantiate(staticObstaclePrefab, position, Quaternion.identity);
            staticObstacle.transform.localScale = new Vector3(scale, scale, scale);
        }

        for (int i = 0; i < waveConfig.waveDatas[WaveProgress.instance.CurrentWave].dynamicObstacleQuantity; i++)
        {
            Vector3 position = new Vector3(Random.Range(-mapSize, mapSize), dynamicObstaclePrefab.transform.localScale.y / 2, Random.Range(-mapSize, mapSize));
            Instantiate(dynamicObstaclePrefab, position, Quaternion.identity);
        }
    }
}