using System;
using InstantGamesBridge;

public class PlayerUpgradeRepository
{
    private string KEY = "upgrades";
    public void LoadUpgrades(Action<UpgradeSaveData[]> onComplete)
    {
        UpgradeSaveData[] upgrades;
        Bridge.game.GetData(KEY, (succes, data) => {
            if (succes && data != null)
            {
                upgrades = JsonHelper.FromJson<UpgradeSaveData>(data);
            }
            else
            {
                upgrades = null;
            }
            onComplete?.Invoke(upgrades);
        });
    }
    public void SaveUpgrades(UpgradeSaveData[] upgrades)
    {
        Bridge.game.SetData(KEY, JsonHelper.ToJson(upgrades));
    }
}
