using UnityEngine;
using System.Collections;
using CloudGoods.Services;
using CloudGoods.SDK.Models;
using CloudGoods.Enums;
using CloudGoods.Services.WebCommunication;

public class CurrencyPurchaseExample : MonoBehaviour {

    public GameObject PremiumCurrencyStore;

    void OnEnable()
    {
        ExampleSceneLogin.OnUserLogin += OnUserLoggedIn;
    }

    void OnDisable()
    {
        ExampleSceneLogin.OnUserLogin -= OnUserLoggedIn;
    }


    void OnUserLoggedIn(CloudGoodsUser user)
    {
        //AccountServices.Login(new LoginRequest(CloudGoodsSettings.ExpSceneUserName, CloudGoodsSettings.ExpScenePassword), OnRegisteredtoSession);
        PremiumCurrencyStore.SetActive(true);
    }
}
