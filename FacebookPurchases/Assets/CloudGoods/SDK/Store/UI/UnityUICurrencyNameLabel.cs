using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CloudGoods.Enums;
using CloudGoods.SDK;
using System.Collections.Generic;

namespace CloudGoods.SDK.Store.UI
{
    [RequireComponent(typeof(Text))]
    public class UnityUICurrencyNameLabel : MonoBehaviour
    {
        public string prefix;
        public string suffix;
        public CurrencyType type = CurrencyType.Standard;
        public List<int> StandardCurrencyAccessLocation = new List<int>();
        Text mLabel;

        void Start()
        {
            mLabel = GetComponent<Text>();
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
            mLabel.text = string.Format("{0}{1}{2}", prefix, name, suffix);
        }
    }
}
