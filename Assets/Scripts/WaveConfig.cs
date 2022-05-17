using UnityEngine;
[CreateAssetMenu]
public class WaveConfig : ScriptableObject
{
    public WaveData[] waveDatas;
    
}
[System.Serializable]
public class WaveData
{
    public int[] enemiesQuantity;
    public int staticObstacleQuantity;
    public int dynamicObstacleQuantity;
}