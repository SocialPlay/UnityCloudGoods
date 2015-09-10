using UnityEngine;
using System.Collections;
using CloudGoods.Services.WebCommunication;
using CloudGoods.SDK.Models;

public class KongregatePurchaseExample : MonoBehaviour {

    public KongregateAPI KongAPI;

    public GameObject premiumCurrencyWindow;

    void OnEnable()
    {
        CallHandler.CloudGoodsInitilized += CallHandler_CloudGoodsInitilized;
        KongregateAPI.OnUserLoggedIn += OnUserLoggedIn;
    }

    void OnDisable()
    {
        CallHandler.CloudGoodsInitilized += CallHandler_CloudGoodsInitilized;
        KongregateAPI.OnUserLoggedIn += OnUserLoggedIn;
    }

    // Use this for initialization
    void Awake()
    {
        CallHandler.Initialize();
    }

    void CallHandler_CloudGoodsInitilized()
    {
        KongAPI.Connect();
    }

    void OnUserLoggedIn(CloudGoodsUser user)
    {
        premiumCurrencyWindow.SetActive(true);
    }
}
