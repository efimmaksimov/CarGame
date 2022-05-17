using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBank : MonoBehaviour, IMoneyBank
{
    public static MoneyBank instance;

    private int coins = 50000;

    public event Action OnMoneyChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public bool CanSpendMoney(int price)
    {
        return coins >= price;
    }

    public void SpendMoney(int price)
    {
        if (coins < price)
        {
            throw new System.Exception($"Price:{price} is more, than number of coins:{coins}");
        }
        else
        {
            coins -= price;
            OnMoneyChanged?.Invoke();
        }
    }

    public void AddMoney(int money)
    {
        coins += money;
        //OnMoneyChanged?.Invoke();
    }

    public int GetMoney()
    {
        return coins;
    }
}
