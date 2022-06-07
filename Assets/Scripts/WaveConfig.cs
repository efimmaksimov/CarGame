using UnityEngine;
[CreateAssetMenu(
    menuName = "Configs/Wave"
    )]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private WaveData[] waveDatas;
    [SerializeField] private WaveData lastWaveData;
    public int mapSize;

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
            int additionalEnemies = wave - waveDatas.Length + 1;
            for (int i = 0; i < additionalEnemies; i++)
            {
                int index = (i + waveData.enemiesQuantity.Length) % waveData.enemiesQuantity.Length;
                waveData.enemiesQuantity[index]++;
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