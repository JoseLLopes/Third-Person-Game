using System;
using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.Gameplay;
using UnityEngine;
using Photon.Pun;

public class NetworkGameTime : MonoBehaviour
{

    [SerializeField] PhotonView photonView;
    float currentTime;
    [SerializeField] bool isStarted;

    private void Start() {
        GameplayController.Instance.OnStartGame += StartTime;
        GameplayController.Instance.OnEndGame += FinishGame;

    }

    private void OnDisable() {
        GameplayController.Instance.OnStartGame -= StartTime;
        GameplayController.Instance.OnEndGame -= FinishGame;

    }

    private void FinishGame()
    {
        isStarted = false;
    }

    private void StartTime()
    {
        if(PhotonNetwork.IsConnected){
            GameplayController.Instance.currentGameMode = GameplayController.GAMEMODE.MULTIPLAYER;
        }
        if(GameplayController.Instance.currentGameMode == GameplayController.GAMEMODE.MULTIPLAYER && photonView.IsMine){
            currentTime = GameplayController.Instance.gameTime;
            isStarted = true;
        }
    }

    void Update()
    {
        if(isStarted){
            currentTime -= Time.deltaTime;
            if(photonView.IsMine){
                photonView.RPC("RPC_UpdateTime", RpcTarget.All, currentTime);
            }
        }
                
    }

    [PunRPC]
    void RPC_UpdateTime(float time){
            Debug.Log(time);
            GameplayController.Instance.UpdateTimeUI(time);
        }
}

