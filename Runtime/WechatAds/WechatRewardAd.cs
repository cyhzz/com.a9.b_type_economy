using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_WEBGL
using WeChatWASM;
namespace Com.A9.B_TypeEconomy
{
    public class WechatRewardAd : MonoBehaviour, IB_TypeItem
    {
        public B_TypeItemID id;
        public string adUnitId;
        public event Action OnLoadingStart;
        public event Action OnLoadingComplete;
        public event Action OnStartWatch;
        public event Action OnWatchComplete;
        public event Action OnWatchCompleteDyanmic;
        public event Action OnWatchNoCompleteDyanmic;

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
            try{
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
            catch (Exception e)
            {
                Debug.LogError(e);
            }
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
                    OnWatchCompleteDyanmic?.Invoke();
                }
                else
                {
                    OnWatchNoCompleteDyanmic?.Invoke();
                }
            });
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
#endif