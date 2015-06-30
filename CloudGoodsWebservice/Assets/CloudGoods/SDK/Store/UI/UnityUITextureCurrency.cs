using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CloudGoods.SDK.Models;
using CloudGoods.Enums;
using CloudGoods.SDK;
using System.Collections.Generic;

namespace CloudGoods.SDK.Store.UI
{
    [RequireComponent(typeof(RawImage))]
    public class UnityUITextureCurrency : MonoBehaviour
    {

        public CurrencyType type = CurrencyType.Standard;
        public List<int> StandardCurrencyAccessLocation = null;
        RawImage mTexture;      


        void Start()
        {
            mTexture = GetComponent<RawImage>();
            if (type == CurrencyType.Standard)
            {
                CurrencyManager.GetStandardCurrencyDetails(StandardCurrencyAccessLocation, SetCurrencyLabel);
            }
            else if (type == CurrencyType.Premium)
            {
                CurrencyManager.GetPremiumCurrencyDetails(SetCurrencyLabel);
            }
        }

        void SetCurrencyLabel(string name, Texture2D icon)
        {
            mTexture.texture = icon;
        }
    }
}
