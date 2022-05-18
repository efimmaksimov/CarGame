using UnityEngine;

public class SkinViewModelManager : MonoBehaviour
{
    [SerializeField] private SkinView view;
    private SkinViewModel viewModel;
    private ISkinManager skinManager;
    private IMoneyBank moneyBank;

    private void Start()
    {
        skinManager = ServiceLocator.GetService<SkinManager>();
        moneyBank = MoneyBank.Instance;
        Initialize();
    }

    private void Initialize()
    {
        var allSkins = skinManager.GetAllSkins();
        viewModel = new SkinViewModel(allSkins, view);
        viewModel.Initialize(skinManager, moneyBank);
    }

    private void OnDestroy()
    {
        viewModel.Dispose();
    }
}
