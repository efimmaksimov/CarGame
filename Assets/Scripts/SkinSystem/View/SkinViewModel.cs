using System;

public class SkinViewModel
{
    private readonly SkinView view;
    private ISkin[] skins;
    private ISkinManager skinManager;
    private IMoneyBank moneyBank;
    private int currentSkinIndex;

    public SkinViewModel(ISkin[] skins, SkinView view)
    {
        this.skins = skins;
        this.view = view;
    }

    public void Initialize(ISkinManager skinManager, IMoneyBank moneyBank)
    {
        this.skinManager = skinManager;
        this.moneyBank = moneyBank;
        this.moneyBank.OnMoneyChanged += OnMoneyChanged;

        view.BuyButton.onClick.AddListener(OnBuyButtonClicked);
        view.ArrowLeft.onClick.AddListener(OnArrowLeft);
        view.ArrowRight.onClick.AddListener(OnArrowRight);

        currentSkinIndex = Array.IndexOf(skins, skinManager.GetSelectedSkin());
        skins[currentSkinIndex].OnBuy += OnBuy;
        view.ShowAnotherSkin(currentSkinIndex);

        UpdateState();
    }

    public void Dispose()
    {
        this.moneyBank.OnMoneyChanged -= OnMoneyChanged;

        view.BuyButton.onClick.RemoveListener(OnBuyButtonClicked);
        view.ArrowLeft.onClick.RemoveListener(OnArrowLeft);
        view.ArrowRight.onClick.RemoveListener(OnArrowRight);
        skins[currentSkinIndex].OnBuy -= OnBuy;
    }

    private void UpdateState()
    {
        if (skins[currentSkinIndex].Purchased)
        {
            view.ShowOrHideLock(false);

        }
        else
        {
            view.SetPrice(skins[currentSkinIndex].Price);
            UpdateButtonState();
            view.ShowOrHideLock(true);
        }
    }
    private void UpdateButtonState()
    {
        view.BuyButton.interactable = moneyBank.CanSpendMoney(skins[currentSkinIndex].Price);
    }

    private void ChangeActiveSkin(int direction)
    {
        skins[currentSkinIndex].OnBuy -= OnBuy;
        currentSkinIndex = (currentSkinIndex + direction + skins.Length) % skins.Length;
        skins[currentSkinIndex].OnBuy += OnBuy;
        if (skinManager.CanSelect(skins[currentSkinIndex]))
        {
            skinManager.Select(skins[currentSkinIndex]);
        }
        view.ShowAnotherSkin(currentSkinIndex);
        UpdateState();
    }

    #region EVENTS
    private void OnMoneyChanged()
    {
        UpdateButtonState();
    }
    private void OnBuyButtonClicked()
    {
        if (skinManager.CanBuy(skins[currentSkinIndex]))
        {
            skinManager.Buy(skins[currentSkinIndex]);
        } 
    }
    private void OnBuy()
    {
        UpdateState();
    }
    private void OnArrowLeft()
    {
        int direction = -1;
        ChangeActiveSkin(direction);
    }
    private void OnArrowRight()
    {
        int direction = 1;
        ChangeActiveSkin(direction);
    }
    #endregion
}
