using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerSettings : ScriptableObject
{
    public float MusicFadeTime = 2f;

    [Range(0f, 1f)]
    public float musicVolume;

    public AudioMixerGroup masterMixerGroup;
    public AudioMixerGroup MusicAudioMixerGroup;
    public AudioMixerGroup SoundAudioMixerGroup;
    private bool _mutedMusic;
    private bool _mutedSound;


    public void SaveSettings()
    {
        PlayerPrefs.SetInt("SM_MusicMute", _mutedMusic ? 1 : 0);
        PlayerPrefs.SetInt("SM_SoundMute", _mutedSound ? 1 : 0);
    }

    public void LoadSettings()
    {
        _mutedMusic = PlayerPrefs.GetInt("SM_MusicMute", 0) == 1;
        _mutedSound = PlayerPrefs.GetInt("SM_SoundMute", 0) == 1;
    }

    public void SetMusicMuted(bool mute)
    {
        _mutedMusic = mute;
        SaveSettings();
    }

    public bool GetMusicMuted()
    {
        return _mutedMusic;
    }

    public void SetSoundMuted(bool mute)
    {
        _mutedSound = mute;
        SaveSettings();
    }

    public bool GetSoundMuted()
    {
        return _mutedSound;
    }


    //[MenuItem("AudioManager/Create AudioManagerSettings")]
    //public static void CreateAsset()
    //{
    //    AudioManagerSettings asset = ScriptableObject.CreateInstance<AudioManagerSettings>();
    //    string assetPathAndName = "Assets/Resources/AudioManager/AudioManagerSettings.asset";
    //    AssetDatabase.CreateAsset(asset, assetPathAndName);
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //    EditorUtility.FocusProjectWindow();
    //    Selection.activeObject = asset;
    //}
}
