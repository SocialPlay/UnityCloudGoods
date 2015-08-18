using UnityEngine;
using System.Collections;
using CloudGoods.Services;
using CloudGoods.SDK.Models;
using CloudGoods.Enums;
using CloudGoods.SDK.Store;
using CloudGoods.ItemBundles;
using CloudGoods.Services.WebCommunication;

public class StoreExample : MonoBehaviour
{
    public StoreInitializer initializer;
    public GameObject ExampleLoginObject;
    public GameObject StoreExampleObject;

    void Start()
    {
        ExampleSceneLogin.OnUserLogin += OnUserLogin;
        ExampleLoginObject.SetActive(true);
    }

    void OnUserLogin(CloudGoodsUser user)
    {
        StoreExampleObject.SetActive(true);
        initializer.InitializeStore();
    }
}
