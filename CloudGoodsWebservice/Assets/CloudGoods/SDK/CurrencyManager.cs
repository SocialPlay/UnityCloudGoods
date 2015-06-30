using UnityEngine;
using System;
using System.Collections;
using CloudGoods.Services.WebCommunication;
using CloudGoods.SDK.Utilities;
using CloudGoods.Enums;
using CloudGoods.Services;
using CloudGoods.SDK.Models;
using System.Collections.Generic;

namespace CloudGoods.SDK
{
    public static class CurrencyManager
    {
        public class CurrencyDetails
        {
            public string Name;
            public Texture2D Icon;
        }

        private static CurrencyDetails PremiumInfo = null;
        private static int? PremiumAmount = null;
        private static CurrencyDetails StandardInfo = null;
        private static int? StandardAmount = null;


        private static bool IsGettingWolrdCurrency = false;

        private static Action<string, Texture2D> RecivedPremiumDetails;
        private static Action<int> RecivecdPremiumAmount;
        private static Action<string, Texture2D> RecivedStandardDetails;
        private static Action<int> RecivecdStandardAmount;
        private static List<int> StandardCurrencyLoaction =null;

        public static void GetPremiumCurrencyDetails(Action<string, Texture2D> callback)
        {
            if (PremiumInfo == null)
            {
                GetWolrdCurrencyInfo(StandardCurrencyLoaction);
                RecivedPremiumDetails += callback;
            }
            else
            {
                callback(PremiumInfo.Name, PremiumInfo.Icon);
            }
        }

        public static void GetPremiumCurrencyBalance(Action<int> callback, bool forceUpdate = true)
        {
            if (PremiumAmount == null)
            {
                GetWolrdCurrencyInfo(StandardCurrencyLoaction);
                RecivecdPremiumAmount += callback;
            }
            else if (forceUpdate)
            {
                ItemStoreServices.GetPremiumCurrencyBalance(balance =>
                {
                    callback(balance.Amount);
                });
            }
            else
            {
                callback(PremiumAmount.GetValueOrDefault(0));
            }
        }

        public static void GetStandardCurrencyBalance(List<int> location, Action<int> callback, bool forceUpdate = true)
        {
            StandardCurrencyLoaction = location;
            if (StandardAmount == null)
            {
                GetWolrdCurrencyInfo(StandardCurrencyLoaction);
                RecivecdStandardAmount += callback;
            }
            else if (forceUpdate)
            {
                ItemStoreServices.GetStandardCurrencyBalance(new StandardCurrencyBalanceRequest(StandardCurrencyLoaction), balance =>
                {                    
                    StandardAmount = balance.Total;
                    callback(StandardAmount.GetValueOrDefault(0));
                    Debug.Log(StandardAmount);
                });
            }
            else
            {
                callback(StandardAmount.GetValueOrDefault(0));
            }

        }

        public static void GetStandardCurrencyDetails(List<int> location, Action<string, Texture2D> callback)
        {
            StandardCurrencyLoaction = location;
            if (StandardInfo == null)
            {
                GetWolrdCurrencyInfo(StandardCurrencyLoaction);
                RecivedStandardDetails += callback;
            }
            else
            {
                callback(StandardInfo.Name, StandardInfo.Icon);
            }
        }


        private static void GetWolrdCurrencyInfo(List<int> location)
        {
            if (!IsGettingWolrdCurrency)
            {
                IsGettingWolrdCurrency = true;
                ItemStoreServices.GetCurrencyInfo(worldCurrencyInfo =>
                {
                    ItemTextureCache.GetItemTexture(worldCurrencyInfo.PremiumCurrencyImage, icon =>
                    {
                        PremiumInfo = new CurrencyDetails()
                        {
                            Name = worldCurrencyInfo.PremiumCurrencyName,
                            Icon = icon                            
                        };
                        if (RecivedPremiumDetails != null)
                        {
                            RecivedPremiumDetails(PremiumInfo.Name, PremiumInfo.Icon);
                        }
                    });
                    ItemStoreServices.GetPremiumCurrencyBalance(premiumCurrencyResponse =>
                    {
                        PremiumAmount = premiumCurrencyResponse.Amount;
                        if (RecivecdPremiumAmount != null)
                        {
                            RecivecdPremiumAmount(premiumCurrencyResponse.Amount);
                        }
                    });

                    ItemTextureCache.GetItemTexture(worldCurrencyInfo.StandardCurrencyImage, icon =>
                    {
                        StandardInfo = new CurrencyDetails()
                        {
                            Name = worldCurrencyInfo.StandardCurrencyName,
                            Icon = icon
                        };
                        if (RecivedStandardDetails != null)
                        {
                            RecivedStandardDetails(StandardInfo.Name, StandardInfo.Icon);
                        }
                    });
                    ItemStoreServices.GetStandardCurrencyBalance(new StandardCurrencyBalanceRequest(StandardCurrencyLoaction), standardCurrencybalance =>
                    {  
                        StandardAmount = standardCurrencybalance.Total;
                        if (RecivecdStandardAmount != null)
                        {
                            RecivecdStandardAmount(standardCurrencybalance.Total);
                        }
                    });
                });
            }
        }
    }
}
