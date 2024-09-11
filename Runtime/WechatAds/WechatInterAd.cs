using System;
using System.Collections;
using System.Collections.Generic;
using Com.A9.ToxicSystem;
using UnityEngine;
using WeChatWASM;
namespace Com.A9.ToxicSystem
{
    public class WechatInterAd : MonoBehaviour, IToxic
    {
#if UNITY_WEBGL
        public ToxicType id;
        public string adUnitId;
        public event Action OnLoadingStart;
        public event Action OnLoadingComplete;
        public event Action OnStartWatch;
        public event Action OnWatchComplete;
        WXInterstitialAd vd;

        public void DestroyAd()
        {
            throw new NotImplementedException();
        }

        public ToxicType GetID()
        {
            return id;
        }
        bool loaded;
        public void LoadAd()
        {
            if (loaded)
            {
                return;
            }
            OnLoadingStart?.Invoke();
            Debug.Log("Start Load Reward");
            vd = WX.CreateInterstitialAd(new WXCreateInterstitialAdParam()
            {
                adUnitId = adUnitId
            });
            vd.OnLoad((c) =>
            {
                Debug.Log("Load Reward Complete");
                OnLoadingComplete?.Invoke();
                loaded = true;
            });
        }

        public bool Loaded()
        {
            return loaded;
        }

        public void ShowAd()
        {
            vd.Show((c) =>
            {
                OnStartWatch?.Invoke();
            }, (c) =>
            {
                Debug.LogError(c.errMsg);
            });
        }
#endif
    }
}