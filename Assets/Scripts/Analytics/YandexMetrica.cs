using System.Runtime.InteropServices;
using UnityEngine;

public class YandexMetrica : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void NotBounce();
    [DllImport("__Internal")]
    public static extern void EventStartWave(int id);
    [DllImport("__Internal")]
    public static extern void EventWinWave(int id);
    [DllImport("__Internal")]
    public static extern void EventLoseWave(int id);
    [DllImport("__Internal")]
    public static extern void EventWatchRewarded(int id);
    [DllImport("__Internal")]
    public static extern void EventUpgrade(string id, int level);
    [DllImport("__Internal")]
    public static extern void EventBuyCar(int id);
}
