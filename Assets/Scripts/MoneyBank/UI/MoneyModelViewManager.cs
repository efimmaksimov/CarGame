using UnityEngine;

public class MoneyModelViewManager : MonoBehaviour
{
    [SerializeField] private MoneyView view;
    private MoneyModelView modelView;

    private IMoneyBank moneyBank;

    private void Start()
    {
        moneyBank = ServiceLocator.GetService<MoneyBank>();
        modelView = new MoneyModelView(moneyBank, view);
        modelView.Initialize();
    }

    private void OnDestroy()
    {
        modelView.Dispose();
    }
}
