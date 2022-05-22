using UnityEngine;
[CreateAssetMenu(
    menuName = "Configs/Wave"
    )]
public class WaveConfig : ScriptableObject
{
    public WaveData[] waveDatas;
    
}
[System.Serializable]
public class WaveData
{
    public int[] enemiesQuantity;
    public int staticObstacleQuantity;
}