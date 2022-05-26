using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProgressSetter : MonoBehaviour
{
    public void SetWaveProgress(int index)
    {
        WaveProgress.Instance.SetProgress(index);
    }
}
