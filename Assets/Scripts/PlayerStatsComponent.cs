using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsComponent : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private UIController controllerUI;

    [SerializeField] private int maxHealth;
    private int currentHealth;

    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            healthBar.value = (float)currentHealth / (float)maxHealth;
        }
    }
    public int Damage { get; private set; }

    private void Start()
    {
        PlayerStats playerStats = PlayerStats.instance;
        maxHealth = playerStats.Health;
        Damage = playerStats.Damage;
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(int damage)
    {
        int health = currentHealth;
        health -= damage;
        health = Mathf.Max(0, health);
        Health = health;
        if (currentHealth == 0)
        {
            Time.timeScale = 0;
            controllerUI.ChangeGameOverText();
            Messenger.Broadcast(GameEvents.gameOver);
        }
    }
}
