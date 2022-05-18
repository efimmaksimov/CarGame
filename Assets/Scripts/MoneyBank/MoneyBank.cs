using System;
using UnityEngine;

public class MoneyBank : MonoBehaviour, IMoneyBank
{
    private static MoneyBank instance;

    private int coins = 50000;

    public event Action OnMoneyChanged;

    public static MoneyBank Instance
    {
        get
        {
            if (instance == null) instance = new GameObject("GameManager").AddComponent<MoneyBank>(); //create game manager object if required
            return instance;
        }
    }

    void Awake()
    {
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
