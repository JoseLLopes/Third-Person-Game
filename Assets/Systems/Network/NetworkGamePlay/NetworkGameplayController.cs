using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.Gameplay;
using Photon.Pun;

namespace ThirdPersonGame.NetworkSystem.Gameplay{
    public class NetworkGameplayController : MonoBehaviour
    {
        
        [SerializeField] GameObject networkAvatar;
        [SerializeField] NetworkManager networkManager;
        [SerializeField] GameplayController gameplayController;
        private void Start() {
            networkManager.OnJoinedRoomEvent += StartMultiplayer;
        }

        void StartMultiplayer(){
            gameplayController.currentGameMode = GameplayController.GAMEMODE.MULTIPLAYER;
            gameplayController.StartGame();
            PhotonNetwork.Instantiate(networkAvatar.name,Vector3.zero,networkAvatar.transform.rotation);
        }

        

    }
}