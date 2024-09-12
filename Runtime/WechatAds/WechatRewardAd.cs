using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;
namespace Com.A9.B_TypeEconomy
{
#if UNITY_WEBGL
    public class WechatRewardAd : MonoBehaviour, IB_TypeItem
    {
        public B_TypeItemID id;
        public string adUnitId;
        public event Action OnLoadingStart;
        public event Action OnLoadingComplete;
        public event Action OnStartWatch;
        public event Action OnWatchComplete;
        WXRewardedVideoAd vd;

        public void DestroyAd()
        {
            throw new NotImplementedException();
        }

        public B_TypeItemID GetID()
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
            vd = WX.CreateRewardedVideoAd(new WXCreateRewardedVideoAdParam()
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
            });
            vd.OnClose((c) =>
            {
                if (c != null && c.isEnded || c == null)
                {
                    OnWatchComplete?.Invoke();
                }
                else
                {

                }
            });
        }
    }
#endif
}