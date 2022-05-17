using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    public Button UpgradeButton
    {
        get { return upgradeButton; }
    }

    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text valueText;
    [SerializeField] private Text nextValueText;
    [SerializeField] private Text priceText;

    public void SetValue(string value)
    {
        valueText.text = value;
    }

    public void SetNextValue(string value)
    {
        nextValueText.text = value;
    }

    public void SetPrice(string price)
    {
        priceText.text = price;
    }
}
