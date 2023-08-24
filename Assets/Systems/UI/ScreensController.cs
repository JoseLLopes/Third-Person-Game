using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonGame.Gameplay;
using System;
using ThirdPersonGame.NetworkSystem;
using ThirdPersonGame.Pagination;

namespace ThirdPersonGame.UI{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField] NetworkManager networkManager;
        [SerializeField] PaginationSystem paginationSystem;
        [SerializeField] Page inGameUI;
        [SerializeField] Page lobbyUI;
        [SerializeField] Page menuUi;
    

        private void Start() {
            GameplayController.Instance.OnStartGame += ActiveInGameUI;
            GameplayController.Instance.OnEndGame += ActiveMenuUI;

            networkManager.OnConnectedToLobbyEvent += ActiveLobbyUI;
            networkManager.OnLeftRoomEvent += ActiveLobbyUI;
            networkManager.OnJoinedRoomEvent += ActiveInGameUI;
        }

        private void OnDisable() {
            GameplayController.Instance.OnStartGame -= ActiveInGameUI;
            GameplayController.Instance.OnEndGame -= ActiveMenuUI;

            networkManager.OnConnectedToLobbyEvent -= ActiveLobbyUI;
            networkManager.OnLeftRoomEvent -= ActiveLobbyUI;
            networkManager.OnJoinedRoomEvent -= ActiveInGameUI;
        }

        void ActiveLobbyUI()
        {
            paginationSystem.ShowLonely(lobbyUI);
        }

        void ActiveInGameUI()
        {
            paginationSystem.ShowLonely(inGameUI);
        }

        void ActiveMenuUI()
        {
            if(GameplayController.Instance.currentGameMode == GameplayController.GAMEMODE.SINGLE){
                paginationSystem.ShowLonely(menuUi);
            }else
            {
                paginationSystem.ShowLonely(lobbyUI);
            }
        }

        
    }
}