using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProgress : MonoBehaviour
{
    public static WaveProgress instance;

    public int CurrentWave { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
