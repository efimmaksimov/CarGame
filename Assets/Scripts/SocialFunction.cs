using UnityEngine;
using InstantGamesBridge;

public class SocialFunction : MonoBehaviour
{
    private void Start()
    {
        if (Bridge.platform.id != "vk")
        {
            gameObject.SetActive(false);
        }
    }

    public void InviteFriends()
    {
        Bridge.social.InviteFriends();
    }

}
