using System;
using Photon.Pun;

namespace ThirdPersonGame.NetworkSystem{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public event Action OnStartToConnectEvent;
        public event Action OnNetworkConnectedEvent;
        public event Action OnConnectedToLobbyEvent;

        //ROOM
        public event Action OnJoinedRoomEvent;
        public event Action OnLeftRoomEvent;


        public void ConnectToServer(){
            PhotonNetwork.ConnectUsingSettings();
            if(OnStartToConnectEvent != null)
                OnStartToConnectEvent();
        }

        public override void OnLeftLobby(){
            if(OnStartToConnectEvent != null)
                OnStartToConnectEvent();
            if(OnNetworkConnectedEvent != null)
                OnNetworkConnectedEvent();
        }

        public override void OnConnectedToMaster(){
            PhotonNetwork.JoinLobby();
            
            if(OnNetworkConnectedEvent != null)
                OnNetworkConnectedEvent();
        }
        public override void OnJoinedLobby(){
            if(OnConnectedToLobbyEvent != null)
                OnConnectedToLobbyEvent();
            
        }


        #region ROOM

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

        public override void OnLeftRoom(){
            if(OnLeftRoomEvent != null)
                OnLeftRoomEvent();
        }

        #endregion

    }
}