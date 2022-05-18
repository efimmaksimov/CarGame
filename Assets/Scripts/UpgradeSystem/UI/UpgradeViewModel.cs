public class UpgradeViewModel
{
    private readonly IPlayerUpgrade upgrade;
    private readonly UpgradeView view;
    private IPlayerUpgradesManager upgradesManager;
    private IMoneyBank moneyBank;
    
    public UpgradeViewModel(IPlayerUpgrade upgrade, UpgradeView view)
    {
        this.upgrade = upgrade;
        this.view = view;
    }

    public void Initialize(IPlayerUpgradesManager upgradesManager, IMoneyBank moneyBank)
    {
        this.upgradesManager = upgradesManager;
        this.moneyBank = moneyBank;
        this.moneyBank.OnMoneyChanged += OnMoneyChanged;

        upgrade.OnLevelUp += OnLevelUp;
        view.UpgradeButton.onClick.AddListener(OnButtonClicked);
        UpdateState();
    }

    public void Dispose()
    {
        upgrade.OnLevelUp -= OnLevelUp;
        view.UpgradeButton.onClick.RemoveListener(OnButtonClicked);
        this.moneyBank.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnButtonClicked()
    {
        if (upgradesManager.CanLevelUp(upgrade))
        {
            upgradesManager.LevelUp(upgrade);
        }
    }

    private void OnLevelUp(int level)
    {
        UpdateState();
    }

    private void OnMoneyChanged()
    {
        UpdateButtonState();
    }

    private void UpdateState()
    {
        view.SetValue(upgrade.CurrentStats);
        view.SetNextValue(upgrade.NextImprovement);
        view.SetPrice(upgrade.NextPrice);
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        view.UpgradeButton.interactable = moneyBank.CanSpendMoney(upgrade.NextPrice);
    }
}
