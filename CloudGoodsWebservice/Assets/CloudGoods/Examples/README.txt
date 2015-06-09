Welcome To The Cloud Goods SDK. We have set up a few scenes for you to demonstrate how easy it is to implement and use the Cloud Goods SDK in any of your games. Each scene demonstrates a specific feature that has prefabs that you can also use in scenes in your game in a few easy steps.

1. Socialplay Login

This scene demonstrates user persistency manangement. You can use the auto created account that was given when you had registered on the Socialplay Developer Portal. If you have not yet registered on the developer portal, Create an account at developer.socialplay.com
You can also register a new user by clicking the register button. You will have to give a valid email, and create a password and username. Once your new user has been successfully registered, you will have to check your email for an authentication email to activated your newly created user.
If at any time you forget your user's password, you can enter your users email in the email field, then click on the forgot password button. It should send an email to that user to prompt a password reset. There are various events that can be hooked onto for this system, and CloudsGoodsSDK makes it easy for
developers to integrate their game with the login system.

In the UnityUICloudGoodsLogin script, there are a few events that can be used to tell your system when things have happened.

public static event Action<CloudGoodsUser> UserLoggedIn - This event gets fired off when a user has successfully been logged in and registered a play session.
Gives a parameter CloudGoodsUser
{
	public string UserID = "";
    public bool IsNewUserToWorld = false;
    public string UserName = "";
    public string UserEmail = "";
    public string SessionId;
}

so you will have the information about the user available to you after login.

public static event Action<RegisteredUser> UserRegistered - This event gets fired off when 
public static event Action<StatusMessageResponse> PasswordResetSent;
public static event Action<StatusMessageResponse> ResentAuthentication;
public static event Action<WebserviceError> ErrorOccurred;
public static event Action<bool> UserLoggedOut;

2. Item Containers And Generation

3. Item Store

4. Item Bundle Store

5. Virtual Currency Store
