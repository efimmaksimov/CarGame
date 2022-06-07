public class SkinBuyer
{
    private IMoneyBank moneyBank;

    public SkinBuyer(IMoneyBank moneyBank)
    {
        this.moneyBank = moneyBank;
    }

    public bool CanBuy(Skin skin)
    {
        return moneyBank.CanSpendMoney(skin.Price);
    }

    public void Buy(Skin skin)
    {
        if (!CanBuy(skin))
        {
            throw new System.Exception($"Can not buy {skin.Id}");
        }
        int price = skin.Price;
        
        skin.Buy();
        moneyBank.SpendMoney(price);
    }
}
