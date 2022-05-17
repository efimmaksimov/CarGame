using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public int timer;
    private void Start()
    {
        Destroy(gameObject, timer);
    }

}
