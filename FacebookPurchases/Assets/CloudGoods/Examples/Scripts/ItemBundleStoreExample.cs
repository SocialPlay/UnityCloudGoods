using UnityEngine;
using System.Collections;
using CloudGoods.ItemBundles;
using CloudGoods;
using CloudGoods.SDK.Models;
using CloudGoods.Services.WebCommunication;
using CloudGoods.Services;

public class ItemBundleStoreExample : MonoBehaviour
{

    public UnityUIItemBundleLoader itemBundlesLoader;
    public GameObject StoreDisplay;

    void Awake()
    {
        CallHandler.CloudGoodsInitilized += CallHandler_CloudGoodsInitilized;
        CallHandler.Initialize();
    }

    void CallHandler_CloudGoodsInitilized()
    {
        CloudGoods.Services.AccountServices.Login(new LoginRequest(CloudGoodsSettings.ExpSceneUserName, CloudGoodsSettings.ExpScenePassword), OnRegisteredtoSession);
    }

    void OnRegisteredtoSession(CloudGoodsUser user)
    {
        StoreDisplay.SetActive(true);
        itemBundlesLoader.GetItemBundles();
    }


}
