using UnityEngine;
using UnityEngine.UI;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Text coins;

    public void SetCoins(int coins)
    {
        this.coins.text = coins.ToString();
    }
}
