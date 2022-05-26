using UnityEngine;

public abstract class PlayerUpgradeConfig : ScriptableObject
{
    public string id;
    [SerializeField] protected int basePrise;
    [SerializeField] protected float additionalPrice;

    public abstract IPlayerUpgrade InstantiateUpgrade();


    public int GetPrice(int level)
    {
        //return (int)(basePrise * Mathf.Pow(multiplier, level));
        return (int)(basePrise + additionalPrice * level);
    }
}
