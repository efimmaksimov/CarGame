using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerUpgradeCatalog",
    menuName = "PlayerUpgrades/New PlayerUpgradeCatalog"
    )]
public class PlayerUpgradeCatalog : ScriptableObject
{
    [SerializeField] private PlayerUpgradeConfig[] configs;

    public PlayerUpgradeConfig[] GetPlayerUpgrades()
    {
        return configs;
    }

    public PlayerUpgradeConfig FindUpgrade(string id)
    {
        for (int i = 0; i < configs.Length; i++)
        {
            if (configs[i].id == id)
            {
                return configs[i];
            }
        }
        return null;
    }
}

