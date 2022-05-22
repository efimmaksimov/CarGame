using UnityEngine;
public class PlayerUpgradesInteractor : MonoBehaviour
{
    private PlayerUpgradeRepository repository;
    private PlayerUpgradesManager playerUpgradesManager;
    private void Start()
    {
        repository = new PlayerUpgradeRepository();
        playerUpgradesManager = ServiceLocator.GetService<PlayerUpgradesManager>();
        repository.LoadUpgrades(OnLoadUpgrades);
        GameSaver.instance.AddListener(OnSaveGame);
        DontDestroyOnLoad(gameObject);
    }

    private void OnLoadUpgrades(UpgradeSaveData[] upgrades)
    {
        if (upgrades != null)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                var data = upgrades[i];
                var upgrade = playerUpgradesManager.GetUpgrade(data.id);
                upgrade.Setup(data.level);
            }
        }  
    }

    private void OnSaveGame()
    {
        IPlayerUpgrade[] playerUpgrades = playerUpgradesManager.GetAllUpgrades();
        UpgradeSaveData[] upgradeSaveDatas = new UpgradeSaveData[playerUpgrades.Length];
        for (int i = 0; i < playerUpgrades.Length; i++)
        {
            var upgrade = playerUpgrades[i];
            var data = new UpgradeSaveData
            {
                id = upgrade.Id,
                level = upgrade.Level

            };
            upgradeSaveDatas[i] = data;
        }
        repository.SaveUpgrades(upgradeSaveDatas);
    }
}
