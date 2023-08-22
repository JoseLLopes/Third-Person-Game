using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{   

    public event Action OnJoinedRoomEvent;

    public void CreateRoom(string roomName){
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(string roomName){
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom(){
        if(OnJoinedRoomEvent != null)
            OnJoinedRoomEvent();
    }

}
