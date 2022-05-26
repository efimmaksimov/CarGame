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

    public void ToggleMusicMuted()
    {
        AudioManager.Instance.SetMusicMuted(!AudioManager.Instance.GetMusicMuted());
    }

    public void ToggleSoundMuted()
    {
        AudioManager.Instance.SetSoundMuted(!AudioManager.Instance.GetSoundMuted());
    }
}
