using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CloudGoods.SDK.Login;
using CloudGoods.Services;
using CloudGoods.SDK.Models;
using CloudGoods.Services.WebCommunication;

public class ExampleSceneLogin : MonoBehaviour {

    public InputField userEmailInput;
    public InputField userPassword;

    private InputFieldValidation loginUserEmailValidator;
    private InputFieldValidation loginUserPasswordValidator;

    public GameObject ExampleScene;

    public GameObject StatusPanel;
    public Text StatusText;

    void Start()
    {
        CallHandler.IsError += CallHandler_IsError;
        loginUserEmailValidator = userEmailInput.GetComponent<InputFieldEmailValidation>();
        loginUserPasswordValidator = userPassword.GetComponent<InputFieldPasswordValidation>();

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("SocialPlay_Login_UserEmail")))
        {
            userEmailInput.text = PlayerPrefs.GetString("SocialPlay_Login_UserEmail");
        }
    }

    void CallHandler_IsError(WebserviceError obj)
    {
        StatusText.text = obj.Message;
        StatusPanel.SetActive(true);
    }

    public void Login()
    {
        string ErrorMsg = "";

        PlayerPrefs.SetString("SocialPlay_Login_UserEmail", userEmailInput.text);

        if (!loginUserEmailValidator.IsValidCheck())
        {
            ErrorMsg = "Invalid Email";
        }

        if (!loginUserPasswordValidator.IsValidCheck())
        {
            if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
            ErrorMsg = "Invalid Password";
        }

        if (string.IsNullOrEmpty(ErrorMsg))
        {
            Debug.Log("login email: " + userEmailInput.text + " login password: " + userPassword.text);

            PlayerPrefs.SetString("SocialPlay_Login_UserEmail", userEmailInput.text);
            AccountServices.Login(new LoginRequest(userEmailInput.text.ToLower(), userPassword.text), OnReceivedCloudGoodsUser);
        }


    }

    void OnReceivedCloudGoodsUser(CloudGoodsUser user)
    {
        ExampleScene.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void CloseStatusPanel()
    {
        StatusPanel.SetActive(false);
    }
    
}
