using System.Collections.Generic;
using UnityEngine;

public class UpgradeViewManager : MonoBehaviour
{
    private IPlayerUpgradesManager upgradesManager;
    private IMoneyBank moneyBank;

    private List<UpgradeViewModel> viewModels;
    [SerializeField] private List<UpgradeView> views;

    private void Start()
    {
        upgradesManager = ServiceLocator.GetService<PlayerUpgradesManager>();
        moneyBank = ServiceLocator.GetService<MoneyBank>(); ;
        Initialize();
    }

    private void Initialize()
    {
        var upgrades = upgradesManager.GetAllUpgrades();
        viewModels = new List<UpgradeViewModel>();
        for (int i = 0; i < upgrades.Length; i++)
        {
            var model = upgrades[i];
            var viewModel = new UpgradeViewModel(model, views[i]);
            viewModel.Initialize(upgradesManager, moneyBank);
            this.viewModels.Add(viewModel);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < viewModels.Count; i++)
        {
            viewModels[i].Dispose();
        }
    }
}
