using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private UIController controllerUI;
    [SerializeField] private CoinCounter coinCounter;

    private IMoneyBank moneyBank;
    private int reward;
    private void Awake()
    {
        Messenger.AddListener(GameEvents.gameOver, OnGameOver);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.gameOver, OnGameOver);
    }

    private void Start()
    {
        moneyBank = MoneyBank.instance;
    }

    private void OnGameOver()
    {
        controllerUI.OnGameOver();
        reward = coinCounter.GetReward();
        controllerUI.SetReward(reward);
    }

    public void GiveRewardToPlayer()
    {
        moneyBank.AddMoney(reward);
    }
}
