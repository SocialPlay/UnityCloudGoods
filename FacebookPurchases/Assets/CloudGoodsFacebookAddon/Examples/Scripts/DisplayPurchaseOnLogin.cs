using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Models;

public class DisplayPurchaseOnLogin : MonoBehaviour {

    public GameObject UnityCurrencyDisplay;

	// Use this for initialization
	void OnEnable () {
        CloudGoodsFacebookLogin.OnUserLoggedIn += OnUserLoggedIn;
	}

    void OnDisable()
    {
        CloudGoodsFacebookLogin.OnUserLoggedIn -= OnUserLoggedIn;
    }
	
    void OnUserLoggedIn(CloudGoodsUser user)
    {
        UnityCurrencyDisplay.SetActive(true);
    }
}
