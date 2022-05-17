using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerStatsComponent stats;

    //private VehicleControl vehicleControl;
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
        //vehicleControl = GetComponentInChildren<VehicleControl>();
        rb = GetComponentInChildren<Rigidbody>();
        stats = GetComponent<PlayerStatsComponent>();
        //vehicleControl.controlMode = WebPlatformSDK.instance.isMobile ? ControlMode.touch : ControlMode.simple;
    }

    public void TakeDamage(int damage)
    {
        stats.DecreaseHealth(damage);
    }
}
