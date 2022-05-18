using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour, ISkinManager
{
    [SerializeField] private SkinsConfig config;

    private SkinBuyer skinBuyer;
    private Skin[] allSkins;
    private Skin selectedSkin;
     public void Buy(ISkin skin)
    {
        skinBuyer.Buy((Skin)skin);
    }

    public bool CanBuy(ISkin skin)
    {
        return skinBuyer.CanBuy((Skin)skin);
    }

    public bool CanSelect(ISkin skin)
    {
        return skin.Purchased;
    }

    public void Select(ISkin skin)
    {
        selectedSkin.Deselect();
        selectedSkin = (Skin)skin;
        selectedSkin.Select();
    }

    public ISkin GetSkin(int id)
    {
        return allSkins[id];
    }
    public ISkin[] GetAllSkins()
    {
        return allSkins;
    }

    private void Awake()
    {
        int[] allPrices = config.GetAllPrices();
        allSkins = new Skin[allPrices.Length];
        for (int i = 0; i < allPrices.Length; i++)
        {
            Skin skin = new Skin(allPrices[i], i);
            allSkins[i] = skin;
        }
        var moneyBank = MoneyBank.Instance;
        skinBuyer = new SkinBuyer(moneyBank);

        selectedSkin = allSkins[0];
        allSkins[0].Setup(true);
    }
}
