public class DamageUpgrade : PlayerUpgrade
{
    private readonly DamageUpgradeConfig config;

    private PlayerStats playerStats;

    public DamageUpgrade(DamageUpgradeConfig config) : base(config)
    {
        this.config = config;
    }

    public override string CurrentStats 
    {
        get { return config.GetDamage(Level).ToString(); }
    }

    public override string NextImprovement
    {
        get { return config.GetDamage(Level + 1).ToString(); }
    }

    public override void Initialize()
    {
        playerStats = ServiceLocator.GetService<PlayerStats>();
        SetDamage(Level);
    }

    protected override void UpdateLevel(int level)
    {
        SetDamage(level);
#if !UNITY_EDITOR
        YandexMetrica.EventUpgrade(Id, level);
#endif
    }

    private void SetDamage(int level)
    {
        int damage = config.GetDamage(level);
        playerStats.SetDamage(damage);
    }
}
