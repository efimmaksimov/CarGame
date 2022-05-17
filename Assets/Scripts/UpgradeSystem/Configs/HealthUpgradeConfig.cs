using UnityEngine;

[CreateAssetMenu(
    fileName = "HealthUpgradeConfig",
    menuName = "PlayerUpgrades/New HealthUpgradeConfig"
    )]
public class HealthUpgradeConfig : PlayerUpgradeConfig
{
    [SerializeField] private int baseHealth;
    [SerializeField] private int bonusHealth;
    public int GetHealth(int level)
    {
        return baseHealth + level * bonusHealth;
    }

    public override IPlayerUpgrade InstantiateUpgrade()
    {
        HealthUpgrade healthUpgrade = new HealthUpgrade(this);
        healthUpgrade.Initialize();
        return healthUpgrade;
    }
}
