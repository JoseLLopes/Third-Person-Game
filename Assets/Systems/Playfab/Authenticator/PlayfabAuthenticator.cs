using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabAuthenticator: MonoBehaviour
{
    [SerializeField] Button loginButton;
    [SerializeField] Button loginAsGuestButton;
    [SerializeField] Button createAccountButton;

    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_InputField usernameInputField;
    [SerializeField] TMP_InputField passwordInputField;


    private void Start() {
        loginAsGuestButton.onClick.AddListener(LoginAsGuest);
    }



    public void LoginAsGuest(){
        var loginRequest = new LoginWithCustomIDRequest{ CreateAccount = true, CustomId = SystemInfo.deviceUniqueIdentifier};
        PlayFabClientAPI.LoginWithCustomID(loginRequest, socceess,faliide);
    }

    private void faliide(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }

    private void socceess(LoginResult result)
    {
        Debug.Log("AOSDPOAS");
    }

    public void CreateAccount(){
        if(emailInputField.text != "" && usernameInputField.text != "" &&  passwordInputField.text != ""){
            CreateAccountWithEmail(emailInputField.text, usernameInputField.text, passwordInputField.text);
        }
        
    }

    void CreateAccountWithEmail(string email, string username, string password){
        var registerRequest = new RegisterPlayFabUserRequest{
                Email = email, 
                Username = username, 
                Password = password
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterFailed);
    }

    private void RegisterFailed(PlayFabError error)
    {
        
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        
    }
}
