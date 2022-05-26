using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Clips : ScriptableObject
{
    [Header("Music")]
    public AudioClip gameMusic;
    public AudioClip menuMusic;

    [Header("Sound")]
    public AudioClip metalHit;

    [Header("UI Sound")]
    public AudioClip click1;
    public AudioClip upgrade;


    private Dictionary<string, AudioClip> allClips;
    public Dictionary<string, AudioClip> AllClips
    {
        get
        {
            allClips = new Dictionary<string, AudioClip> {
                { nameof(gameMusic), gameMusic },
                { nameof(menuMusic), menuMusic},
                { nameof(metalHit), metalHit},
                { nameof(click1), click1},
                { nameof(upgrade), upgrade}
            };
            return allClips;
        }
    }

    //[MenuItem("AudioManager/Create Clips")]
    //public static void CreateAsset()
    //{
    //    Clips asset = ScriptableObject.CreateInstance<Clips>();
    //    string assetPathAndName = "Assets/Resources/AudioManager/Clips.asset";
    //    AssetDatabase.CreateAsset(asset, assetPathAndName);
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //    EditorUtility.FocusProjectWindow();
    //    Selection.activeObject = asset;
    //}
}
