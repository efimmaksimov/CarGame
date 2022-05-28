using UnityEngine;

public class AudioManagerComponent : MonoBehaviour
{
    public void PlaySound(string name)
    {
        AudioManager.Instance.PlaySound(name);
    }

    public void PlayMusic(string name)
    {
        AudioManager.Instance.PlayMusic(name);
    }

    public void ToggleMusicMuted(bool mute)
    {
        AudioManager.Instance.SetMusicMuted(mute);
    }

    public void ToggleSoundMuted(bool mute)
    {
        AudioManager.Instance.SetSoundMuted(mute);
    }
}
