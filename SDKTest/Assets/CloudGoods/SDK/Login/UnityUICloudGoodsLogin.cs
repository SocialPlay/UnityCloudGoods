using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using CloudGoods.Enums;
using CloudGoods.SDK.Models;
using CloudGoods.SDK.Utilities;
using CloudGoods.Services;
using CloudGoods.Services.WebCommunication;

namespace CloudGoods.SDK.Login
{
    public class UnityUICloudGoodsLogin : MonoBehaviour
    {

        #region Login variables
        public InputField loginUserEmail;
        public InputField loginUserPassword;

        private InputFieldValidation loginUserEmailValidator;
        private InputFieldValidation loginUserPasswordValidator;


        #endregion

        #region Register variables
        public InputField registerUserEmail;
        public InputField registerUserPassword;
        public InputField registerUserPasswordConfirm;
        public InputField registerUserName;

        private InputFieldValidation registerUserEmailValidator;
        private InputFieldValidation registerUserPasswordValidator;
        private InputFieldValidation registerUserPasswordConfirmValidator;
        #endregion


        #region ResendVerification Variables

        public GameObject ResendVerificationWindow;

        #endregion

        #region Events

        public static event Action<CloudGoodsUser> UserLoggedIn;
        public static event Action<RegisteredUser> UserRegistered;
        public static event Action<StatusMessageResponse> PasswordResetSent;
        public static event Action<StatusMessageResponse> ResentAuthentication;
        public static event Action<WebserviceError> ErrorOccurred;
        public static event Action<bool> UserLoggedOut;

        #endregion

        public bool IsKeptActiveOnAllPlatforms;

        void Awake()
        {
            CallHandler.IsError += CallHandler_onErrorEvent;
        }


        void Start()
        {
            if (!RemoveIfNeeded())
            {
                return;
            }

            loginUserEmailValidator = loginUserEmail.GetComponent<InputFieldValidation>();
            loginUserPasswordValidator = loginUserPassword.GetComponent<InputFieldValidation>();

            registerUserEmailValidator = registerUserEmail.GetComponent<InputFieldValidation>();
            registerUserPasswordValidator = registerUserPassword.GetComponent<InputFieldValidation>(); ;
            registerUserPasswordConfirmValidator = registerUserPasswordConfirm.GetComponent<InputFieldValidation>();

            if (!string.IsNullOrEmpty(PlayerPrefs.GetString("SocialPlay_Login_UserEmail")))
            {
                loginUserEmail.text = PlayerPrefs.GetString("SocialPlay_Login_UserEmail");
            }
            else
            {
                loginUserEmail.text = "";
            }
        }

        #region webservice responce events

        void OnReceivedCloudGoodsUser(CloudGoodsUser userLoginResponse)
        {
            if(UserLoggedIn != null)
                UserLoggedIn(userLoginResponse);
        }

        void OnRegisteredUser(RegisteredUser registeredUserResponse)
        {
            if(UserRegistered != null)
                UserRegistered(registeredUserResponse);
        }

        void OnSentPassword(StatusMessageResponse sentPasswordResponse)
        {
            if(PasswordResetSent != null)
                PasswordResetSent(sentPasswordResponse);
        }

        void OnResentAuthenticationEmail(StatusMessageResponse authenticationResponse)
        {
            if(ResentAuthentication != null)
                ResentAuthentication(authenticationResponse);
        }

        void CallHandler_onErrorEvent(WebserviceError errorResponse)
        {
            if(ErrorOccurred != null)
                ErrorOccurred(errorResponse);
        }

        #endregion

        #region button functions

        public void Login()
        {
            Debug.Log("Login");

            string ErrorMsg = "";
            if (!loginUserEmailValidator.IsValidCheck())
            {
                ErrorMsg = "-Invalid Email";
            }

            Debug.Log("email validated");

            if (!loginUserPasswordValidator.IsValidCheck())
            {
                if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
                ErrorMsg += "-Invalid Password";
            }

            if (string.IsNullOrEmpty(ErrorMsg))
            {
                Debug.Log("login email: " + loginUserEmail.text + " login password: " + loginUserPassword.text);

                PlayerPrefs.SetString("SocialPlay_Login_UserEmail", loginUserEmail.text);
                AccountServices.Login(new LoginRequest(loginUserEmail.text.ToLower(), loginUserPassword.text), OnReceivedCloudGoodsUser);
            }
        }

        public void Register()
        {
            string ErrorMsg = "";
            if (!registerUserEmailValidator.IsValidCheck())
            {
                if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
                ErrorMsg += "-Invalid Email";
            }

            if (!registerUserPasswordValidator.IsValidCheck() || !registerUserPasswordConfirmValidator.IsValidCheck())
            {
                if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
                ErrorMsg += "-Invalid Password";
            }
            if (string.IsNullOrEmpty(ErrorMsg))
            {
                AccountServices.Register(new RegisterUserRequest(registerUserName.text, registerUserEmail.text, registerUserPassword.text), OnRegisteredUser);
            }
        }

        public void ForgotPassword()
        {

            string ErrorMsg = "";
            if (!loginUserEmailValidator.IsValidCheck())
            {
                ErrorMsg = "Password reset requires valid E-mail";
            }

            if (string.IsNullOrEmpty(ErrorMsg))
            {
                AccountServices.ForgotPassword(new ForgotPasswordRequest(loginUserEmail.text), OnSentPassword);
            }
        }



        public void ResendVerificationEmail()
        {
            string ErrorMsg = "";
            if (!loginUserEmailValidator.IsValidCheck())
            {
                ErrorMsg = "Validation resend requires valid E-mail";
            }

            if (string.IsNullOrEmpty(ErrorMsg))
            {
                AccountServices.ResendVerificationEmail(new ResendVerificationRequest(loginUserEmail.text), OnResentAuthenticationEmail);
            }

        }

        public void Logout()
        {
            AccountServices.Logout();

            if(UserLoggedOut != null)
                UserLoggedOut(true);
        }


        #endregion

        public bool RemoveIfNeeded()
        {
            if (IsKeptActiveOnAllPlatforms) return true;

            if (BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Automatic)
            {
                BuildPlatform.OnBuildPlatformFound += platform => { RemoveIfNeeded(); };
                return false;
            }

            if (BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Facebook || BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Kongergate)
            {
                Destroy(this);
                return false;
            }
            return true;
        }
    }
}
