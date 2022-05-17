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
    }

    public int GetReward()
    {
        return coins;
    }
}
