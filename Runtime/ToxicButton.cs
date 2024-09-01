using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Com.A9.ToxicSystem
{
    public class AdsButton : MonoBehaviour
    {
        [SerializeField]
        ToxicType id;
        [SerializeField]
        Button button;

        void Start()
        {
            var ad = ToxicSystem.instance.ads[id];
            ad.OnLoadingStart += () => { button.interactable = false; };
            ad.OnLoadingComplete += () => { button.interactable = true; };
            ad.OnStartWatch += () => { button.interactable = false; };

            button.onClick.AddListener(ShowAd);
            button.interactable = ad.Loaded();
        }

        void ShowAd()
        {
            ToxicSystem.instance.ShowAd(id);
        }
    }
}


