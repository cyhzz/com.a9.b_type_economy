using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Com.A9.B_TypeEconomy
{
    public class B_TypeItemButton : MonoBehaviour
    {
        [SerializeField]
        B_TypeItemID id;
        Button button;
        [SerializeField]
        UnityEvent OnLoadingStart;
        [SerializeField]
        UnityEvent OnLoadingComplete;
        [SerializeField]
        UnityEvent OnStartWatch;
        [SerializeField]
        UnityEvent OnInteractive;
        [SerializeField]
        UnityEvent OnDisabled;

        void Start()
        {
            button = GetComponent<Button>();
            var ad = B_TypeEconomySystem.instance.ads[id];

            if (B_TypeEconomySystem.instance.test_mode)
            {
                button.onClick.AddListener(ShowMockAd);
                OnInteractive?.Invoke();
                button.interactable = true;
                return;
            }

            ad.OnLoadingStart += () =>
            {
                button.interactable = false;
                OnLoadingStart?.Invoke();
                OnDisabled?.Invoke();
            };

            ad.OnLoadingComplete += () =>
            {
                button.interactable = true;
                OnLoadingComplete?.Invoke();
                OnInteractive?.Invoke();
            };

            ad.OnStartWatch += () =>
            {
                button.interactable = false;
                OnStartWatch?.Invoke();
                OnDisabled?.Invoke();
            };

            button.onClick.AddListener(ShowAd);
            button.interactable = ad.Loaded();

            if (button.interactable)
            {
                OnInteractive?.Invoke();
            }
            else
            {
                OnDisabled?.Invoke();
            }
        }

        void ShowAd()
        {
            B_TypeEconomySystem.instance.ShowAd(id);
        }

        void ShowMockAd()
        {
            B_TypeEconomySystem.instance.ShowMockAd(id);
            button.interactable = false;
            OnStartWatch?.Invoke();
            OnDisabled?.Invoke();
        }

        public void MockLoaded()
        {
            button.interactable = true;
            OnLoadingComplete?.Invoke();
            OnInteractive?.Invoke();
        }
    }
}


