using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : PlayerUpgrade
{
    private readonly HealthUpgradeConfig config;

    private PlayerStats playerStats;

    public HealthUpgrade(HealthUpgradeConfig config) : base(config)
    {
        this.config = config;
    }

    public override string CurrentStats 
    {
        get { return config.GetHealth(Level).ToString(); }
    }

    public override string NextImprovement
    {
        get { return config.GetHealth(Level + 1).ToString(); }
    }

    public override void Initialize()
    {
        playerStats = ServiceLocator.GetService<PlayerStats>();
        SetHealth(Level);
    }

    protected override void UpdateLevel(int level)
    {
        SetHealth(level);
#if !UNITY_EDITOR
        YandexMetrica.EventUpgrade(Id, level);
#endif
    }

    private void SetHealth(int level)
    {
        int health = config.GetHealth(level);
        playerStats.SetHealth(health);
        //PlayerStats.instance.SetHealth(health);
    }
}
