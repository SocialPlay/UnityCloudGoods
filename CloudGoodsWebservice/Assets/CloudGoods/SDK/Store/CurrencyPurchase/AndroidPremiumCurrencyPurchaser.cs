using UnityEngine;
using System.Collections.Generic;
using System;
using LitJson;
using CloudGoods.SDK.Models;
using CloudGoods.Services;
using CloudGoods.Services.WebCommunication;

namespace CloudGoods.CurrencyPurchase
{
    public class AndroidPremiumCurrencyPurchaser : MonoBehaviour, IPlatformPurchaser
    {
        public int currentBundleID = 0;
        public string currentProductID = "";

#if UNITY_ANDROID
        AndroidJavaClass jc;
        AndroidJavaClass cls;
#endif

        public event Action<PurchasePremiumCurrencyBundleResponse> RecievedPurchaseResponse;
        public event Action<PurchasePremiumCurrencyBundleResponse> OnPurchaseErrorEvent;

        void Start()
        {
            gameObject.name = "AndroidCreditPurchaser";
            initStore();
        }

        void initStore()
        {
#if UNITY_ANDROID
            if (string.IsNullOrEmpty(CloudGoodsSettings.AndroidKey))
            {
                Debug.LogError("No Android key has been set, cannot initialize premium bundle store");
                return;
            }

            jc = new AndroidJavaClass("com.example.unityandroidpremiumpurchase.AndroidPurchaser");

            cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            {
                using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
                {

                    Debug.Log("Calling androidpurchas init");
                    jc.CallStatic("InitAndroidPurchaser", obj_Activity, CloudGoodsSettings.AndroidKey, gameObject.name);

                }
            }
#endif
        }

        public void Purchase(PremiumBundle bundleItem, int amount, string userID)
        {
#if UNITY_ANDROID
            if (string.IsNullOrEmpty(CloudGoodsSettings.AndroidKey))
            {
                Debug.LogError("No Android key has been set, cannot purchase from premium store");
                return;
            }

            currentBundleID = int.Parse(bundleItem.BundleID);
            currentProductID = bundleItem.ProductID;

                using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jc.CallStatic("PurchasePremiumCurrencyBundle", obj_Activity, currentProductID);

                }
            
#endif
        }

        void ConsumeAndroidPurchase()
        {
#if UNITY_ANDROID
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jc.CallStatic("ConsumeCurrentPurchase");

            }
#endif
        }

        void OnAndroidPurchaseSuccessful(string message)
        {
            Debug.Log("Received from java message: " + message);

            if (message != "Fail")
            {
                BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
                bundlePurchaseRequest.BundleID = currentBundleID;
                bundlePurchaseRequest.PaymentPlatform = 3;

                GooglePlayReceiptToken receiptToken = JsonMapper.ToObject<GooglePlayReceiptToken>(message);
                receiptToken.OrderInfo = receiptToken.OrderInfo.Replace("\\\"", "\"");
                string jsonData  = JsonMapper.ToJson(receiptToken);

                bundlePurchaseRequest.ReceiptToken = jsonData;

                CallHandler.Instance.PurchasePremiumCurrencyBundle(bundlePurchaseRequest, OnReceivedPurchaseResponse);
            }
            else
            {
                PurchasePremiumCurrencyBundleResponse response = new PurchasePremiumCurrencyBundleResponse();
                response.StatusCode = 0;
                response.Message = message;

                OnPurchaseErrorEvent(response);
            }
        }

        void OnAndroidPurchaseCancelled(string message)
        {
            OnPurchaseErrorEvent(new PurchasePremiumCurrencyBundleResponse()
                {
                    Balance = 0,
                    Message = "Android Purchase Cancelled",
                    StatusCode = 2
                });
        }

        void DebugFromJava(string message)
        {
            Debug.Log("Debug from Java: " + message);
        }
        void ErrorFromAndroid(string msg)
        {
            Debug.LogError("Error from android: " + msg);
        }       


        public void OnReceivedPurchaseResponse(PurchasePremiumCurrencyBundleResponse data)
        {
            Debug.Log("Received purchase response:" + data.Message);

            if (data.StatusCode == 1)
            {
                if (RecievedPurchaseResponse != null)
                {
                    RecievedPurchaseResponse(data);
                    initStore();
                }
            }
            else
            {
                Debug.Log("Purchase was not authentic, consuming Item");

                if (OnPurchaseErrorEvent != null)
                {
                    OnPurchaseErrorEvent(data);
                    initStore();
                }

            }
        }

    }
}