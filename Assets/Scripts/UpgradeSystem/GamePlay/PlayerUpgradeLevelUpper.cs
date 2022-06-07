public sealed class PlayerUpgradeLevelUpper
{
    private readonly IMoneyBank moneyBank;
    public PlayerUpgradeLevelUpper(IMoneyBank moneyBank)
    {
        this.moneyBank = moneyBank;
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

        upgrade.IncrementLevel();
        moneyBank.SpendMoney(price);
    }
}
