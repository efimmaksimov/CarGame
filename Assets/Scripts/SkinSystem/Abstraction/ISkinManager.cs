public interface ISkinManager
{
    bool CanBuy(ISkin skin);
    void Buy(ISkin skin);
    bool CanSelect(ISkin skin);
    void Select(ISkin skin);
    ISkin GetSkin(int id);
    ISkin[] GetAllSkins();
    ISkin GetSelectedSkin();
}
