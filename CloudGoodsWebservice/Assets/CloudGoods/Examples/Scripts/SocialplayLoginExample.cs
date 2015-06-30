using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Login;
using UnityEngine.UI;

public class SocialplayLoginExample : MonoBehaviour {

    public GameObject LoginPanel;
    public GameObject RegisterPanel;
    public GameObject UserInfoPanel;
    public GameObject StatusPanel;
    public GameObject ResendAuthenticationPanel;

    public Text StatusText;

    public Text UserName;
    public Text UserEmail;
    public Text UserGuid;
    public Text SessionID;
    public Text FirstTimeLogin;

	void OnEnable () {
        UnityUICloudGoodsLogin.UserLoggedIn += UnityUICloudGoodsLogin_UserLoggedIn;
        UnityUICloudGoodsLogin.UserRegistered += UnityUICloudGoodsLogin_UserRegistered;
        UnityUICloudGoodsLogin.PasswordResetSent += UnityUICloudGoodsLogin_PasswordResetSent;
        UnityUICloudGoodsLogin.ResentAuthentication += UnityUICloudGoodsLogin_ResentAuthentication;
        UnityUICloudGoodsLogin.ErrorOccurred += UnityUICloudGoodsLogin_ErrorOccurred;
        UnityUICloudGoodsLogin.UserLoggedOut += UnityUICloudGoodsLogin_UserLoggedOut;
	}

    void OnDisable()
    {
        UnityUICloudGoodsLogin.UserLoggedIn -= UnityUICloudGoodsLogin_UserLoggedIn;
        UnityUICloudGoodsLogin.UserRegistered -= UnityUICloudGoodsLogin_UserRegistered;
        UnityUICloudGoodsLogin.PasswordResetSent -= UnityUICloudGoodsLogin_PasswordResetSent;
        UnityUICloudGoodsLogin.ResentAuthentication -= UnityUICloudGoodsLogin_ResentAuthentication;
        UnityUICloudGoodsLogin.ErrorOccurred -= UnityUICloudGoodsLogin_ErrorOccurred;
        UnityUICloudGoodsLogin.UserLoggedOut -= UnityUICloudGoodsLogin_UserLoggedOut;
    }

    void UnityUICloudGoodsLogin_UserLoggedIn(CloudGoods.SDK.Models.CloudGoodsUser obj)
    {
        SwitchToUserInfo();
        UserName.text = obj.UserName;
        UserEmail.text = obj.UserEmail;
        UserGuid.text = obj.UserID;
        SessionID.text = obj.SessionId;
        FirstTimeLogin.text = obj.IsNewUserToWorld.ToString();
    }

    void UnityUICloudGoodsLogin_UserRegistered(RegisteredUser obj)
    {
        StatusPanel.SetActive(true);
        StatusText.text = "User has successfully been registered. Check your email to authorize your user for login.";
    }

    void UnityUICloudGoodsLogin_PasswordResetSent(CloudGoods.SDK.Models.StatusMessageResponse obj)
    {
        StatusPanel.SetActive(true);
        StatusText.text = obj.message;
    }

    void UnityUICloudGoodsLogin_ResentAuthentication(CloudGoods.SDK.Models.StatusMessageResponse obj)
    {
        ResendAuthenticationPanel.SetActive(false);

        StatusPanel.SetActive(true);
        StatusText.text = "Authentication Email has been Sent.";
    }

    void UnityUICloudGoodsLogin_ErrorOccurred(CloudGoods.SDK.Models.WebserviceError obj)
    {
        if (obj.ErrorCode == 1003)
        {
            ResendAuthenticationPanel.SetActive(true);
        }
        else
        {
            StatusPanel.SetActive(true);
            StatusText.text = obj.Message;

            ResendAuthenticationPanel.SetActive(false);
        }
    }

    void UnityUICloudGoodsLogin_UserLoggedOut(bool obj)
    {
        SwitchToLogin();
    }

    public void SwitchToRegister()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
        UserInfoPanel.SetActive(false);
        StatusPanel.SetActive(false);
    }

    public void SwitchToLogin()
    {
        RegisterPanel.SetActive(false);
        LoginPanel.SetActive(true);
        UserInfoPanel.SetActive(false);
        StatusPanel.SetActive(false);
        ResendAuthenticationPanel.SetActive(false);
    }

    public void SwitchToUserInfo()
    {
        RegisterPanel.SetActive(false);
        LoginPanel.SetActive(false);
        UserInfoPanel.SetActive(true);
        StatusPanel.SetActive(false);
    }

}
