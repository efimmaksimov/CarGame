using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerStatsComponent stats;
    public float Speed
    {
        get { return rb.velocity.magnitude; }
        private set { } 
    }  
    public int Damage
    {
        get { return stats.Damage; }
    }
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        stats = GetComponent<PlayerStatsComponent>();
    }

    public void TakeDamage(int damage)
    {
        stats.DecreaseHealth(damage);
    }
}
