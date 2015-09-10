using UnityEngine;
using System.Collections;
using CloudGoods.Services.WebCommunication;
using CloudGoods.SDK.Models;
using UnityEngine.UI;

public class KongregateLoginExample : MonoBehaviour {

    public KongregateAPI KongAPI;

    public GameObject UserInfoDisplayWindow;

    public Text UserName;
    public Text UserEmail;
    public Text UserId;
    public Text SessionId;
    public Text isNewPlayer;

    void OnEnable()
    {
        CallHandler.CloudGoodsInitilized += CallHandler_CloudGoodsInitilized;
        KongregateAPI.OnUserLoggedIn += OnUserLoggedIn;
    }

    void OnDisable()
    {
        CallHandler.CloudGoodsInitilized += CallHandler_CloudGoodsInitilized;
        KongregateAPI.OnUserLoggedIn += OnUserLoggedIn;
    }

	// Use this for initialization
	void Awake () {
        CallHandler.Initialize();
	}

    void CallHandler_CloudGoodsInitilized()
    {
        KongAPI.Connect();
    }

    void OnUserLoggedIn(CloudGoodsUser user)
    {
        UserName.text = user.UserName;
        UserEmail.text = "N/A";
        UserId.text = user.UserID;
        SessionId.text = user.SessionId.ToString();
        isNewPlayer.text = user.IsNewUserToWorld.ToString();

        UserInfoDisplayWindow.SetActive(true);
    }
}
