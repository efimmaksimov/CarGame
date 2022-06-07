using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Clips _clips;

    private string _currentMusicName;
    private Dictionary<string, AudioClip> _preloadedClips;

    private AudioSource[] _soundSources = new AudioSource[3];
    private AudioSource _musicSource;

    private AudioMixerGroup _masterMixerGroup;

    private AudioManagerSettings _settings;

    protected override void Awake()
    {
        base.Awake();

        _clips = Resources.Load<Clips>("AudioManager/Clips");

        _settings = Resources.Load<AudioManagerSettings>("AudioManager/AudioManagerSettings"); 
        if (_settings == null)
        {
            Debug.LogWarning("AudiosManagerSettings not founded resources. Using default settings");
            _settings = ScriptableObject.CreateInstance<AudioManagerSettings>();
        }

        _settings.LoadSettings();

        _masterMixerGroup = _settings.masterMixerGroup;

        _preloadedClips = _clips.AllClips;
    }

    private void CreateSoundObject(SoundChanel chanel)
    {
        GameObject soundGameObject = new GameObject("Sound" + chanel);
        soundGameObject.transform.parent = transform;

        _soundSources[(int)chanel] = soundGameObject.AddComponent<AudioSource>();

        _soundSources[(int)chanel].outputAudioMixerGroup = _settings.SoundAudioMixerGroup;
        _soundSources[(int)chanel].playOnAwake = false;
        _soundSources[(int)chanel].priority = 0;
    }

    private void CreateMusicObject()
    {
        GameObject musicGameObject = new GameObject("Music");
        musicGameObject.transform.parent = transform;

        _musicSource = musicGameObject.AddComponent<AudioSource>();

        _musicSource.outputAudioMixerGroup = _settings.MusicAudioMixerGroup;
        _musicSource.loop = true;
        _musicSource.priority = 256;
        _musicSource.playOnAwake = false;
        _musicSource.volume = 0.08f;
    }

    private void ApplySoundMuted()
    {
        float volume = _settings.GetSoundMuted() ? -80 : 0;
        _masterMixerGroup.audioMixer.SetFloat("VolumeSounds", volume);
    }

    private void ApplyMusicMuted()
    {
        float volume = _settings.GetMusicMuted() ? -80 : 0;
        _masterMixerGroup.audioMixer.SetFloat("VolumeMusic", volume);
    }

    public void PlaySound(AudioClip sound, float pitch = 1f, SoundChanel chanel = SoundChanel.First)
    {
        if (sound == null)
        {
            Debug.Log("Sound null or empty");
            return;
        }
        if (_soundSources[(int)chanel] == null)
        {
            CreateSoundObject(chanel);
        }

        _soundSources[(int)chanel].pitch = pitch;
        _soundSources[(int)chanel].PlayOneShot(sound);
    }

    public void PlaySound(string soundName, float pitch = 1f, SoundChanel chanel = SoundChanel.First)
    {
        if (string.IsNullOrEmpty(soundName))
        {
            Debug.Log("Sound null or empty");
            return;
        }

        if (_soundSources[(int)chanel] == null)
        {
            CreateSoundObject(chanel);
        }

        AudioClip clip;
        if (_preloadedClips.TryGetValue(soundName, out clip))
        {
            _soundSources[(int)chanel].pitch = pitch;
            _soundSources[(int)chanel].PlayOneShot(clip);
        }
        else
        {
            Debug.Log("This clip doesn't exist " + soundName);
        }
    }

    public void PlayMusic(AudioClip music)
    {
        if (music == null)
        {
            Debug.Log("Music empty or null");
            return;
        }

        if (_currentMusicName == music.name)
        {
            Debug.Log("Music already playing: " + music.name);
            return;
        }

        

        if(_musicSource == null)
        {
            CreateMusicObject();
        }

        StartCoroutine(ChangeMusic(music, _settings.MusicFadeTime, _currentMusicName != null));
        _currentMusicName = music.name;
    }

    public void PlayMusic(string musicName)
    {
        if (string.IsNullOrEmpty(musicName))
        {
            Debug.Log("Music empty or null");
            return;
        }

        if (_currentMusicName == musicName)
        {
            Debug.Log("Music already playing: " + musicName);
            return;
        }

        if (_musicSource == null)
        {
            CreateMusicObject();
        }

        AudioClip clip;
        if (_preloadedClips.TryGetValue(musicName, out clip))
        {
            StartCoroutine(ChangeMusic(clip, _settings.MusicFadeTime, _currentMusicName != null));
            _currentMusicName = musicName;
        }
        else
        {
            Debug.Log("This clip doesn't exist");
        }
    }

    private IEnumerator ChangeMusic(AudioClip clip, float fadeTime, bool isPlaying)
    {
        float timer;
        if (isPlaying)
        {
            timer = fadeTime;
            while (timer > 0)
            {
                timer -= Time.unscaledDeltaTime;
                _musicSource.volume = Mathf.Clamp01(timer / fadeTime) * _settings.musicVolume;
                yield return null;
            }
        }

        _musicSource.clip = clip;
        _musicSource.Play();

        timer = 0;
        while(timer < fadeTime)
        {
            timer += Time.unscaledDeltaTime;
            _musicSource.volume = Mathf.Clamp01(timer / fadeTime) * _settings.musicVolume;
            yield return null;
        }
    }

    public void SetSoundMuted(bool mute)
    {
        _settings.SetSoundMuted(mute);
        ApplySoundMuted();
    }

    public bool GetSoundMuted()
    {
        return _settings.GetSoundMuted();
    }

    public void SetMusicMuted(bool mute)
    {
        _settings.SetMusicMuted(mute);
        ApplyMusicMuted();
    }

    public bool GetMusicMuted()
    {
        return _settings.GetMusicMuted();
    }
}
public enum SoundChanel
{
    First,
    Second,
    Third,
}
