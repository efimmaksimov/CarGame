using UnityEngine;
[CreateAssetMenu(
    fileName = "SkinsConfig",
    menuName = "Skins/New SkinsConfig"
    )]
public class SkinsConfig : ScriptableObject
{
    [SerializeField] private int[] prices;
    public int[] GetAllPrices()
    {
        return prices;
    }

    public int GetPrice(int id)
    {
        return prices[id];
    }
}
