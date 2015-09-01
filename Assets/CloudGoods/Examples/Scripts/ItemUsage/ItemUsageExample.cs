using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Models;
using CloudGoods.SDK.Container;

public class ItemUsageExample : MonoBehaviour {

    public GameObject Cube;
    public GameObject ItemContainerObj;

	void OnEnable()
    {
        ExampleSceneLogin.OnUserLogin += OnUserLogin;
    }

    void OnDisable()
    {
        ExampleSceneLogin.OnUserLogin -= OnUserLogin;
    }

    void OnUserLogin(CloudGoodsUser user)
    {
        ItemContainerObj.SetActive(true);
        ItemContainerManager.RefreshAllPersistentItemContainers();

        GameObject.Instantiate(Cube);
    }
}
