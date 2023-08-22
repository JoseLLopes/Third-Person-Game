using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ThirdPersonGame.UI{
    public class UINetworkActions : MonoBehaviour
    {
        [SerializeField] CreateAndJoinRoom createAndJoinRoom;
        [SerializeField] ConnectToServer connectToServer;

        [Header("String Messages")]
        [SerializeField] TMP_Text messageText;
        [SerializeField] string ConnectingToServerMessage;
        [SerializeField] string ConnectingToLobbyMessage;

        [Header("Panels")]
        [SerializeField] GameObject LoadingPanel;

        private void Start() {
            connectToServer.OnNetworkConnectedEvent += HandleConnectedToServer;
            connectToServer.OnConnectedToLobbyEvent += HandleConnectedToLobby;
            messageText.text = ConnectingToServerMessage;
        }

        private void HandleConnectedToServer()
        {
            messageText.text = ConnectingToLobbyMessage;
        }

        private void HandleConnectedToLobby()
        {
            LoadingPanel.SetActive(false);
        }
    }
}