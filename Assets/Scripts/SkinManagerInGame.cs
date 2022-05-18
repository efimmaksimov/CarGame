using UnityEngine;

public class SkinManagerInGame : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;
    [SerializeField] private CameraFollow cameraFollow;
    private int currentSkinIndex;

    private void Awake()
    {
        currentSkinIndex = PlayerStats.instance.CurrentSkinIndex;
        skins[currentSkinIndex].SetActive(true);
        cameraFollow.carTransform = skins[currentSkinIndex].transform;
    }
}
