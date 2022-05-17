using System;

public interface IMoneyBank
{
    event Action OnMoneyChanged;

    public bool CanSpendMoney(int price);
    public void SpendMoney(int price);
    public void AddMoney(int money);
    public int GetMoney();
}
