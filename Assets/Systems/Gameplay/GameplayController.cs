using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using TMPro;

namespace ThirdPersonGame.Gameplay{
    public class GameplayController : MonoBehaviour
    {
        
        public static GameplayController Instance;

        public enum GAMEMODE{
            SINGLE,
            MULTIPLAYER
        }
        public GAMEMODE currentGameMode;

        [SerializeField] Camera uiCamera;

        [Header("Player")]
        [SerializeField] GameObject playerPrefab;
        GameObject currentPlayer;
        [SerializeField] Transform[] playerSpawnPoints;

        [Header("Game Settings")]
        public float gameTime;
        float currentTime;
        bool isStarted;

        [Header("Interface")]
        [SerializeField] TMP_Text timeText;
        [SerializeField] TMP_Text scoreText;
        
        
        public event Action OnStartGame;    
        public event Action OnEndGame;    

        private void Awake() {
            if(Instance == null){
                Instance = this;
            }
        }

        public void ChangeToSinglePlayer(){
            currentGameMode = GAMEMODE.SINGLE;
        }

        public virtual void StartGame(){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            uiCamera.gameObject.SetActive(false);
            Transform randomSpawnPoint = playerSpawnPoints[UnityEngine.Random.Range(0,playerSpawnPoints.Length)];
            currentPlayer = Instantiate(playerPrefab,randomSpawnPoint.position, playerPrefab.transform.rotation);
            currentTime = gameTime;
            isStarted= true;
            if(OnStartGame != null)
                OnStartGame();
        }
        
        private void Update() {
            if(isStarted && currentGameMode == GAMEMODE.SINGLE){
                currentTime -= Time.deltaTime;
                UpdateTimeUI(currentTime); 
            }
        }

        private void FinishGame(){
            if(OnEndGame != null)
               OnEndGame();
            Destroy(currentPlayer);
            uiCamera.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void UpdateTimeUI(float currentTimeToUpdate){
            timeText.text = "Time left: " + (int)currentTimeToUpdate;
            if(currentTimeToUpdate <= 0){
                    FinishGame();
            }
        }
       
    }
}