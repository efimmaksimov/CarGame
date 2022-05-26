using UnityEngine;

public class GameSaverOnMoneyChanged : MonoBehaviour
{
    private void Start()
    {
        MoneyBank moneyBank = ServiceLocator.GetService<MoneyBank>();
        moneyBank.OnMoneyChanged += SaveGame;
        DontDestroyOnLoad(gameObject);
    }

    private void SaveGame()
    {
        GameSaver.Instance.SaveGame();
    }
}
