using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using ThirdPersonGame.Playfab.Data;

namespace ThirdPersonGame.Playfab.Authenticator{
public class PlayfabAuthenticator: MonoBehaviour
{
    [SerializeField] Button loginButton;
    [SerializeField] Button loginAsGuestButton;
    [SerializeField] Button createAccountButton;

    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] GameObject loginPanel;
    public string playfabID;

    //Events
    public event Action OnLogedinEvent;

    private void Start() {
        loginButton.onClick.AddListener(OnclickLoginWithEmail);
        loginAsGuestButton.onClick.AddListener(OnClickLoginAsGuest);
        createAccountButton.onClick.AddListener(OnClickCreateAccont);
    }

    //BUttons
    public void OnclickLoginWithEmail(){

        if(emailInputField.text != "" &&  passwordInputField.text != ""){
            LoginWithEmail(emailInputField.text, passwordInputField.text);
        }
        else if (emailInputField.text == ""){
            errorText.text = "Please provide an email";
        }
        else if (passwordInputField.text == ""){
            errorText.text = "Please provide a password";
        }
    }

    public void OnClickCreateAccont(){
         if(emailInputField.text != "" &&  passwordInputField.text != ""){
            CreateAccountWithEmail(emailInputField.text, passwordInputField.text);
        }
        else if (emailInputField.text == ""){
            errorText.text = "Please provide an email";
        }
        else if (passwordInputField.text == ""){
            errorText.text = "Please provide a password";
        }
    }

    #region GUEST
    public void OnClickLoginAsGuest(){
        var loginRequest = new LoginWithCustomIDRequest{ CreateAccount = true, CustomId = SystemInfo.deviceUniqueIdentifier};
        PlayFabClientAPI.LoginWithCustomID(loginRequest, LoginAsGuestSuccess,LoginAsGuestFailed);
    }

    private void LoginAsGuestFailed(PlayFabError error)
    {
        errorText.text = error.ErrorMessage;
    }
    

    private void LoginAsGuestSuccess(LoginResult result)
    {
        if(OnLogedinEvent != null)
            OnLogedinEvent();
        playfabID = result.PlayFabId;
        PlayfabDataManager.GetUserData(playfabID);
        loginPanel.SetActive(false);
        
    }
    #endregion

    #region EMAIL


    void CreateAccountWithEmail(string email, string password){
        var registerRequest = new RegisterPlayFabUserRequest{
                Email = email,  
                Password = password,
                RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterFailed);
    }

    void LoginWithEmail(string email, string password){
        var loginWithEmailRequest = new LoginWithEmailAddressRequest{
            Email = email, 
            Password = password,
        };
        PlayFabClientAPI.LoginWithEmailAddress(loginWithEmailRequest,LoginSuccess,LoginFailed);
    }

    private void LoginFailed(PlayFabError error)
    {
        errorText.text = error.ErrorMessage;
    }

    private void LoginSuccess(LoginResult result)
    {
        if(OnLogedinEvent != null)
            OnLogedinEvent();
        playfabID = result.PlayFabId;
        PlayfabDataManager.GetUserData(playfabID);
        loginPanel.SetActive(false);

    }

    private void RegisterFailed(PlayFabError error)
    {
        errorText.text = error.ErrorMessage;
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        playfabID = result.PlayFabId;
        PlayfabDataManager.GetUserData(playfabID);
        if(OnLogedinEvent != null)
            OnLogedinEvent();
        loginPanel.SetActive(false);
        
    }
    #endregion

}
}