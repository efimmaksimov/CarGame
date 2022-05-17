using UnityEngine;

public sealed class PlayerUpgradeLevelUpper
{
    private readonly IMoneyBank moneyBank;
    public PlayerUpgradeLevelUpper()
    {
        this.moneyBank = MoneyBank.instance;
    }

    public bool CanLevelUp(PlayerUpgrade upgrade)
    {
        int price = upgrade.NextPrice;
        return moneyBank.CanSpendMoney(price);
    }

    public void LevelUp(PlayerUpgrade upgrade)
    {
        if (!CanLevelUp(upgrade))
        {
            throw new System.Exception($"Can not level up {upgrade.Id}");
        }
        int price = upgrade.NextPrice;
        moneyBank.SpendMoney(price);

        upgrade.IncrementLevel();
    }
}
