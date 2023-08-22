using System;
using Photon.Pun;

namespace ThirdPersonGame.NetworkSystem{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public event Action OnNetworkConnectedEvent;
        public event Action OnConnectedToLobbyEvent;

        //ROOM
        public event Action OnJoinedRoomEvent;

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
        #endregion

    }
}