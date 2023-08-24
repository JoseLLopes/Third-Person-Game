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
        [SerializeField] GameplayController gameplayController;
        [Range(3,50)]
        [SerializeField] float timeTospawn = 10;
        [SerializeField] GameObject objectCollectable;
        [SerializeField] ScoreSpawnPointArea[] ScoreSpawnPointAreas;



        private void Start() {
            gameplayController.OnStartGame += StartSpawnNetwork;
        }

        private void StartSpawnNetwork()
        {
            if(gameplayController.currentGameMode == GameplayController.GAMEMODE.MULTIPLAYER){
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