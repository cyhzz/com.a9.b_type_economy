using System;
using System.Collections;
using System.Collections.Generic;
using Com.A9.Singleton;
using UnityEngine;
using UnityEngine.Events;

namespace Com.A9.ToxicSystem
{
    public enum ToxicType
    {
        INTER_0,
        REWARD_0
    }

    public interface IToxic
    {
        public ToxicType GetID();
        public event Action OnLoadingStart;
        public void LoadAd();
        public event Action OnLoadingComplete;
        public bool Loaded();

        public void ShowAd();
        public event Action OnStartWatch;
        public event Action OnWatchComplete;

        public void DestroyAd();
    }

    public class ToxicSystem : Singleton<ToxicSystem>
    {
        public UnityEvent Initialize;
        public Dictionary<ToxicType, IToxic> ads = new Dictionary<ToxicType, IToxic>();
        public bool open;

        protected override void Awake()
        {
            base.Awake();

            if (!open) return;

            for (int i = 0; i < transform.childCount; i++)
            {
                var trans = transform.GetChild(i);
                var al = trans.GetComponent<IToxic>();
                ads.Add(al.GetID(), al);
            }
        }

        public void InitializeAds()
        {
            if (!open) return;
            Initialize?.Invoke();
        }

        public void LoadAd(ToxicType id)
        {
            if (!open) return;
            ads[id].LoadAd();
        }

        public void ShowAd(ToxicType id)
        {
            if (!open) return;
            if (ads[id].Loaded())
            {
                ads[id].ShowAd();
            }
        }

        public void DestroyAd(ToxicType id)
        {
            if (!open) return;
            ads[id].DestroyAd();
        }
    }
}


