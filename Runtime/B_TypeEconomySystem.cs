using System;
using System.Collections;
using System.Collections.Generic;
using Com.A9.Singleton;
using UnityEngine;
using UnityEngine.Events;

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
        public event Action OnWatchNoCompleteDyanmic;

        public void SetDynamicOnWatchComplete(Action action);
        public void SetDynamicOnWatchNoComplete(Action action);

        public void DestroyAd();
    }

    public class B_TypeEconomySystem : Singleton<B_TypeEconomySystem>
    {
        public UnityEvent Initialize;
        public Dictionary<B_TypeItemID, IB_TypeItem> ads = new Dictionary<B_TypeItemID, IB_TypeItem>();
        public bool open;
        Dictionary<B_TypeItemID, float> timer = new Dictionary<B_TypeItemID, float>();

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

        void Start()
        {
            StartCoroutine(Counting());
        }

        IEnumerator Counting()
        {
            while (true)
            {
                foreach (var item in timer)
                {
                    if (item.Value > 0)
                    {
                        timer[item.Key] += 1.0f;
                    }
                }
                yield return new WaitForSecondsRealtime(1.0f);
            }
        }

        public bool TimeGT(B_TypeItemID id, float time)
        {
            if (timer.ContainsKey(id))
            {
                return timer[id] >= time;
            }
            else
            {
                return true;
            }
        }

        public void ResetCounter(B_TypeItemID id)
        {
            if (timer.ContainsKey(id))
            {
                timer[id] = 0;
            }
            else
            {
                timer.Add(id, 0);
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
                ResetCounter(id);
            }
        }

        public void DestroyAd(B_TypeItemID id)
        {
            if (!open) return;
            ads[id].DestroyAd();
        }
    }
}


