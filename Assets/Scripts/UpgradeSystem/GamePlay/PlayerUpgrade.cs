using System;

public abstract class PlayerUpgrade : IPlayerUpgrade
{
    public event Action<int> OnLevelUp;

    public int Level { get; private set; } = 0;
    public string Id 
    {
        get { return this.config.id; }
    }

    public abstract string CurrentStats { get; }
    public abstract string NextImprovement { get; }
    public int NextPrice
    {
        get { return this.config.GetPrice(this.Level); }
    }

    private readonly PlayerUpgradeConfig config;

    protected PlayerUpgrade(PlayerUpgradeConfig config)
    {
        this.config = config;
    }

    public void Setup(int level)
    {
        this.Level = level;
        OnLevelUp?.Invoke(Level);
        Initialize();
    }

    public void IncrementLevel()
    {
        int nextLevel = Level + 1;
        UpdateLevel(nextLevel);
        Level = nextLevel;
        OnLevelUp?.Invoke(Level);
    }

    public abstract void Initialize();
    protected abstract void UpdateLevel(int level);
}
