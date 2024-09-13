using System;
using System.Collections;
using System.Collections.Generic;
using Com.A9.Singleton;
using UnityEngine;
using UnityEngine.Events;
using WeChatWASM;

namespace Com.A9.B_TypeEconomy
{
    public enum B_TypeItemID
    {
        INTER_0,
        REWARD_0
    }

    public interface IB_TypeItem
    {
        public B_TypeItemID GetID();
        public event Action OnLoadingStart;
        public void LoadAd();
        public event Action OnLoadingComplete;
        public bool Loaded();

        public void ShowAd();
        public event Action OnStartWatch;
        public event Action OnWatchComplete;

        public event Action OnWatchCompleteDyanmic;
        public void SetDynamicOnWatchComplete(Action action);

        public void DestroyAd();
    }

    public class B_TypeEconomySystem : Singleton<B_TypeEconomySystem>
    {
        public UnityEvent Initialize;
        public Dictionary<B_TypeItemID, IB_TypeItem> ads = new Dictionary<B_TypeItemID, IB_TypeItem>();
        public bool open;

        protected override void Awake()
        {
            base.Awake();

            if (!open) return;

            for (int i = 0; i < transform.childCount; i++)
            {
                var trans = transform.GetChild(i);
                var al = trans.GetComponent<IB_TypeItem>();
                ads.Add(al.GetID(), al);
            }
        }

        public void InitializeAds()
        {
            if (!open) return;
            Initialize?.Invoke();
        }

        public void LoadAd(B_TypeItemID id)
        {
            if (!open) return;
            ads[id].LoadAd();
        }

        public void ShowAd(B_TypeItemID id)
        {
            if (!open) return;
            if (ads[id].Loaded())
            {
                ads[id].ShowAd();
            }
        }

        public void DestroyAd(B_TypeItemID id)
        {
            if (!open) return;
            ads[id].DestroyAd();
        }
    }
}


