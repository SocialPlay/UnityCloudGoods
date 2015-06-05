using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Utilities;
using CloudGoods.Services.WebCommunication;
using CloudGoods.SDK.Models;
using CloudGoods.Services;
using CloudGoods.CurrencyPurchase;

public class KongregateLogin : MonoBehaviour
{

    public PremiumCurrencyBundleStore store;

    void Start()
    {
        this.gameObject.name = "KongragateLogin";
        Application.ExternalEval(
            "if(typeof(kongregateUnitySupport) != 'undefined'){" +
            " kongregateUnitySupport.initAPI('KongragateLogin', 'OnKongregateAPILoaded');" +
            "};"
            );
    }

    public void OnKongregateAPILoaded(string userInfoString)
    {
        if (BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Automatic)
        {
            BuildPlatform.Platform = BuildPlatform.BuildPlatformType.Kongergate;
        }
        string[] parts = userInfoString.Split('|');

        LoginByPlatformRequest loginRequest = new LoginByPlatformRequest()
        {
            AppId = CloudGoodsSettings.AppID,
            PlatformId = 3,
            PlatformUserId = parts[0],
            UserName = parts[1],
            DeviceType = 3
        };

        CallHandler.Instance.LoginByPlatform(loginRequest, OnUserLoggedIn);
    }

    void OnUserLoggedIn(CloudGoodsUser user)
    {
        store.gameObject.SetActive(true);
    }

}
