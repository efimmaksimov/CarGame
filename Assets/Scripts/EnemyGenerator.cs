﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int mapSize;
    [SerializeField] private float cooldownRespawn;
    private Player player;

    private List<Enemy> activeEnemies = new List<Enemy>();
    private int enemyCounter;

    private void Awake()
    {
        Messenger.AddListener(GameEvents.gameOver, DisableEnemies);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.gameOver, DisableEnemies);
    }
    void Start()
    {
        player = FindObjectOfType<Player>(false);
        StartCoroutine(Respawn());
        UnityEngine.Debug.Log(WaveProgress.Instance.CurrentWave);
    }

    private void RespawnEnemy(GameObject prefab)
    {
        Vector3 position = new Vector3(Random.Range(-mapSize, mapSize), 1, Random.Range(-mapSize, mapSize));
        Enemy enemy = Instantiate(prefab, position, Random.rotation, transform).GetComponent<Enemy>();
        enemy.target = player;
        enemyCounter++;
        activeEnemies.Add(enemy);

    }

    private IEnumerator Respawn()
    {
        for (int j = 0; j < enemyPrefabs.Length; j++)
        {
            Debug.Log(waveConfig.GetWaveData(WaveProgress.Instance.CurrentWave).enemiesQuantity[j]);
            for (int i = 0; i < waveConfig.GetWaveData(WaveProgress.Instance.CurrentWave).enemiesQuantity[j]; i++)
            {
                RespawnEnemy(enemyPrefabs[j]);
                yield return new WaitForSeconds(cooldownRespawn);
            }
        }
    }

    public void DeathEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        enemyCounter--;
        if (enemyCounter == 0)
        {
            Messenger.Broadcast(GameEvents.gameOver);
            WaveProgress.Instance.CompleteWave();
        }
    }

    private void DisableEnemies()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            activeEnemies[i].enabled = false;
        }
    }
}
