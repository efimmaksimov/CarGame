using UnityEngine;

public class MoneyModelView
{
    private IMoneyBank moneyBank;
    private readonly MoneyView view;

    public MoneyModelView(IMoneyBank moneyBank, MoneyView view)
    {
        this.moneyBank = moneyBank;
        this.view = view;
    }

    public void Initialize()
    {
        this.moneyBank.OnMoneyChanged += OnMoneyChanged;
        UpdateState();
    }

    private void OnMoneyChanged()
    {
        UpdateState();
        Debug.Log("Money");
    }

    private void UpdateState()
    {
        view.SetCoins(moneyBank.GetMoney());
    }
}
