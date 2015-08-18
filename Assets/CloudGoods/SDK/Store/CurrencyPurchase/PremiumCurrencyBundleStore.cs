// ----------------------------------------------------------------------
// <copyright file="CreditBundleStore.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir
// Date: 11/2/2012
// Description: This is a store that sells only credit bundles, to allow from native currency to our currency, if we choose not to support direct buy with platforms native currency.
// ------------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections.Generic;
using CloudGoods.SDK.Models;
using CloudGoods.Enums;
using CloudGoods.SDK.Utilities;
using CloudGoods.Services;

namespace CloudGoods.CurrencyPurchase
{
    public class PremiumCurrencyBundleStore : MonoBehaviour
    {
        public static event Action<PurchasePremiumCurrencyBundleResponse> OnPremiumCurrencyPurchased;
        public static event Action<List<PremiumCurrencyBundle>> OnPremiumBundlesReceived;
        public GameObject Grid;
        [HideInInspector]
        public bool isInitialized = false;
        private bool isWaitingForPlatform = false;

        IGridLoader gridLoader;
        public IPlatformPurchaser platformPurchasor;
        bool isPurchaseRequest = false;

        string domain;

        public CurrencyType type = CurrencyType.Standard;

        void Start()
        {
            this.gameObject.name = "PremiumCurrencyBundleStore";
            Initialize();
        }


        public void Initialize()
        {
            Debug.Log("Initialize Credit Bundles");
            ItemStoreServices.GetPremiumCurrencyBalance(null);

            switch (CloudGoodsSettings.PlatformType)
            {
                case CloudGoodsSettings.BuildPlatformType.Automatic:
                    if (isWaitingForPlatform) return;
                    isWaitingForPlatform = true;
                    CloudGoodsSettings.OnBuildPlatformFound += (platform) =>
                    {
                        Debug.Log("Recived new build platform");
                        Initialize();
                    };
                    return;
                case CloudGoodsSettings.BuildPlatformType.Facebook:
                    platformPurchasor = gameObject.AddComponent<FaceBookPurchaser>();
                    break;
                case CloudGoodsSettings.BuildPlatformType.Kongergate:
                    platformPurchasor = gameObject.AddComponent<KongregatePurchase>();
                    break;
                case CloudGoodsSettings.BuildPlatformType.Android:
                    platformPurchasor = gameObject.AddComponent<AndroidPremiumCurrencyPurchaser>();
                    break;
                case CloudGoodsSettings.BuildPlatformType.IOS:
                    platformPurchasor = gameObject.AddComponent<iOSPremiumCurrencyPurchaser>();
                    GameObject o = new GameObject("iOSConnect");
                    o.AddComponent<iOSConnect>();
                    break;
                case CloudGoodsSettings.BuildPlatformType.CloudGoodsStandAlone:
                    Debug.LogWarning("Cloud Goods Stand alone has not purchase method currently.");
                    break;
                case CloudGoodsSettings.BuildPlatformType.Editor:
                    Debug.LogWarning("Cloud Goods Stand alone has not purchase method currently.");
                    break;
            }

            ItemStoreServices.GetPremiumBundles(new PremiumBundlesRequest((int)CloudGoodsSettings.PlatformType), OnPurchaseBundlesRecieved);

            if (platformPurchasor == null)
            {
                Debug.Log("platform purchasor is null");
                return;
            }

            platformPurchasor.RecievedPurchaseResponse += OnRecievedPurchaseResponse;
            platformPurchasor.OnPurchaseErrorEvent += platformPurchasor_OnPurchaseErrorEvent;

            isInitialized = true;
        }


        void OnDisable()
        {
            if (platformPurchasor != null) platformPurchasor.RecievedPurchaseResponse -= OnRecievedPurchaseResponse;
        }

        void OnPurchaseBundlesRecieved(List<PremiumCurrencyBundle> data)
        {
            Debug.Log("purchase bundles: " + data);

            Debug.Log("Got credit bundles");
            gridLoader = (IGridLoader)Grid.GetComponent(typeof(IGridLoader));
            gridLoader.ItemAdded += OnItemInGrid;
            gridLoader.LoadGrid(data);

            if (OnPremiumBundlesReceived != null)
                OnPremiumBundlesReceived(data);
        }

        void OnItemInGrid(PremiumCurrencyBundle item, GameObject obj)
        {
            PremiumBundle creditBundle = obj.GetComponent<PremiumBundle>();
            creditBundle.Amount = item.CreditAmount.ToString();
            creditBundle.Cost = item.Cost.ToString();

            if (item.Data.Count > 0)
            {
                creditBundle.ProductID = item.Data[0].Value;
            }

            //if (item.CreditPlatformIDs.ContainsKey("IOS_Product_ID"))
            //    creditBundle.ProductID = item.CreditPlatformIDs["IOS_Product_ID"].ToString();

            creditBundle.BundleID = item.ID.ToString();

            creditBundle.PremiumCurrencyName = "";
            creditBundle.Description = item.Description;


            if (!string.IsNullOrEmpty(item.Image))
            {
                ItemTextureCache.GetItemTexture(item.Image, delegate(Texture2D texture)
                {
                    creditBundle.SetIcon(texture);
                });
            }

            creditBundle.SetBundleName(item.Name);

            creditBundle.OnPurchaseRequest = OnPurchaseRequest;
        }

        void OnPurchaseRequest(PremiumBundle item)
        {
            if (!isPurchaseRequest)
            {
                isPurchaseRequest = true;
                if(platformPurchasor != null)
                    platformPurchasor.Purchase(item, 1, AccountServices.ActiveUser.UserID.ToString());
                else
                {
                    Debug.LogError("PlatformPurchasor not initialized");
                    isPurchaseRequest = false;
                }
            }
        }

        void OnRecievedPurchaseResponse(PurchasePremiumCurrencyBundleResponse data)
        {
            Debug.Log("Received purchase response:  " + data);
            isPurchaseRequest = false;

            if (OnPremiumCurrencyPurchased != null)
            {
                OnPremiumCurrencyPurchased(data);
            }
        }

        void platformPurchasor_OnPurchaseErrorEvent(PurchasePremiumCurrencyBundleResponse obj)
        {
            Debug.LogError("Purchase Platform Error: " + obj);

            isPurchaseRequest = false;
        }

    }
}