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
        Debug.Log(PlayerStats.instance.Damage);
        Debug.Log(PlayerStats.instance.Health);
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < waveConfig.GetWaveData(WaveProgress.Instance.CurrentWave).staticObstacleQuantity; i++)
        {
            int scale = Random.Range(minStaticObstacleSize, maxStaticObstacleSize);
            Vector3 position = new Vector3(Random.Range(-mapSize, mapSize), scale / 2, Random.Range(-mapSize, mapSize));
            GameObject staticObstacle = Instantiate(staticObstaclePrefab, position, Quaternion.identity);
            staticObstacle.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
