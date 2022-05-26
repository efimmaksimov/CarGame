using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private int timer;

    public void Initialize(AudioClip clip, EnemyType type, float pitch)
    {
        AudioManager.Instance.PlaySound(clip, pitch, (SoundChanel)type);
        Destroy(gameObject, timer);
    }

}
