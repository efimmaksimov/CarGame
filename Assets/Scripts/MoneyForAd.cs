using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEngine;
using UnityEngine.UI;

public class MoneyForAd : MonoBehaviour
{
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private MoneyForEnemyConfig moneyForEnemyConfig;
    [SerializeField] private Text rewardText;
    private int reward;
    private MoneyBank moneyBank;

    private void Awake()
    {
        Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
    }

    private void OnDestroy()
    {
        Bridge.advertisement.rewardedStateChanged -= OnRewardedStateChanged;
    }

    private void Start()
    {
        moneyBank = ServiceLocator.GetService<MoneyBank>();
        int[] enemiesQuantityOfCurrentWave = waveConfig.GetWaveData(WaveProgress.Instance.CurrentWave).enemiesQuantity;
        for (int i = 0; i < enemiesQuantityOfCurrentWave.Length; i++)
        {
            reward += enemiesQuantityOfCurrentWave[i] * moneyForEnemyConfig.GetMoneyForEnemy((EnemyType)i);
        }
        rewardText.text = $"+{reward}";
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
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }
    private void OnReward()
    {
        moneyBank.AddMoney(reward);
#if !UNITY_EDITOR
        YandexMetrica.EventWatchRewarded(2);
#endif
    }
    private void OnClose()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
#endregion
}
