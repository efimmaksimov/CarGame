using UnityEngine;

public class Damageable : MonoBehaviour 
{
    public int maxHealth;
    [SerializeField] private float damageForceThreshold = 1f;

    private Enemy enemy;

    public int CurrentHealth { get; private set; }

    private void Start() 
    {
        CurrentHealth = maxHealth;
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player;
        if (other.TryGetComponent(out player))
        {
            if (player.Speed > damageForceThreshold)
            {
                CurrentHealth -= (int)((player.Speed - damageForceThreshold) / 20 * player.Damage);
                CurrentHealth = Mathf.Max(0, CurrentHealth);
                if (CurrentHealth == 0)
                {
                    enemy.Death();
                }
                else
                {
                    enemy.Fall();
                }
            }
        }
    }
}