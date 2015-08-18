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

    public GameObject ExampleLoginObject;

    void Start()
    {
        ExampleSceneLogin.OnUserLogin += OnUserLoggedIn;
        ExampleLoginObject.SetActive(true);
    }

    void OnUserLoggedIn(CloudGoodsUser user)
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
