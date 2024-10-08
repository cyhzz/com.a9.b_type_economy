using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using System;


namespace Com.A9.B_TypeEconomy
{
    public class AdsReward : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IB_TypeItem
    {
        public B_TypeItemID id;
        [SerializeField] string _androidAdUnitId = "Rewarded_Android";
        [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
        string _adUnitId = null; // This will remain null for unsupported platforms

        public event Action OnLoadingComplete;
        public event Action OnLoadingStart;
        public event Action OnStartWatch;
        public event Action OnWatchComplete;
        public event Action OnWatchCompleteDyanmic;
        public event Action OnWatchNoCompleteDyanmic;

        bool loaded;

        void Awake()
        {
            // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
            _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
            // Disable the button until the ad is ready to show:
        }

        // Call this public method when you want to get an ad ready to show.
        public void LoadAd()
        {
            if (Loaded())
            {
                return;
            }
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Advertisement.Load(_adUnitId, this);
            OnLoadingStart?.Invoke();
        }

        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            OnLoadingComplete?.Invoke();
            loaded = true;
        }

        // Implement a method to execute when the user clicks the button:
        public void ShowAd()
        {
            Advertisement.Show(_adUnitId, this);
            loaded = false;
        }

        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                OnWatchComplete?.Invoke();
                OnWatchCompleteDyanmic?.Invoke();
            }
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) == false)
            {
                OnWatchNoCompleteDyanmic?.Invoke();
            }
        }

        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            OnStartWatch?.Invoke();
        }
        public void OnUnityAdsShowClick(string adUnitId) { }

        public bool Loaded()
        {
            return loaded;
        }

        public B_TypeItemID GetID()
        {
            return id;
        }

        public void DestroyAd()
        {
            throw new NotImplementedException();
        }

        public void SetDynamicOnWatchComplete(Action action)
        {
            OnWatchCompleteDyanmic = action;
        }

        public void SetDynamicOnWatchNoComplete(Action action)
        {
            OnWatchNoCompleteDyanmic = action;
        }
    }
}