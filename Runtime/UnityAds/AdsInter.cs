using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Com.A9.B_TypeEconomy
{
    public class AdsInter : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IB_TypeItem
    {
        public B_TypeItemID id;
        [SerializeField] string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
        string _adUnitId;

        public event Action OnLoadingComplete;
        public event Action OnLoadingStart;
        public event Action OnStartWatch;
        public event Action OnWatchComplete;
        public event Action OnWatchCompleteDyanmic;
        public event Action OnWatchNoCompleteDyanmic;

        bool loaded;

        void Awake()
        {
#if UNITY_IOS
            _adUnitId = _iOsAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        }

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            if (Loaded())
            {
                return;
            }
            Advertisement.Load(_adUnitId, this);
            OnLoadingStart?.Invoke();
        }

        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            Advertisement.Show(_adUnitId, this);
            loaded = false;
        }

        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
            OnLoadingComplete?.Invoke();
            loaded = true;
        }

        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string _adUnitId)
        {
            OnStartWatch?.Invoke();
        }
        public void OnUnityAdsShowClick(string _adUnitId) { }
        public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                OnWatchComplete?.Invoke();
                OnWatchCompleteDyanmic?.Invoke();
            }
            if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) == false)
            {
                OnWatchNoCompleteDyanmic?.Invoke();
            }
        }

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
