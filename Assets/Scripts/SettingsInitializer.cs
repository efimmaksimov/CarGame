using UnityEngine;
using UnityEngine.UI;

public class SettingsInitializer : MonoBehaviour
{
    [SerializeField] private Toggle toggleSound;
    [SerializeField] private Toggle toggleMusic;

    private void Start()
    {
        toggleSound.isOn = AudioManager.Instance.GetSoundMuted();
        toggleMusic.isOn = AudioManager.Instance.GetMusicMuted();
    }
}
