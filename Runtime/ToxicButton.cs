using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Com.A9.ToxicSystem
{
    public class ToxicButton : MonoBehaviour
    {
        [SerializeField]
        ToxicType id;
        Button button;

        void Start()
        {
            button = GetComponent<Button>();
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


