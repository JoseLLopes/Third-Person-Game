using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    public event Action OnNetworkConnectedEvent;
    public event Action OnConnectedToLobbyEvent;

    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
        if(OnNetworkConnectedEvent != null)
           OnNetworkConnectedEvent();
    }
    public override void OnJoinedLobby(){
        PhotonNetwork.JoinLobby();
        if(OnConnectedToLobbyEvent != null)
            OnConnectedToLobbyEvent();
    }

}
