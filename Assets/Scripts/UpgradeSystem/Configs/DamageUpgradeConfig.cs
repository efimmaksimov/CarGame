using UnityEngine;

[CreateAssetMenu(
    fileName = "DamageUpgradeConfig",
    menuName = "PlayerUpgrades/New DamageUpgradeConfig"
    )]
public class DamageUpgradeConfig : PlayerUpgradeConfig
{
    [SerializeField] private int baseDamage;
    [SerializeField] private int bonusDamage;
    public int GetDamage(int level)
    {
        return baseDamage + level * bonusDamage;
    }

    public override IPlayerUpgrade InstantiateUpgrade()
    {
        DamageUpgrade damageUpgrade = new DamageUpgrade(this);
        damageUpgrade.Initialize();
        return damageUpgrade;
    }
}
