using UnityEngine;
using InstantGamesBridge;

public class MoneySaveLoader : MonoBehaviour
{
    private const string KEY = "money";

    private MoneyBank moneyBank;
    private void Start()
    {
        moneyBank = ServiceLocator.GetService<MoneyBank>();
        moneyBank.OnMoneyChanged += SaveMoney;
        LoadMoney();
        DontDestroyOnLoad(gameObject);
    }

    private void SaveMoney()
    {
        int money = moneyBank.GetMoney();
        Bridge.game.SetData(KEY, money);
    }

    private void LoadMoney()
    {
        int money;
        Bridge.game.GetData(KEY, (succes, data) => {
            if (succes && data != null)
            {
                Debug.Log($"Money: {data}");
                money = int.Parse(data);
            }
            else
            {
                money = 0;
            }
            OnLoadMoney(money);
        });
    }

    private void OnLoadMoney(int money)
    {
        moneyBank.Setup(money);
    }
}