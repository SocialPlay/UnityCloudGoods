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


    void Start()
    {
        initializer.InitializeStore();
    }
}
