// ----------------------------------------------------------------------
// <copyright file="KongregatePurchase.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir
// ------------------------------------------------------------------------
using System;
using UnityEngine;
using CloudGoods.SDK.Models;
using CloudGoods.Services;
using LitJson;

namespace CloudGoods.CurrencyPurchase
{
    public class KongregatePurchase : MonoBehaviour, IPlatformPurchaser
    {
        public event Action<PurchasePremiumCurrencyBundleResponse> RecievedPurchaseResponse;
        public event Action<PurchasePremiumCurrencyBundleResponse> OnPurchaseErrorEvent;


        void Start()
        {
            Debug.Log("Kong purchase init");
        }

        public void Purchase(PremiumBundle bundleItem, int amount, string appID)
        {
            Debug.Log("Purchase");

            string data = "'{\"id\":\"" + bundleItem.BundleID + "\",\"amount\":\"" + amount + "\",\"type\":\"Premium\",\"appID\":\"" + CloudGoodsSettings.AppID + "\"}'";
            
            Application.ExternalEval("KongregatePurchase(" + data + ");");

        }

        public void KongregatePurchaseResponse(string data)
        {
            Debug.Log("Kongragate Purchase Successful: " + data);

            PurchasePremiumCurrencyBundleResponse response;

            if(data == "true")
            {
                response = new PurchasePremiumCurrencyBundleResponse()
                {
                    Balance = 0,
                    Message = "Purchase Successful",
                    StatusCode = 1
                };
            }
            else
            {
                response = new PurchasePremiumCurrencyBundleResponse()
                {
                    Balance = 0,
                    Message = "Purchase Failed",
                    StatusCode = 2
                };
            }

            OnReceivedPurchaseResponse(response);
        }


        public void OnReceivedPurchaseResponse(PurchasePremiumCurrencyBundleResponse data)
        {
            if (RecievedPurchaseResponse != null)
                RecievedPurchaseResponse(data);
        }

    }
}

