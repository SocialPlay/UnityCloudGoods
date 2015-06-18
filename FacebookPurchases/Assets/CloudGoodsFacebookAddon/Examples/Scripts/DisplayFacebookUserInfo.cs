using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CloudGoods.SDK.Models;

public class DisplayFacebookUserInfo : MonoBehaviour {

    public Text UserLoginText;

	// Use this for initialization
	void OnEnable () {
	    //Ca;l.OnUserAuthorized += OnUserLogin;
        CloudGoodsFacebookLogin.OnUserLoggedIn += OnUserLogin;
	}
	
	// Update is called once per frame
	void OnDisable () {
        CloudGoodsFacebookLogin.OnUserLoggedIn -= OnUserLogin;
        //CloudGoods.OnUserAuthorized -= OnUserLogin;
	}

    void OnUserLogin(CloudGoodsUser userResponse)
    {
        UserLoginText.text = "User logged in: " + userResponse.UserName + " with ID: " + userResponse.UserID;
    }
}
