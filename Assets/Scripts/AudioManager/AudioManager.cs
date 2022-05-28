using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    //private static AudioManager _instance;

    public Clips clips;

    private string _currentMusicName;

    private Dictionary<string, AudioClip> _preloadedClips;

    private AudioSource[] soundSources = new AudioSource[3];
    private AudioSource musicSource;

    private AudioMixerGroup masterMixerGroup;

    private AudioManagerSettings _settings;

    protected override void Awake()
    {
        base.Awake();

        clips = Resources.Load<Clips>("AudioManager/Clips");

        _settings = Resources.Load<AudioManagerSettings>("AudioManager/AudioManagerSettings"); 
        if (_settings == null)
        {
            Debug.LogWarning("AudiosManagerSettings not founded resources. Using default settings");
            _settings = ScriptableObject.CreateInstance<AudioManagerSettings>();
        }

        _settings.LoadSettings();

        masterMixerGroup = _settings.masterMixerGroup;

        _preloadedClips = clips.AllClips;
    }

    private void CreateSoundObject(SoundChanel chanel)
    {
        GameObject soundGameObject = new GameObject("Sound" + chanel);
        soundGameObject.transform.parent = transform;

        soundSources[(int)chanel] = soundGameObject.AddComponent<AudioSource>();

        soundSources[(int)chanel].outputAudioMixerGroup = _settings.SoundAudioMixerGroup;
        soundSources[(int)chanel].playOnAwake = false;
        soundSources[(int)chanel].priority = 0;
        //soundSources[(int)chanel].mute = _settings.GetSoundMuted();
    }

    private void CreateMusicObject()
    {
        GameObject musicGameObject = new GameObject("Music");
        musicGameObject.transform.parent = transform;

        musicSource = musicGameObject.AddComponent<AudioSource>();

        musicSource.outputAudioMixerGroup = _settings.MusicAudioMixerGroup;
        musicSource.loop = true;
        musicSource.priority = 256;
        musicSource.playOnAwake = false;
        //musicSource.mute = _settings.GetMusicMuted();
        musicSource.volume = 0.08f;
    }

    private void ApplySoundMuted()
    {
        //for (int i = 0; i < soundSources.Length; i++)
        //{
        //    if (soundSources[i] != null)
        //    {
        //        soundSources[i].mute = _settings.GetSoundMuted();
        //    }
        //}
        float volume = _settings.GetSoundMuted() ? -80 : 0;
        masterMixerGroup.audioMixer.SetFloat("VolumeSounds", volume);
    }

    private void ApplyMusicMuted()
    {
        //if (musicSource != null)
        //{
        //    musicSource.mute = _settings.GetMusicMuted();
        //}
        float volume = _settings.GetMusicMuted() ? -80 : 0;
        masterMixerGroup.audioMixer.SetFloat("VolumeMusic", volume);
    }

    public void PlaySound(AudioClip sound, float pitch = 1f, SoundChanel chanel = SoundChanel.First)
    {
        if (sound == null)
        {
            Debug.Log("Sound null or empty");
            return;
        }
        if (soundSources[(int)chanel] == null)
        {
            CreateSoundObject(chanel);
        }

        soundSources[(int)chanel].pitch = pitch;
        soundSources[(int)chanel].PlayOneShot(sound);
    }

    public void PlaySound(string soundName, float pitch = 1f, SoundChanel chanel = SoundChanel.First)
    {
        if (string.IsNullOrEmpty(soundName))
        {
            Debug.Log("Sound null or empty");
            return;
        }

        if (soundSources[(int)chanel] == null)
        {
            CreateSoundObject(chanel);
        }

        AudioClip clip;
        if (_preloadedClips.TryGetValue(soundName, out clip))
        {
            soundSources[(int)chanel].pitch = pitch;
            soundSources[(int)chanel].PlayOneShot(clip);
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

        

        if(musicSource == null)
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

        if (musicSource == null)
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
                musicSource.volume = Mathf.Clamp01(timer / fadeTime) * _settings.musicVolume;
                yield return null;
            }
        }

        musicSource.clip = clip;
        musicSource.Play();

        timer = 0;
        while(timer < fadeTime)
        {
            timer += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Clamp01(timer / fadeTime) * _settings.musicVolume;
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

    private IEnumerator PitchMusic()
    {
        if (musicSource.pitch >= 1)
        {
            while (musicSource.pitch > 0.8f)
            {
                musicSource.pitch -= 0.005f;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (musicSource.pitch < 1f)
            {
                musicSource.pitch += 0.005f;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
public enum SoundChanel
{
    First,
    Second,
    Third,
}
