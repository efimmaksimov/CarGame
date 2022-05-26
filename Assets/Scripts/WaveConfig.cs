using System;
using UnityEngine;
[CreateAssetMenu(
    menuName = "Configs/Wave"
    )]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private WaveData[] waveDatas;
    [SerializeField] private WaveData lastWaveData;

    public WaveData GetWaveData(int wave)
    {
        if (waveDatas.Length > wave)
        {
            return waveDatas[wave];
        }
        else
        {
            WaveData waveData = new WaveData();
            waveData.enemiesQuantity = (int[])lastWaveData.enemiesQuantity.Clone();
            waveData.staticObstacleQuantity = lastWaveData.staticObstacleQuantity;
            for (int i = 0; i < waveData.enemiesQuantity.Length; i++)
            {
                waveData.enemiesQuantity[i] += wave - waveDatas.Length + 1;
            }
            return waveData;
        }
    }
    
}
[System.Serializable]
public struct WaveData
{
    public int[] enemiesQuantity;
    public int staticObstacleQuantity;
}