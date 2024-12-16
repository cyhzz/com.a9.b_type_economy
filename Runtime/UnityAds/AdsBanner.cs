using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Com.A9.B_TypeEconomy;
using System;

public class AdsBanner : MonoBehaviour, IB_TypeItem
{
    public B_TypeItemID id;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    public event Action OnLoadingStart;
    public event Action OnLoadingComplete;
    public event Action OnStartWatch;
    public event Action OnWatchComplete;
    public event Action OnWatchClosed;
    public event Action OnWatchCompleteDyanmic;
    public event Action OnWatchNoCompleteDyanmic;

    void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    void OnBannerLoaded()
    {
        ShowBannerAd();
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    bool loaded;
    public B_TypeItemID GetID()
    {
        return id;
    }

    public void LoadAd()
    {
        if (Loaded())
        {
            return;
        }

        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
        OnLoadingStart?.Invoke();
    }

    public bool Loaded()
    {
        return loaded;
    }

    public void ShowAd()
    {
        ShowBannerAd();
    }

    public void SetDynamicOnWatchComplete(Action action)
    {
    }

    public void SetDynamicOnWatchNoComplete(Action action)
    {
    }

    public void DestroyAd()
    {
        HideBannerAd();
    }

    [SerializeField]
    float coolDown = 180;

    public float GetCoolDown()
    {
        return coolDown;
    }
}