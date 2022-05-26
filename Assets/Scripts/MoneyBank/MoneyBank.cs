using System;
using UnityEngine;

public class MoneyBank : MonoBehaviour, IMoneyBank
{
    public event Action OnMoneyChanged;

    private int money;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public bool CanSpendMoney(int price)
    {
        return money >= price;
    }

    public void SpendMoney(int price)
    {
        if (money < price)
        {
            throw new System.Exception($"Price:{price} is more, than number of coins:{money}");
        }
        else
        {
            money -= price;
            OnMoneyChanged?.Invoke();
        }
    }

    public void AddMoney(int money)
    {
        this.money += money;
        OnMoneyChanged?.Invoke();
    }

    public int GetMoney()
    {
        return money;
    }

    public void Setup(int money)
    {
        this.money = money;
        OnMoneyChanged?.Invoke();
    }
}
