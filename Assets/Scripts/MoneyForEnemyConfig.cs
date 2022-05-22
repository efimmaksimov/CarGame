using UnityEngine;
[CreateAssetMenu(
    menuName ="Configs/MoneyForEnemy"
    )]
public class MoneyForEnemyConfig : ScriptableObject
{
    [SerializeField] private int[] moneyForEnemy;

    public int GetMoneyForEnemy(EnemyType type)
    {
        return moneyForEnemy[(int)type];
    }
}
