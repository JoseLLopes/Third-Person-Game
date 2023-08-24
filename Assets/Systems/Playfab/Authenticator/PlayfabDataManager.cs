using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;


namespace ThirdPersonGame.Playfab.Data{
    public class PlayfabDataManager
    {

        public static string bestScore;

        public static void SetUserData() {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                Data = new Dictionary<string, string>() {
                    {"BestScore", bestScore}
                }
            },
            result => Debug.Log("Successfully updated user data"),
            error => {
                Debug.Log("Got error setting user data");
                Debug.Log(error.GenerateErrorReport());
            });
        }

        public static void GetUserData(string myPlayFabId) {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                PlayFabId = myPlayFabId,
                Keys = null
            }, OnGetDataSuccess, OnGetDataFailed);
        }



        private static void OnGetDataSuccess(GetUserDataResult result)
        {
            if(result.Data != null && result.Data.ContainsKey("BestScore")){
                bestScore = result.Data["BestScore"].Value;
            }else{
                bestScore = "0";
            }
        }
        private static void OnGetDataFailed(PlayFabError error)
        {
            bestScore = "Error";
        }

    }
}