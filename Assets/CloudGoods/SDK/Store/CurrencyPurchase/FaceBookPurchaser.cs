using System;
using UnityEngine;
using LitJson;
using CloudGoods.SDK.Models;


namespace CloudGoods.CurrencyPurchase
{
    public class FaceBookPurchaser : MonoBehaviour, IPlatformPurchaser
    {
        public event Action<PurchasePremiumCurrencyBundleResponse> RecievedPurchaseResponse;
        public event Action<PurchasePremiumCurrencyBundleResponse> OnPurchaseErrorEvent;
        public int currentBundleID = 0;

        public IFacebookPurchase FacebookPurchasing;

        void Start()
        {
#if UNITY_WEBPLAYER
            FacebookPurchasing = this.gameObject.AddComponent<FacebookPurchasing>() as IFacebookPurchase;
            FacebookPurchasing.Init();
#endif
        }

        public void Purchase(PremiumBundle bundleItem, int amount, string userID)
        {
#if UNITY_WEBPLAYER
            if (Type.GetType("FacebookPurchasing") != null)
            {
                FacebookPurchasing = this.gameObject.AddComponent<FacebookPurchasing>() as IFacebookPurchase;
            }
#endif

            if (FacebookPurchasing == null)
            {
                Debug.LogError("Facebook purchase not found. Please add the FacebookPurchase script from the CloudGoodsFacebookAddon folder to this object and drag it as the public reference to the facebookPurchase variable in the inspector");
                return;
            }

            currentBundleID = int.Parse(bundleItem.BundleID);
            FacebookPurchasing.Purchase(bundleItem, amount, OnReceivedFacebookCurrencyPurchase);
        }

        public void OnReceivedFacebookCurrencyPurchase(string data)
        {
            Debug.Log("data: " + data);
            JsonData receiptData = JsonMapper.ToObject(data);

            if (data.Contains("error"))
            {
                PurchasePremiumCurrencyBundleResponse response = new PurchasePremiumCurrencyBundleResponse()
                {
                    Balance = 0,
                    Message = receiptData["error_message"].ToString(),
                    StatusCode = int.Parse(receiptData["error_code"].ToString())
                };

                OnPurchaseErrorEvent(response);
                return;
            }

            if (receiptData["status"].ToString() == "completed")
            {
                PurchasePremiumCurrencyBundleResponse response = new PurchasePremiumCurrencyBundleResponse()
                {
                    Balance = 0,
                    Message = receiptData["status"].ToString(),
                    StatusCode = 0
                };

                OnReceivedPurchaseResponse(response);
            }
            else
            {
                OnPurchaseErrorEvent(null);
            }

        }

        public void OnReceivedPurchaseResponse(PurchasePremiumCurrencyBundleResponse data)
        {
            if (RecievedPurchaseResponse != null)
                RecievedPurchaseResponse(data);
        }
    }
}

