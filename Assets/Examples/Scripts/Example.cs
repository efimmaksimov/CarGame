using System.Collections.Generic;
using InstantGamesBridge;
using UnityEngine;
using UnityEngine.UI;

namespace Examples
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private Button _initializeButton;

        [SerializeField] private GameObject _initializationPanel;

        [SerializeField] private GameObject _initializationErrorPanel;

        [SerializeField] private List<GameObject> _otherPanels;

        private void Start()
        {
            _initializeButton.onClick.AddListener(OnInitializeButtonClicked);

            _initializationPanel.SetActive(true);
            _initializationErrorPanel.SetActive(false);

            foreach (var panel in _otherPanels)
                panel.SetActive(false);
        }

        private void OnInitializeButtonClicked()
        {
            _initializationErrorPanel.SetActive(false);
            Bridge.Initialize(OnInitializationCompleted);
        }

        private void OnInitializationCompleted(bool isInitialized)
        {
            if (isInitialized)
            {
                _initializationPanel.SetActive(false);

                foreach (var panel in _otherPanels)
                    panel.SetActive(true);
            }
            else
                _initializationErrorPanel.SetActive(true);
        }
    }
}