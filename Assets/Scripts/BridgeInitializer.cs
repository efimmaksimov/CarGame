using UnityEngine;
using UnityEngine.SceneManagement;
using InstantGamesBridge;

public class BridgeInitializer : MonoBehaviour
{
    private void Awake()
    {
        Bridge.Initialize((succes) => {
            if (succes)
            {
                if (Bridge.platform.id != "vk")
                {
                    Bridge.advertisement.ShowInterstitial();
                }
                else
                {
                    Bridge.advertisement.SetMinimumDelayBetweenInterstitial(60);
                }
            }
        });
#if !UNITY_EDITOR
        YandexMetrica.NotBounce();
#endif
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
