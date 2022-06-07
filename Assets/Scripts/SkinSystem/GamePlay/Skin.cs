using System;
public class Skin : ISkin
{
    public event Action OnBuy;
    public int Id { get; }

    public int Price { get; }

    public bool Purchased { get; private set; }

    public bool Selected { get; private set; }

    public Skin(int price, int id)
    {
        Price = price;
        Id = id;
    }

    public void Setup(bool purchased)
    {
        Purchased = purchased;
        OnBuy?.Invoke();
    }
    public void Buy()
    {
        Purchased = true;
        OnBuy?.Invoke();
#if !UNITY_EDITOR
        YandexMetrica.EventBuyCar(Id);
#endif
    }

    public void Select()
    {
        Selected = true;
        PlayerStats.instance.SetCurrentSkin(Id);
    }
    public void Deselect()
    {
        Selected = false;
    }
}
