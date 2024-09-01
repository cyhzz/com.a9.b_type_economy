// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using GoogleMobileAds;
// using GoogleMobileAds.Api;
// using System;
// using UnityEngine.UI;
// using UnityEngine.Events;

// public class GoogleAdsInter : MonoBehaviour, IInters
// {
//     // These ad units are configured to always serve test ads.
// #if UNITY_ANDROID
//     private string _adUnitId = "ca-app-pub-9858607105039346/6437224291";
// #elif UNITY_IPHONE
//     private string _adUnitId = "ca-app-pub-9858607105039346/6437224291";
// #else
//   private string _adUnitId = "unused";
// #endif

//     private InterstitialAd _interstitialAd;
//     public event Action OnLoadingComplete;
//     public event Action OnLoadingStart;
//     public event Action OnWatchComplete;
//     public event Action OnStartWatch;
//     public bool loaded;

//     /// <summary>
//     /// Loads the interstitial ad.
//     /// </summary>
//     public void LoadAd()
//     {
//         // Clean up the old ad before loading a new one.
//         if (_interstitialAd != null)
//         {
//             _interstitialAd.Destroy();
//             _interstitialAd = null;
//         }

//         Debug.Log("Loading the interstitial ad.");

//         // create our request used to load the ad.
//         var adRequest = new AdRequest();
//         OnLoadingStart?.Invoke();
//         loaded = false;
//         // send the request to load the ad.
//         InterstitialAd.Load(_adUnitId, adRequest,
//             (InterstitialAd ad, LoadAdError error) =>
//             {
//                 // if error is not null, the load request failed.
//                 if (error != null || ad == null)
//                 {
//                     Debug.LogError("interstitial ad failed to load an ad " +
//                                    "with error : " + error);
//                     return;
//                 }

//                 loaded = true;
//                 OnLoadingComplete?.Invoke();
//                 Debug.Log("Interstitial ad loaded with response : "
//                           + ad.GetResponseInfo());

//                 _interstitialAd = ad;
//             });
//     }
//     /// <summary>
//     /// Shows the interstitial ad.
//     /// </summary>
//     public void ShowAd()
//     {
//         if (_interstitialAd != null && _interstitialAd.CanShowAd())
//         {
//             Debug.Log("Showing interstitial ad.");
//             loaded = false;
//             OnStartWatch?.Invoke();
//             _interstitialAd.Show();
//         }
//         else
//         {
//             Debug.LogError("Interstitial ad is not ready yet.");
//         }
//     }

//     private void RegisterEventHandlers(InterstitialAd interstitialAd)
//     {
//         // Raised when the ad is estimated to have earned money.
//         interstitialAd.OnAdPaid += (AdValue adValue) =>
//         {
//             Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
//                 adValue.Value,
//                 adValue.CurrencyCode));
//         };
//         // Raised when an impression is recorded for an ad.
//         interstitialAd.OnAdImpressionRecorded += () =>
//         {
//             Debug.Log("Interstitial ad recorded an impression.");
//         };
//         // Raised when a click is recorded for an ad.
//         interstitialAd.OnAdClicked += () =>
//         {
//             Debug.Log("Interstitial ad was clicked.");
//         };
//         // Raised when an ad opened full screen content.
//         interstitialAd.OnAdFullScreenContentOpened += () =>
//         {
//             Debug.Log("Interstitial ad full screen content opened.");
//         };
//         // Raised when the ad closed full screen content.
//         interstitialAd.OnAdFullScreenContentClosed += () =>
//         {
//             Debug.Log("Interstitial ad full screen content closed.");
//             OnWatchComplete?.Invoke();
//         };
//         // Raised when the ad failed to open full screen content.
//         interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
//         {
//             Debug.LogError("Interstitial ad failed to open full screen content " +
//                            "with error : " + error);
//         };
//     }

//     public bool Loaded()
//     {
//         return loaded;
//     }
// }
