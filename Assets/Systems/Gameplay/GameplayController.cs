using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


namespace ThirdPersonGame.Gameplay{
    public class GameplayController : MonoBehaviour
    {
        
        public enum GAMEMODE{
            SINGLE,
            MULTIPLAYER
        }
        public GAMEMODE currentGameMode;

        [SerializeField] Camera uiCamera;

        [Header("Player")]
        [SerializeField] GameObject playerPrefab;
        [SerializeField] Transform[] playerSpawnPoints;

        public event Action OnStartGame;    

        public void ChangeToSinglePlayer(){
            currentGameMode = GAMEMODE.SINGLE;
        }

        public virtual void StartGame(){
            uiCamera.gameObject.SetActive(false);
            Transform randomSpawnPoint = playerSpawnPoints[UnityEngine.Random.Range(0,playerSpawnPoints.Length)];
            Instantiate(playerPrefab,randomSpawnPoint.position, playerPrefab.transform.rotation);
            if(OnStartGame != null)
                OnStartGame();
        }
        

    }
}