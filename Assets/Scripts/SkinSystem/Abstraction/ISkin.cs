using System;

public interface ISkin
{
    event Action OnBuy;
    bool Purchased { get; }
    bool Selected { get; }
    int Id { get; }
    int Price { get; }
    void Setup(bool purchased);

}
