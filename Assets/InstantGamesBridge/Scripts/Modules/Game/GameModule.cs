#if UNITY_WEBGL
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace InstantGamesBridge.Modules.Game
{
    public class GameModule : MonoBehaviour
    {
#if !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void InstantGamesBridgeGetGameData(string key);

        [DllImport("__Internal")]
        private static extern void InstantGamesBridgeSetGameData(string key, string value);

        [DllImport("__Internal")]
        private static extern void InstantGamesBridgeDeleteGameData(string key);
#else
        private const string _gameDataEditorPlayerPrefsPrefix = "game_data";
#endif

        private Action<bool, string> _getDataCallback;
        //private Queue<Action<bool, string>> _getDataCallbacks = new Queue<Action<bool, string>>();

        private Action<bool> _setDataCallback;

        private Action<bool> _deleteDataCallback;

        private bool _dataIsLoading;


        public void GetData(string key, Action<bool, string> onComplete)
        {
            if (!Bridge.isInitialized)
            {
                StartCoroutine(WaitForInitialize(key, onComplete));
                return;
            }
            if (_dataIsLoading)
            {
                StartCoroutine(WaitForLoading(key, onComplete));
                return;
            }
            _getDataCallback = onComplete;
            _dataIsLoading = true;
            //_getDataCallbacks.Enqueue(onComplete);
#if !UNITY_EDITOR
            InstantGamesBridgeGetGameData(key);
#else
            var data = PlayerPrefs.GetString($"{_gameDataEditorPlayerPrefsPrefix}_{key}", null);
            OnGetGameDataCompletedSuccess(data);
#endif
        }

        private IEnumerator WaitForInitialize(string key, Action<bool, string> onComplete)
        {
            while (!Bridge.isInitialized)
            {
                yield return null;
            }
            GetData(key, onComplete);
        }

        private IEnumerator WaitForLoading(string key, Action<bool, string> onComplete)
        {
            while (_dataIsLoading)
            {
                yield return null;
            }
            GetData(key, onComplete);
        }

        public void SetData(string key, string value, Action<bool> onComplete = null)
        {
            _setDataCallback = onComplete;
#if !UNITY_EDITOR
            InstantGamesBridgeSetGameData(key, value);
#else
            PlayerPrefs.SetString($"{_gameDataEditorPlayerPrefsPrefix}_{key}", value);
            OnSetGameDataCompleted("true");
#endif
        }

        public void SetData(string key, int value, Action<bool> onComplete = null)
        {
            SetData(key, value.ToString(), onComplete);
        }

        public void SetData(string key, bool value, Action<bool> onComplete = null)
        {
            SetData(key, value.ToString(), onComplete);
        }

        public void DeleteData(string key, Action<bool> onComplete = null)
        {
            _deleteDataCallback = onComplete;
#if !UNITY_EDITOR
            InstantGamesBridgeDeleteGameData(key);
#else
            PlayerPrefs.DeleteKey($"{_gameDataEditorPlayerPrefsPrefix}_{key}");
            OnDeleteGameDataCompleted("true");
#endif
        }


        // Called from JS
        private void OnGetGameDataCompletedSuccess(string result)
        {
            //Action<bool, string> getDataCallback = _getDataCallbacks.Dequeue();
            _getDataCallback?.Invoke(true, string.IsNullOrEmpty(result) ? null : result);
            _getDataCallback = null;
            _dataIsLoading = false;
        }

        private void OnGetGameDataCompletedFailed()
        {
            _getDataCallback?.Invoke(false, null);
            _getDataCallback = null;
        }

        private void OnSetGameDataCompleted(string result)
        {
            var isSuccess = result == "true";
            _setDataCallback?.Invoke(isSuccess);
            _setDataCallback = null;
        }

        private void OnDeleteGameDataCompleted(string result)
        {
            var isSuccess = result == "true";
            _deleteDataCallback?.Invoke(isSuccess);
            _deleteDataCallback = null;
        }
    }
}
#endif