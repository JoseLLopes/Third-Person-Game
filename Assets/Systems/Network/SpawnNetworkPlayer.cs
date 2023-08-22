using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ThirdPersonGame.NetworkSystem{
public class SpawnNetworkPlayer: MonoBehaviour
{
    [SerializeField] GameObject photonPlayer;
    [SerializeField] NetworkManager networkManager;

    private void Start() {
        networkManager.OnJoinedRoomEvent += HandleJoinedRoom;
    }

    private void HandleJoinedRoom()
    {
        PhotonNetwork.Instantiate(photonPlayer.name,default,default);
    }
}
}