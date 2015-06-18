using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Utilities;
using CloudGoods.Services.WebCommunication;
using CloudGoods.SDK.Models;
using System.Collections.Generic;
using CloudGoods.Services;
using System;

public class CloudGoodsFacebookLogin : MonoBehaviour
{

    public static bool isFBInitialized = false;
    public static Action<CloudGoodsUser> OnUserLoggedIn;

    void Awake()
    {
        CallHandler.Initialize();
        FB.Init(SetInit, OnHideUnity);
    }

    public void PromptFacebookUserLogin()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Already logged in");
        }
        FB.Login("email,publish_actions", FBLoginCallback);
    }

    void FBLoginCallback(FBResult FBResult)
    {
        Debug.Log(FBResult.Text);
        OnLoggedIn();
    }

    void OnLoggedIn()
    {
        if (BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Automatic)
        {
            BuildPlatform.Platform = BuildPlatform.BuildPlatformType.Facebook;
        }

        FB.API("me?fields=name", Facebook.HttpMethod.GET, UserCallback);
    }

    void UserCallback(FBResult fbResult)
    {
        Dictionary<string, object> FBInfo = Facebook.MiniJSON.Json.Deserialize(fbResult.Text) as Dictionary<string, object>;

        LoginByPlatformRequest request = new LoginByPlatformRequest()
        {
            AppId = CloudGoodsSettings.AppID,
            PlatformId = 2,
            PlatformUserId = FB.UserId,
            UserName = FBInfo["name"].ToString(),
            DeviceType = 1
        };

        AccountServices.LoginByPlatform(request, OnUserLogin);
    }

    void OnUserLogin(CloudGoodsUser userResponse)
    {
        Debug.Log("User logged in: " + userResponse.UserName + " with ID: " + userResponse.SessionId);
        OnUserLoggedIn(userResponse);
    }

    private void SetInit()
    {
        isFBInitialized = true; // "enabled" is a property inherited from MonoBehaviour                  
        if (FB.IsLoggedIn)
        {
            OnLoggedIn();
        }
        else
        {
            PromptFacebookUserLogin();
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // pause the game - we will need to hide                                             
            Time.timeScale = 0;
        }
        else
        {
            // start the game back up - we're getting focus again                                
            Time.timeScale = 1;
        }
    }
}
