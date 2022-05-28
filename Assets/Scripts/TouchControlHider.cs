using InstantGamesBridge;
using UnityEngine;

public class TouchControlHider : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(Bridge.device.type != InstantGamesBridge.Modules.Device.DeviceType.Desktop);
    }
}
