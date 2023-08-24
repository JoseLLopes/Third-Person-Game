using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using TMPro;

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

        [Header("Game Settings")]
        [SerializeField] float gameTime;
        float currentTime;

        [Header("Interface")]
        [SerializeField] TMP_Text timeText;
        
        public event Action OnStartGame;    
        public event Action OnEndGame;    

        public void ChangeToSinglePlayer(){
            currentGameMode = GAMEMODE.SINGLE;
        }

        public virtual void StartGame(){
            uiCamera.gameObject.SetActive(false);
            Transform randomSpawnPoint = playerSpawnPoints[UnityEngine.Random.Range(0,playerSpawnPoints.Length)];
            Instantiate(playerPrefab,randomSpawnPoint.position, playerPrefab.transform.rotation);
            if(OnStartGame != null)
                OnStartGame();
            currentTime = gameTime;
        }
        
        private void Update() {
            currentTime -= Time.deltaTime;
            timeText.text = "Time left: " + currentTime;
        }

        private void FinishGame(){
             if(OnEndGame != null)
                OnEndGame();
        }

    }
}