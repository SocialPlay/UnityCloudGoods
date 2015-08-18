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
    public GameObject ExampleLoginObject;
    public GameObject ItemBundleStoreObject;

    void Start()
    {
        ExampleSceneLogin.OnUserLogin += OnUserLogin;
        ExampleLoginObject.SetActive(true);
    }

    void OnUserLogin(CloudGoodsUser user)
    {
        ItemBundleStoreObject.SetActive(true);
        itemBundlesLoader.GetItemBundles();
    }


}
