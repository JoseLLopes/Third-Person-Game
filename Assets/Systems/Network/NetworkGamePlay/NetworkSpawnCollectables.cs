using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ThirdPersonGame.Gameplay;
using System;
using ThirdPersonGame.ScoreSystem;

namespace ThirdPersonGame.NetworkSystem.Gameplay{
    public class NetworkSpawnCollectables : MonoBehaviour
    {
        [SerializeField] PhotonView photonView;
        [Range(3,50)]
        [SerializeField] float timeTospawn = 5;
        [SerializeField] GameObject objectCollectable;
        [SerializeField] ScoreSpawnPointArea[] ScoreSpawnPointAreas;

        

        private void Start() {
            GameplayController.Instance.OnStartGame += StartSpawnNetwork;
        }

        private void OnDisable() {
            GameplayController.Instance.OnStartGame -= StartSpawnNetwork;
        }

        private void StartSpawnNetwork()
        {
            if(GameplayController.Instance.currentGameMode == GameplayController.GAMEMODE.MULTIPLAYER){
                StartCoroutine(SpawnCollectable());
            }
        }

        IEnumerator SpawnCollectable(){
            if(photonView.IsMine){
                photonView.RPC("RPC_SpawnCollectable",RpcTarget.AllBuffered,GetRandomPosition());
            }
            yield return new WaitForSeconds(timeTospawn);
            StartCoroutine(SpawnCollectable());
        }

       Vector3 GetRandomPosition(){
            int randomSpawnPointIndex = UnityEngine.Random.Range(0,ScoreSpawnPointAreas.Length);
            return ScoreSpawnPointAreas[randomSpawnPointIndex].GetRandomPosition();
        }

        [PunRPC]
        void RPC_SpawnCollectable(Vector3 position){
            var collectableObject = Instantiate(objectCollectable,
                                                    position,
                                                    objectCollectable.transform.rotation);
                ScoreManager.Instance.AddCollectableToList(collectableObject);
        }


    }
}