using UnityEngine;
using System.Collections;
using CloudGoods.Services.WebCommunication;
using CloudGoods.Services;
using CloudGoods.SDK.Models;
using System.Collections.Generic;
using CloudGoods.SDK.Container;
using UnityEngine.UI;

public class UserItemContainerExample : MonoBehaviour {



    public List<PersistentItemContainer> containers;
    public GameObject tagsMenu;
    bool isTagsMenuOpen = false;

    void Awake()
    {
        CallHandler.CloudGoodsInitilized += CallHandler_CloudGoodsInitilized;
        CallHandler.Initialize();
    }

    void CallHandler_CloudGoodsInitilized()
    {
        AccountServices.Login(new LoginRequest(CloudGoodsSettings.ExpSceneUserName, CloudGoodsSettings.ExpScenePassword), OnRegisteredtoSession);
    }

    void OnRegisteredtoSession(CloudGoodsUser user)
    {
        ItemContainerManager.RefreshAllPersistentItemContainers();
    }

    public void TogglerTagsMenu()
    {
        if (isTagsMenuOpen)
        {
            tagsMenu.SetActive(false);
            isTagsMenuOpen = false;
        }
        else
        {
            tagsMenu.SetActive(true);
            isTagsMenuOpen = true;
        }
    }

    public void CloseTagsMenu()
    {
        tagsMenu.SetActive(false);
        isTagsMenuOpen = false;

    }
}
