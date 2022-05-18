using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private int[] coinsForEnemy;

    private int coins;
    private void Awake()
    {
        Messenger<EnemyType>.AddListener(GameEvents.enemyDeath, OnEnemyDeath);
    }

    private void OnDestroy()
    {
        Messenger<EnemyType>.RemoveListener(GameEvents.enemyDeath, OnEnemyDeath);
    }

    public void OnEnemyDeath(EnemyType type)
    {
        coins += coinsForEnemy[(int)type];
        Debug.Log("point1");
    }

    public int GetReward()
    {
        Debug.Log("point2");
        return coins;
    }
}
