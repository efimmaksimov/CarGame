using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int mapSize;
    [SerializeField] private int cooldownRespawn;

    //private List<Enemy> activeEnemies = new List<Enemy>();
    private int enemyCounter;

    void Start()
    {
        StartCoroutine(Respawn());
    }

    private void RespawnEnemy(GameObject prefab)
    {
        Vector3 position = new Vector3(Random.Range(-mapSize, mapSize), 1, Random.Range(-mapSize, mapSize));
        Enemy enemy = Instantiate(prefab, position, Random.rotation, transform).GetComponent<Enemy>();
        enemy.target = player;
        enemyCounter++;
        //activeEnemies.Add(enemy);

    }

    private IEnumerator Respawn()
    {
        for (int j = 0; j < enemyPrefabs.Length; j++)
        {
            for (int i = 0; i < waveConfig.waveDatas[WaveProgress.instance.CurrentWave].enemiesQuantity[j]; i++)
            {
                RespawnEnemy(enemyPrefabs[j]);
                yield return new WaitForSeconds(cooldownRespawn);
            }
        }
    }

    public void OnEnemyDeath()
    {
        Debug.Log("Death");
    }

    public void DeathEnemy()
    {
        enemyCounter--;
        if (enemyCounter == 0)
        {
            Messenger.Broadcast(GameEvents.gameOver);
        }
    }
}
