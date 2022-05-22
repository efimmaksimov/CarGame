using UnityEngine;
using UnityEngine.SceneManagement;
using InstantGamesBridge;
using System.Collections.Generic;

public class Initializer : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    private void Awake()
    {
        Bridge.Initialize();
    }

    private void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync(1);
    }
}
