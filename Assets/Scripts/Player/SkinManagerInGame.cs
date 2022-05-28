using InstantGamesBridge;
using UnityEngine;

public class SkinManagerInGame : MonoBehaviour
{
    [SerializeField] private VehicleControl[] skins;
    [SerializeField] private CameraFollow cameraFollow;
    private int currentSkinIndex;

    private void Awake()
    {
        currentSkinIndex = PlayerStats.instance.CurrentSkinIndex;
        VehicleControl currentSkin = skins[currentSkinIndex];
        currentSkin.gameObject.SetActive(true);
        cameraFollow.carTransform = skins[currentSkinIndex].transform;
        if (Bridge.device.type != InstantGamesBridge.Modules.Device.DeviceType.Desktop)
        {
            GetComponent<TouchControl>().SetCarScript(currentSkin);
            currentSkin.controlMode = ControlMode.touch;
        }
        
    }
}
