using System;
using UnityEngine;
using LitJson;
using CloudGoods.SDK.Models;
using CloudGoods.Services;


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
            FacebookPurchasing = this.gameObject.AddComponent(Type.GetType("FacebookPurchasing")) as IFacebookPurchase;
            FacebookPurchasing.Init();
        }

        public void Purchase(PremiumBundle bundleItem, int amount, string userID)
        {

            if (Type.GetType("FacebookPurchasing") != null)
            {
                FacebookPurchasing = this.gameObject.AddComponent(Type.GetType("FacebookPurchasing")) as IFacebookPurchase;
            }

            if (FacebookPurchasing == null)
            {
                Debug.LogError("Facebook purchase not found. Please add the FacebookPurchase script from the CloudGoodsFacebookAddon folder to this object and drag it as the public reference to the facebookPurchase variable in the inspector");
                return;
            }

            currentBundleID = int.Parse(bundleItem.BundleID);
            Console.WriteLine("Credit bundle purchase:  ID: " + bundleItem.BundleID + " Amount: " + amount);
            Debug.Log("ID: " + bundleItem.BundleID + "\nAmount: " + amount + "\nUserID: " + userID);
            FacebookPurchasing.Purchase(bundleItem, amount, OnReceivedFacebookCurrencyPurchase);
        }

        public void OnReceivedFacebookCurrencyPurchase(string data)
        {
            PurchasePremiumCurrencyBundleResponse errorResponse = new PurchasePremiumCurrencyBundleResponse();

            JsonData facebookData = JsonMapper.ToObject(data);
            try
            {
                if (facebookData["error_code"] != null)
                {
                    errorResponse.StatusCode = int.Parse(facebookData["error_code"].ToString());
                    errorResponse.Message = facebookData["error_message"].ToString();
                    OnPurchaseErrorEvent(errorResponse);

                    return;
                }
            }
            catch
            {

            }

            if(facebookData["status"].ToString() == "completed")
            {
                PurchasePremiumCurrencyBundleResponse response = new PurchasePremiumCurrencyBundleResponse()
                {
                    Balance = 0,
                    Message = "",
                    StatusCode = 0
                };
                OnReceivedPurchaseResponse(response);
            }
            else if(facebookData["status"].ToString() == "initialized")
            {
                //TODO: something when initialized payment
            }
            else
            {
                errorResponse.Message = "Facebook Error Occured";
                errorResponse.StatusCode = 2;
 
                OnPurchaseErrorEvent(errorResponse);
            }
        }

        public void OnReceivedPurchaseResponse(PurchasePremiumCurrencyBundleResponse data)
        {
            if (RecievedPurchaseResponse != null)
                RecievedPurchaseResponse(data);

        }
    }
}

