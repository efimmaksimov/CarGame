using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [SerializeField] private string musicName;
    private void Start()
    {
        AudioManager.Instance.PlayMusic(musicName);
    }
}
