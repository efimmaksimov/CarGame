using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int Health { get; private set; }
    public int Damage { get; private set; }
    public int CurrentSkinIndex { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        this.Damage = damage;
    }
    public void SetHealth(int health)
    {
        this.Health = health;
    }
    public void SetCurrentSkin(int id)
    {
        CurrentSkinIndex = id;
    }
}
