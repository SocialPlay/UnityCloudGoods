Welcome To The Cloud Goods SDK. We have set up a few scenes for you to demonstrate how easy it is to implement and use the Cloud Goods SDK in any of your games. Each scene demonstrates a specific feature that has prefabs that you can also use in scenes in your game in a few easy steps.

1. Socialplay Login

This scene demonstrates user persistency manangement. You can use the auto created account that was given when you had registered on the Socialplay Developer Portal. If you have not yet registered on the developer portal, Create an account at developer.socialplay.com
You can also register a new user by clicking the register button. You will have to give a valid email, and create a password and username. Once your new user has been successfully registered, you will have to check your email for an authentication email to activated your newly created user.
If at any time you forget your user's password, you can enter your users email in the email field, then click on the forgot password button. It should send an email to that user to prompt a password reset. There are various events that can be hooked onto for this system, and CloudsGoodsSDK makes it easy for
developers to integrate their game with the login system.

In the UnityUICloudGoodsLogin script, there are a few events that can be used to tell your system when things have happened.

public static event Action<CloudGoodsUser> UserLoggedIn - This event gets fired off when a user has successfully been logged in and registered a play session. Sends CloudGoodsUser on the event callback so you will have various information about the user after login

class CloudGoodsUser
{
	public string UserID;
    public bool IsNewUserToWorld;
    public string UserName;
    public string UserEmail;
    public string SessionId;
}


public static event Action<RegisteredUser> UserRegistered - This event gets fired off when the user has successfully registered a user to your world. It sends a RegisteredUser on the event to give you some information regarding the registered user and the status of the registration

public class RegisteredUser
{
    public int ID;
    public bool Active;
    public bool Deleted;
    public int PlatformId;
    public string PlatformUserId;
    public string FirstName;
    public string LastName;
    public string email;
    public string password;
    public Nullable<int> WorldID;
}


public static event Action<StatusMessageResponse> PasswordResetSent - This event gets fired off after an email has been successfully sent to the user requesting a password reset. Sends a StatusMEssageResponse object

    public class StatusMessageResponse
    {
        public int code;
        public string message;
    }


public static event Action<StatusMessageResponse> ResentAuthentication - This event gets fired off when the user has requested a authorization email to be resent to their email. Sends a StatusMessageResponse on the callback event.

    public class StatusMessageResponse
    {
        public int code;
        public string message;
    }

public static event Action<WebserviceError> ErrorOccurred;
public static event Action<bool> UserLoggedOut;

2. Item Containers And Generation

3. Item Store

4. Item Bundle Store

5. Virtual Currency Store
