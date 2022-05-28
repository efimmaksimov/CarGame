using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private int mapSize;
    [SerializeField] private int minStaticObstacleSize;
    [SerializeField] private int maxStaticObstacleSize;
    [SerializeField] private Vector3 centerOfPlayerZone;
    [SerializeField] private float radius;

    [Space]

    [SerializeField] private GameObject staticObstaclePrefab;

    private void Start()
    {
        GenerateObstacles();
#if !UNITY_EDITOR
        YandexMetrica.EventStartWave(WaveProgress.Instance.CurrentWave);
#endif
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < waveConfig.GetWaveData(WaveProgress.Instance.CurrentWave).staticObstacleQuantity; i++)
        {
            int scale = Random.Range(minStaticObstacleSize, maxStaticObstacleSize);
            Vector3 position;
            do
            {
                position = new Vector3(Random.Range(-mapSize, mapSize), scale / 2, Random.Range(-mapSize, mapSize));
            }
            while (Vector3.Distance(position, centerOfPlayerZone) < radius);
            GameObject staticObstacle = Instantiate(staticObstaclePrefab, position, Quaternion.identity);
            staticObstacle.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
