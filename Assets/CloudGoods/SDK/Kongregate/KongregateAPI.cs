using UnityEngine;
using LitJson;
using CloudGoods.SDK.Models;
using CloudGoods.Services;
using System;
using System.Collections;

/// <summary>
/// Provides quick access to Kongregate's API system, allowing the submission of stats. It is best to handle setup of this class
/// as soon as possible in your application.
/// </summary>
public class KongregateAPI : MonoBehaviour
{

    public static Action<CloudGoodsUser> OnUserLoggedIn;

    bool Connected = false;

    void Awake()
    {
        gameObject.name = "KongregateAPI";
    }

    /// <summary>
    /// Connect to Kongregate's API service.
    /// </summary>
    /////
    public void Connect()
    {
        if (!Connected)
        {
            Application.ExternalCall("GetKongregateAPI");
            Connected = true;
        }
        else
            Debug.LogWarning("You are attempting to connect to Kongregate's API multiple times. You only need to connect once.");
    }

    private void OnKongregateAPILoaded(string userInfoString)
    {
        Debug.Log("On API Loaded: " + userInfoString);

        string[] splitString = userInfoString.Split('|');

        Debug.Log("User name: " + splitString[0]);
        Debug.Log("User Id: " + splitString[1]);
        Debug.Log("Game Token: " + splitString[2]);

        LoginByPlatformRequest request = new LoginByPlatformRequest()
        {
            AppId = CloudGoodsSettings.AppID,
            PlatformId = 4,
            PlatformUserId = splitString[1],
            UserName = splitString[0],
            DeviceType = 1
        };

        AccountServices.LoginByPlatform(request, OnUserLogin);

    }

    void OnUserLogin(CloudGoodsUser userResponse)
    {
        Debug.Log("User logged in: " + userResponse.UserName + " with Session ID: " + userResponse.SessionId + "   with UserID: " + userResponse.UserID +  " first login: " + userResponse.IsNewUserToWorld);
        OnUserLoggedIn(userResponse);
    }

    //public void GetUserDetails(string userName)
    //{
    //    Debug.Log("Get user Details");
    //    StartCoroutine(FetchUserDetails(userName));
    //}

    //IEnumerator FetchUserDetails(string username)
    //{
    //    // Build the url for the REST call
    //    string detailsUrl = String.Format("https://api.kongregate.com/api/user_info.json?username={0}", username);

    //    Debug.Log("detailsUrl: " + detailsUrl);

    //    // Do the call 
    //    WWW www = new WWW(detailsUrl);

    //    // ...and wait until we get back a response
    //    yield return www;

    //    // Get the JSON response text
    //    string responseText = www.text;
    //    Debug.Log("User Details:" + responseText);

    //    Debug.Log(www.error);
    //}

}