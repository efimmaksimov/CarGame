using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text valueText;
    [SerializeField] private Text priceText;

    private int currrentValue;
    private int nextValue;

    public Button UpgradeButton
    {
        get { return upgradeButton; }
    }

    public void SetValue(string value)
    {
        currrentValue = int.Parse(value);
        valueText.text = $"{currrentValue} > <color=#89FD3A>{nextValue}</color>";
    }

    public void SetNextValue(string value)
    {
        nextValue = int.Parse(value);
        valueText.text = $"{currrentValue} > <color=#89FD3A>{nextValue}</color>";
    }

    public void SetPrice(int price)
    {
        priceText.text = price.ToString();
    }
}
