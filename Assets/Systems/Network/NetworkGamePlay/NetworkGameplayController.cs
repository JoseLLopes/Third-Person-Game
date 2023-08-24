using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.Gameplay;
using Photon.Pun;
using System;

namespace ThirdPersonGame.NetworkSystem.Gameplay{
    public class NetworkGameplayController : MonoBehaviour
    {
        
        [SerializeField] GameObject networkAvatar;
        [SerializeField] NetworkManager networkManager;
        GameObject currentAvatar;
        private void Start() {
            networkManager.OnJoinedRoomEvent += StartMultiplayer;
            GameplayController.Instance.OnEndGame += HandleFinishGame;
        }

        private void OnDisable() {
            GameplayController.Instance.OnEndGame -= HandleFinishGame;
        }


        private void HandleFinishGame()
        {
            if(GameplayController.Instance.currentGameMode == GameplayController.GAMEMODE.MULTIPLAYER){
            PhotonNetwork.Destroy(currentAvatar);
            if(PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();
            }
        }

        void StartMultiplayer(){
            GameplayController.Instance.currentGameMode = GameplayController.GAMEMODE.MULTIPLAYER;
            GameplayController.Instance.StartGame();
            currentAvatar = PhotonNetwork.Instantiate(networkAvatar.name,Vector3.zero,networkAvatar.transform.rotation);
        }

        

    }
}