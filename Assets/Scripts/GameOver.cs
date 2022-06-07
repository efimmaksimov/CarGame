using UnityEngine;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [HideInInspector] public VehicleControl vehicleControl;

    [SerializeField] private UIController controllerUI;
    [SerializeField] private CoinCounter coinCounter;
    private IMoneyBank moneyBank;
    private int reward;
    private bool isRewardedAdShown;
    private void Awake()
    {
        Messenger.AddListener(GameEvents.gameOver, OnGameOver);
        Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.gameOver, OnGameOver);
        Bridge.advertisement.rewardedStateChanged -= OnRewardedStateChanged;
    }

    private void Start()
    {
        moneyBank = ServiceLocator.GetService<MoneyBank>();
    }
    private void OnGameOver()
    {
        controllerUI.OnGameOver();
        reward = coinCounter.GetReward();
        controllerUI.SetReward(reward);
    }
    public void GoToGarage()
    {
        moneyBank.AddMoney(reward);
        if (!isRewardedAdShown) Bridge.advertisement.ShowInterstitial();
        SceneManager.LoadSceneAsync(1);
    }
    #region RewardedAd
    public void ShowRewardedAd()
    {
        Bridge.advertisement.ShowRewarded();
    }
    private void OnRewardedStateChanged(RewardedState state)
    {
        switch (state)
        {
            case RewardedState.Opened:
                OnOpen();
                break;
            case RewardedState.Rewarded:
                OnReward();
                break;
            case RewardedState.Closed:
                OnClose();
                break;
            default:
                break;
        }
    }
    private void OnOpen()
    {
        AudioListener.volume = 0;
        vehicleControl.activeControl = false;
    }
    private void OnReward()
    {
        reward *= 2;
        isRewardedAdShown = true;
#if !UNITY_EDITOR
        YandexMetrica.EventWatchRewarded(1);
#endif
    }
    private void OnClose()
    {
        AudioListener.volume = 1;
        vehicleControl.activeControl = true;
        GoToGarage();
    }
#endregion
}
