using UnityEngine;
using UnityEngine.UI;

public class SkinView : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private Button arrowLeft;
    [SerializeField] private Button arrowRight;
    [SerializeField] private Button buyButton;

    [Space]
    [SerializeField] private GameObject[] skins;
    private int currentSkinIndex = 0;

    public Button ArrowLeft
    {
        get { return arrowLeft; }
    }

    public Button ArrowRight
    {
        get { return arrowRight; }
    }
    public Button BuyButton
    {
        get { return buyButton; }
    }

    public void SetPrice(int price)
    {
        priceText.text = price.ToString();
    }

    public void ShowOrHideLock(bool show)
    {
        lockImage.SetActive(show);
        buyButton.gameObject.SetActive(show);
    }
    public void ShowAnotherSkin(int index)
    {
        skins[currentSkinIndex].SetActive(false);
        currentSkinIndex = index;
        skins[currentSkinIndex].SetActive(true);
    }
}
