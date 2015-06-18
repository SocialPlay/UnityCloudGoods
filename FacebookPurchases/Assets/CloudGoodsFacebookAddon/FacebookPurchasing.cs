using UnityEngine;
using System.Collections;
using System;
using CloudGoods.CurrencyPurchase;
using CloudGoods.Services.WebCommunication;

public class FacebookPurchasing : MonoBehaviour, IFacebookPurchase {

    public void Init()
    {
        if (!FB.IsInitialized)
            FB.Init(OnInitComplete, null, null);
    }


    public void Purchase(PremiumBundle bundleItem, int amount, Action<string> callback)
    {
        FacebookCurrencyRequest request = new FacebookCurrencyRequest()
        {
            BundleId = int.Parse(bundleItem.BundleID)
        };

        CallHandler.Instance.FacebookPurchaseRequest(request, x =>
        {
            FB.Canvas.Pay(product: "http://99.229.159.50/Webservice2/FacebookCurrencyBundle?BundleID=" + bundleItem.BundleID,
                      quantity: amount,
                      requestId: x.ToString(),
                      callback: delegate(FBResult response)
                      {
                          Debug.Log("Purchase Response: " + response.Text);
                          callback(response.Text);
                      }
            );
        });
    }

    void OnInitComplete()
    {
        Debug.Log("Facebook initialized");
    }
}
