using UnityEngine;

public class Damageable : MonoBehaviour 
{
    public int maxHealth;
    [SerializeField] private float damageForceThreshold = 1f;

    private Enemy enemy;
    private bool isDamageable = true;

    public int CurrentHealth { get; private set; }

    private void Start() 
    {
        CurrentHealth = maxHealth;
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDamageable)
        {
            return;
        }
        Player player;
        if (other.TryGetComponent(out player))
        {
            if (player.Speed > damageForceThreshold)
            {
                int damage = (int)((player.Speed - damageForceThreshold) / 10 * player.Damage);
                DamageUI.Instance.AddText(damage, transform.position, true);
                CurrentHealth -= damage;
                CurrentHealth = Mathf.Max(0, CurrentHealth);
                if (CurrentHealth == 0)
                {
                    enemy.Death();
                }
                else
                {
                    enemy.Fall();
                    isDamageable = false;
                }
            }
        }
    }

    public void BecomeDamageable()
    {
        isDamageable = true;
    }
}