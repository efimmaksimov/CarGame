using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradesManager : MonoBehaviour, IPlayerUpgradesManager
{
    [SerializeField] private PlayerUpgradeCatalog catalog;

    private Dictionary<string, PlayerUpgrade> upgrades;
    private PlayerUpgradeLevelUpper levelUpper;

    public PlayerUpgradesManager()
    {
        upgrades = new Dictionary<string, PlayerUpgrade>();
    }

    public bool CanLevelUp(IPlayerUpgrade upgrade)
    {
        return levelUpper.CanLevelUp((PlayerUpgrade)upgrade);
    }

    public void LevelUp(IPlayerUpgrade upgrade)
    {
        levelUpper.LevelUp((PlayerUpgrade)upgrade);
    }

    public IPlayerUpgrade[] GetAllUpgrades()
    {
        int count = upgrades.Count;
        IPlayerUpgrade[] result = new IPlayerUpgrade[count];
        int index = 0;
        foreach (var upgrade in upgrades.Values)
        {
            result[index] = upgrade;
            index++;
        }
        return result;
    }

    public IPlayerUpgrade GetUpgrade(string id)
    {
        return upgrades[id];
    }

    private void Awake()
    {
        var configs = catalog.GetPlayerUpgrades();
        for (int i = 0; i < configs.Length; i++)
        {
            var config = configs[i];
            var upgrade = (PlayerUpgrade)config.InstantiateUpgrade();
            upgrades.Add(config.id, upgrade);
        }
        IMoneyBank moneyBank = ServiceLocator.GetService<MoneyBank>();
        levelUpper = new PlayerUpgradeLevelUpper(moneyBank);
        DontDestroyOnLoad(gameObject);
    }
}
