using System;
using InstantGamesBridge;
using UnityEngine;
using UnityEngine.UI;

namespace Examples
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private InputField _coinsInput;

        [SerializeField] private InputField _levelInput;

        [SerializeField] private Button _setGameDataButton;

        [SerializeField] private Button _getGameDataButton;

        [SerializeField] private Button _deleteGameDataButton;

        [SerializeField] private GameObject _overlay;

        private const string _playerProgressKey = "player_progress";


        private void OnEnable()
        {
            _setGameDataButton.onClick.AddListener(OnSetGameDataButtonClicked);
            _getGameDataButton.onClick.AddListener(OnGetGameDataButtonClicked);
            _deleteGameDataButton.onClick.AddListener(OnDeleteGameDataButtonClicked);
        }

        private void OnDisable()
        {
            _setGameDataButton.onClick.RemoveAllListeners();
            _getGameDataButton.onClick.RemoveAllListeners();
            _deleteGameDataButton.onClick.RemoveAllListeners();
        }


        private void OnSetGameDataButtonClicked()
        {
            _overlay.SetActive(true);

            int.TryParse(_coinsInput.text, out var coins);
            var level = _levelInput.text;
            var json = JsonUtility.ToJson(new PlayerProgressContainer(coins, level));

            Bridge.game.SetData(_playerProgressKey, json, success => { _overlay.SetActive(false); });
        }

        private void OnGetGameDataButtonClicked()
        {
            _overlay.SetActive(true);

            Bridge.game.GetData(
                _playerProgressKey, 
                (success, data) =>
                {
                    try
                    {
                        var playerProgressContainer = JsonUtility.FromJson<PlayerProgressContainer>(data);
                        _coinsInput.text = playerProgressContainer.coins.ToString();
                        _levelInput.text = playerProgressContainer.level;
                    }
                    catch (Exception) { }

                    _overlay.SetActive(false);
                });
        }

        private void OnDeleteGameDataButtonClicked()
        {
            _overlay.SetActive(true);

            Bridge.game.DeleteData(
                _playerProgressKey,
                success =>
                {
                    _coinsInput.text = string.Empty;
                    _levelInput.text = string.Empty;
                    _overlay.SetActive(false);
                });
        }


        private class PlayerProgressContainer
        {
            public int coins;

            public string level;

            public PlayerProgressContainer(int coins, string level)
            {
                this.coins = coins;
                this.level = level;
            }
        }
    }
}