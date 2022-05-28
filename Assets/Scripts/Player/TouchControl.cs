using UnityEngine;

public class TouchControl : MonoBehaviour
{
    private VehicleControl carScript;
    public void SetCarScript(VehicleControl vehicleControl)
    {
        carScript = vehicleControl;
    }
    public void CarAccelForward(float amount)
    {
        carScript.accelFwd = amount;
    }
    public void CarAccelBack(float amount)
    {
        carScript.accelBack = amount;
    }
    public void CarSteer(float amount)
    {
        carScript.steerAmount = amount;
    }

    public void CarHandBrake(bool HBrakeing)
    {
        carScript.brake = HBrakeing;
    }
}
