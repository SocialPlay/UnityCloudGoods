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

    void Start()
    {
        itemBundlesLoader.GetItemBundles();
    }


}
