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
        moneyBank.OnMoneyChanged += OnMoneyChanged;
        UpdateState();
    }

    public void Dispose()
    {
        moneyBank.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        view.SetCoins(moneyBank.GetMoney());
    }
}
